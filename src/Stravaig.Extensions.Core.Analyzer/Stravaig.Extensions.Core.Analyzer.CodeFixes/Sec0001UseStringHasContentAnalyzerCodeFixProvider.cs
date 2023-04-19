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

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(Sec0001UseStringHasContentAnalyzerCodeFixProvider)), Shared]
public class Sec0001UseStringHasContentAnalyzerCodeFixProvider : CodeFixProvider
{
    private static readonly ImmutableArray<string> CachedFixableDiagnostics =
        ImmutableArray.Create(Sec0001UseStringHasContentAnalyzer.DiagnosticId);
    public sealed override ImmutableArray<string> FixableDiagnosticIds => CachedFixableDiagnostics;

    public sealed override FixAllProvider GetFixAllProvider()
    {
        // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
        return WellKnownFixAllProviders.BatchFixer;
    }

    public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

        // TODO: Replace the following code with your own analysis, generating a CodeAction for each fix to suggest
        var diagnostic = context.Diagnostics.First();
        var diagnosticSpan = diagnostic.Location.SourceSpan;

        // Find the type declaration identified by the diagnostic.
        var declaration = root.FindToken(diagnosticSpan.Start);

        var declarationKind = declaration.Kind();
        switch (declarationKind)
        {
            case SyntaxKind.ExclamationToken:
                var notExpression = (PrefixUnaryExpressionSyntax) declaration.Parent;
                context.RegisterCodeFix(
                    CodeAction.Create(
                        title: CodeFixResources.SEC0001CodeFixTitle,
                        createChangedDocument: ct => FixNotStringIsNullOrWhiteSpaceAsync(context.Document, notExpression, ct),
                        equivalenceKey: nameof(CodeFixResources.SEC0001CodeFixTitle)),
                    diagnostic);
                break;
            case SyntaxKind.EqualsExpression:
                var equalsExpression = (BinaryExpressionSyntax) declaration.Parent;
                break;
        }
    }

    private async Task<Document> FixNotStringIsNullOrWhiteSpaceAsync(Document document, PrefixUnaryExpressionSyntax notExpression, CancellationToken ct)
    {
        try
        {
            int aNumber = 123;
            string someString = "someString";
            if (!string.IsNullOrWhiteSpace(someString))
            {
            }

            if (!string.IsNullOrWhiteSpace(aNumber.ToString()))
            {
            }

            if (someString.Contains("s"))
            {
            }

            if (aNumber.ToString().Contains("1"))
            {
            }

            var invocation = notExpression.ChildNodes().OfType<InvocationExpressionSyntax>().First();
            var stringObjectToken =
                (IdentifierNameSyntax)invocation.ArgumentList.Arguments.First().ChildNodes().First();

            var invocationExpression = BuildStringHasContentNodes(stringObjectToken);

            SyntaxNode oldRoot = await document.GetSyntaxRootAsync(ct).ConfigureAwait(false);
            SyntaxNode newRoot = oldRoot.ReplaceNode(notExpression, invocationExpression);

            newRoot = UseStravaigExtensionsCore(newRoot);

            return document.WithSyntaxRoot(newRoot);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("It went horribly horribly wrong!");
            Debug.WriteLine(ex);
            throw;
        }
    }

    private SyntaxNode UseStravaigExtensionsCore(SyntaxNode oldRoot)
    {
        SyntaxNode insertBefore = null;
        foreach (var usingDirective in oldRoot.ChildNodes().OfType<UsingDirectiveSyntax>())
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
                usingNamespace = qualifiedIdentifierName.QualifiedText();
            }
            if (usingNamespace.Equals("Stravaig.Extensions.Core"))
                return oldRoot;

            if (usingNamespace.IsAfter("Stravaig.Extensions.Core", StringComparison.Ordinal))
            {
                insertBefore = usingDirective;
                break;
            }
        }

        insertBefore ??= oldRoot.ChildNodes().First();
        var newRoot = oldRoot.InsertNodesBefore(insertBefore, UsingStravaigExtensionsCore());

        return newRoot;
    }

    private IEnumerable<SyntaxNode> UsingStravaigExtensionsCore()
    {
        SimpleNameSyntax stravaig = SyntaxFactory.IdentifierName("Stravaig");
        SimpleNameSyntax extensions = SyntaxFactory.IdentifierName("Extensions");
        SimpleNameSyntax core = SyntaxFactory.IdentifierName("Core");
        QualifiedNameSyntax stravaigExtensions = SyntaxFactory.QualifiedName(stravaig, extensions);
        NameSyntax name = SyntaxFactory.QualifiedName(stravaigExtensions, core);
        SyntaxNode usingStatement = SyntaxFactory.UsingDirective(name);
        return new[] { usingStatement };
    }

    private static InvocationExpressionSyntax BuildStringHasContentNodes(IdentifierNameSyntax stringObjectToken)
    {
        var stringIdentifierName = SyntaxFactory.IdentifierName(stringObjectToken.GetFirstToken().Text);
        var simpleNameSyntax = SyntaxFactory.IdentifierName("HasContent");
        var simpleMemberAccessExpression = SyntaxFactory.MemberAccessExpression(
            SyntaxKind.SimpleMemberAccessExpression,
            stringIdentifierName,
            SyntaxFactory.Token(SyntaxKind.DotToken),
            simpleNameSyntax);
        var invocationExpression = SyntaxFactory.InvocationExpression(simpleMemberAccessExpression);
        return invocationExpression;
    }
}

public static class QualifiedNameSyntaxExtensions
{
    public static string QualifiedText(this QualifiedNameSyntax qualifiedName)
    {
        string left = qualifiedName.Left.Kind() == SyntaxKind.QualifiedName
            ? ((QualifiedNameSyntax)qualifiedName.Left).QualifiedText()
            : ((IdentifierNameSyntax)qualifiedName.Left).Identifier.Text;
        string right = ((IdentifierNameSyntax)qualifiedName.Right).Identifier.Text;
        return $"{left}.{right}";
    }
}// 