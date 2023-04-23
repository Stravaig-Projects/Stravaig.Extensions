using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using Document = Microsoft.CodeAnalysis.Document;

namespace Stravaig.Extensions.Core.Analyzer;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(SEC0001_UseStringHasContentAnalyzerCodeFixProvider)), Shared]
// ReSharper disable once InconsistentNaming
public class SEC0001_UseStringHasContentAnalyzerCodeFixProvider : CodeFixProvider
{
    private static readonly ImmutableArray<string> CachedFixableDiagnostics =
        ImmutableArray.Create(SEC0001_UseStringHasContentAnalyzer.DiagnosticId);
    public sealed override ImmutableArray<string> FixableDiagnosticIds => CachedFixableDiagnostics;

    public sealed override FixAllProvider GetFixAllProvider()
    {
        // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
        return WellKnownFixAllProviders.BatchFixer;
    }

    public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var root = (CompilationUnitSyntax)await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
        if (root == null)
            return;

        var diagnostic = context.Diagnostics.First();
        var diagnosticSpan = diagnostic.Location.SourceSpan;

        // Find the type declaration identified by the diagnostic.
        var declaration = root.FindNode(diagnosticSpan);

        var declarationKind = declaration.Kind();
        if (declarationKind is SyntaxKind.LogicalNotExpression or SyntaxKind.EqualsExpression)
        {
            var notExpression = (ExpressionSyntax)declaration;
            context.RegisterCodeFix(
                CodeAction.Create(
                    title: CodeFixResources.SEC0001CodeFixTitle,
                    createChangedDocument: ct => Task.FromResult(
                        ChangeStringIsNullOrWhiteSpaceToHasContent(root, context.Document, notExpression, ct)),
                    equivalenceKey: nameof(CodeFixResources.SEC0001CodeFixTitle)),
                diagnostic);
        }
    }

    private Document ChangeStringIsNullOrWhiteSpaceToHasContent(CompilationUnitSyntax rootNode, Document document, ExpressionSyntax stringIsNullOrWhiteSpaceExpression, CancellationToken ct)
    {
        var invocation = stringIsNullOrWhiteSpaceExpression.ChildNodes().OfType<InvocationExpressionSyntax>().First();
        var stringArg = (ExpressionSyntax)invocation.ArgumentList.Arguments.First().ChildNodes().First();
        var hasContentExpression = BuildStringHasContentNodes(stringArg);

        rootNode = rootNode.ReplaceNode(stringIsNullOrWhiteSpaceExpression, hasContentExpression);

        rootNode = UseStravaigExtensionsCore(rootNode);

        return document.WithSyntaxRoot(rootNode);
    }

    private CompilationUnitSyntax UseStravaigExtensionsCore(CompilationUnitSyntax oldRoot)
    {
        SyntaxList<UsingDirectiveSyntax> usingDeclarations;
        if (oldRoot.Usings.Count != 0)
        {
            usingDeclarations = oldRoot.Usings;
        }
        else
        {
            var searchStart = GetNamespaceDeclaration(oldRoot);
            usingDeclarations = searchStart?.Usings ?? oldRoot.Usings;
        }

        (bool usingExists, SyntaxNode insertBefore) = FindInsertionPoint(usingDeclarations);

        if (usingExists)
            return oldRoot;
       
        return insertBefore != null 
            ? oldRoot.InsertNodesBefore(insertBefore, UsingStravaigExtensionsCore())
            : oldRoot.AddUsings(UsingStravaigExtensionsCore());
    }

    private static BaseNamespaceDeclarationSyntax GetNamespaceDeclaration(CompilationUnitSyntax oldRoot)
    {
        
        var childNodes = oldRoot.Members;
        var fileScopesNamespaceNodes = childNodes.OfType<FileScopedNamespaceDeclarationSyntax>();
        var namespaceDeclaration = fileScopesNamespaceNodes.FirstOrDefault();
        if (namespaceDeclaration != null)
            return namespaceDeclaration;

        var traditionalNamespaceNodes = childNodes.OfType<NamespaceDeclarationSyntax>();
        return traditionalNamespaceNodes.FirstOrDefault();
    }

    private static (bool, SyntaxNode) FindInsertionPoint(SyntaxList<UsingDirectiveSyntax> usings)
    {
        SyntaxNode insertBefore = null;
        foreach (var usingDirective in usings)
        {
            string usingNamespace = "";
            if (usingDirective.Name.Kind() == SyntaxKind.IdentifierName)
            {
                var identifierName = (IdentifierNameSyntax)usingDirective.Name;
                usingNamespace = identifierName.Identifier.Text;
            }
            else if (usingDirective.Name.Kind() == SyntaxKind.QualifiedName)
            {
                var qualifiedIdentifierName = (QualifiedNameSyntax)usingDirective.Name;
                usingNamespace = qualifiedIdentifierName.AsString();
            }

            if (usingNamespace.Equals("Stravaig.Extensions.Core"))
                return (true, null);

            if (usingNamespace.IsAfter("Stravaig.Extensions.Core", StringComparison.Ordinal))
            {
                insertBefore ??= usingDirective;
            }
        }

        return (false, insertBefore);
    }

    private UsingDirectiveSyntax[] UsingStravaigExtensionsCore()
    {
        SimpleNameSyntax stravaig = SyntaxFactory.IdentifierName("Stravaig");
        SimpleNameSyntax extensions = SyntaxFactory.IdentifierName("Extensions");
        SimpleNameSyntax core = SyntaxFactory.IdentifierName("Core");
        QualifiedNameSyntax stravaigExtensions = SyntaxFactory.QualifiedName(stravaig, extensions);
        NameSyntax name = SyntaxFactory.QualifiedName(stravaigExtensions, core);
        UsingDirectiveSyntax usingStatement = SyntaxFactory.UsingDirective(name);
        return new[] { usingStatement };
    }
    
    private static InvocationExpressionSyntax BuildStringHasContentNodes(ExpressionSyntax stringExpression)
    {
        var simpleNameSyntax = SyntaxFactory.IdentifierName("HasContent");
        var simpleMemberAccessExpression = SyntaxFactory.MemberAccessExpression(
            SyntaxKind.SimpleMemberAccessExpression,
            stringExpression,
            SyntaxFactory.Token(SyntaxKind.DotToken),
            simpleNameSyntax);
        var invocationExpression = SyntaxFactory.InvocationExpression(simpleMemberAccessExpression);
        return invocationExpression;
    }
}