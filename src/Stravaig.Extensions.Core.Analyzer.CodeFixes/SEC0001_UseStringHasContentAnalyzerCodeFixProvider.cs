using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
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
                        ChangeStringIsNullOrWhiteSpaceToHasContent(root, context.Document, notExpression)),
                    equivalenceKey: nameof(CodeFixResources.SEC0001CodeFixTitle)),
                diagnostic);
        }
    }

    private Document ChangeStringIsNullOrWhiteSpaceToHasContent(CompilationUnitSyntax rootNode, Document document, ExpressionSyntax stringIsNullOrWhiteSpaceExpression)
    {
        var invocation = stringIsNullOrWhiteSpaceExpression.ChildNodes().OfType<InvocationExpressionSyntax>().First();
        var stringArg = (ExpressionSyntax)invocation.ArgumentList.Arguments.First().ChildNodes().First();
        var hasContentExpression = BuildStringHasContentNodes(stringArg);

        rootNode = rootNode.ReplaceNode(stringIsNullOrWhiteSpaceExpression, hasContentExpression);

        rootNode = rootNode.UseStravaigExtensionsCore();

        return document.WithSyntaxRoot(rootNode);
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