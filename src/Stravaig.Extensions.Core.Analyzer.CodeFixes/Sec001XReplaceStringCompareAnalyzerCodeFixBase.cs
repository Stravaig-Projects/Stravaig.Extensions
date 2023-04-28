using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Stravaig.Extensions.Core.Analyzer.Extensions;

namespace Stravaig.Extensions.Core.Analyzer;

public abstract class Sec001XReplaceStringCompareAnalyzerCodeFixBase : CodeFixProvider
{
    protected abstract string Title { get; }
    protected abstract string ReplacementCall { get; }
    protected abstract string EquivalenceKey { get; }

    public sealed override FixAllProvider GetFixAllProvider()
    {
        // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
        return WellKnownFixAllProviders.BatchFixer;
    }

    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var root = await context.GetCompilationRootAsync();
        if (root == null)
            return;

        var diagnostic = context.Diagnostics.First();
        var diagnosticSpan = diagnostic.Location.SourceSpan;

        // Find the type declaration identified by the diagnostic.
        var declaration = root.FindNode(diagnosticSpan);

        var expression = (BinaryExpressionSyntax)declaration;

        context.RegisterCodeFix(
            CodeAction.Create(
                title: Title,
                createChangedDocument: _ => Task.FromResult(
                    ReplaceCode(root, context.Document, expression)),
                equivalenceKey: EquivalenceKey),
            diagnostic);
    }

    protected InvocationExpressionSyntax BuildReplacementExpression(ExpressionSyntax argLhs, ExpressionSyntax argRhs, ExpressionSyntax argComparison)
    {
        var isBeforeMethodIdentifier = SyntaxFactory.IdentifierName(ReplacementCall);
        var simpleMemberAccessExpression = SyntaxFactory.MemberAccessExpression(
            SyntaxKind.SimpleMemberAccessExpression,
            argLhs,
            SyntaxFactory.Token(SyntaxKind.DotToken),
            isBeforeMethodIdentifier);
        var invocationExpression = SyntaxFactory.InvocationExpression(simpleMemberAccessExpression)
            .AddArgumentListArguments(
                SyntaxFactory.Argument(argRhs),
                SyntaxFactory.Argument(argComparison));
        return invocationExpression;
    }

    protected Document ReplaceCode(
        CompilationUnitSyntax rootNode,
        Document document,
        BinaryExpressionSyntax binaryExpression)
    {
        var left = (InvocationExpressionSyntax)binaryExpression.Left;
        var argLhs = left.ArgumentList.Arguments[0].Expression;
        var argRhs = left.ArgumentList.Arguments[1].Expression;
        var argComparison = left.ArgumentList.Arguments[2].Expression;

        var newSyntax = BuildReplacementExpression(argLhs, argRhs, argComparison);
        rootNode = rootNode.ReplaceNode(binaryExpression, newSyntax);

        rootNode = rootNode.UseStravaigExtensionsCore();
        return document.WithSyntaxRoot(rootNode);
    }
}