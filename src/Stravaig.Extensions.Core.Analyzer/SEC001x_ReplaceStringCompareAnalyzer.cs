using System;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Stravaig.Extensions.Core.Analyzer;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class SEC001x_ReplaceStringCompareAnalyzer : DiagnosticAnalyzer
{
    public const string BeforeDiagnosticId = "SEC0011";
    public const string BeforeOrEqualDiagnosticId = "SEC0012";
    public const string AfterOrEqualDiagnosticId = "SEC0013";
    public const string AfterDiagnosticId = "SEC0014";

    private const string Category = "Readability";

    private static readonly DiagnosticDescriptor BeforeRule =
        new(BeforeDiagnosticId,
            new LocalizableResourceString(nameof(Resources.SEC0002_AnalyzerTitle), Resources.ResourceManager, typeof(Resources)),
            new LocalizableResourceString(nameof(Resources.SEC0002_MessageFormat), Resources.ResourceManager, typeof(Resources)),
            Category,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: new LocalizableResourceString(nameof(Resources.SEC0002_Description), Resources.ResourceManager, typeof(Resources)));

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterSyntaxNodeAction(
            AnalyzeNode,
            SyntaxKind.GreaterThanExpression,
            SyntaxKind.GreaterThanOrEqualExpression,
            SyntaxKind.LessThanExpression,
            SyntaxKind.LessThanOrEqualExpression);
    }

    private void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        string lhs = null, rhs = null;
        if (string.Compare(lhs, rhs, StringComparison.OrdinalIgnoreCase) > 0) { }

        
        var binaryExpression = (BinaryExpressionSyntax) context.Node;
        if (!binaryExpression.Left.IsKind(SyntaxKind.InvocationExpression))
            return;
        if (!binaryExpression.Right.IsKind(SyntaxKind.NumericLiteralExpression))
            return;

        var left = (InvocationExpressionSyntax)binaryExpression.Left;
        var smaExpression = left.ChildNodes().OfType<MemberAccessExpressionSyntax>().FirstOrDefault();
        if (smaExpression == null || smaExpression.Name.Identifier.Text != nameof(string.Compare))
            return;

        if (left.ArgumentList.Arguments.Count != 3)
            return;
        
        // TODO: We need some semantic analysis to figure out that the arguments are of the right type.
        var semanticModel = context.SemanticModel;
        var argLhs = left.ArgumentList.Arguments[0];
        var argType = semanticModel.GetTypeInfo(argLhs.Expression);
        if (argType.Type?.Name != nameof(String))
            return;

        var argRhs = left.ArgumentList.Arguments[1];
        argType = semanticModel.GetTypeInfo(argRhs.Expression);
        if (argType.Type?.Name != nameof(String))
            return;

        var argComparison = left.ArgumentList.Arguments[2];
        argType = semanticModel.GetTypeInfo(argComparison.Expression);
        if (argType.Type?.Name != nameof(StringComparison))
            return;

        
        //left.ArgumentList.Arguments[0].

        switch (binaryExpression.Kind())
        {
            case SyntaxKind.GreaterThanExpression:
            case SyntaxKind.GreaterThanOrEqualExpression:
            case SyntaxKind.LessThanExpression:
            case SyntaxKind.LessThanOrEqualExpression:
                break;
            default:
                return;
        }
        string lhsText = argLhs.GetText().ToString(),
            rhsText = argRhs.GetText().ToString(),
            comparisonText = argComparison.GetText().ToString();
        var diagnostic = Diagnostic.Create(BeforeRule, context.Node.GetLocation(), lhsText, rhsText, comparisonText);
        context.ReportDiagnostic(diagnostic);
    }

    private static readonly ImmutableArray<DiagnosticDescriptor> CachedSupportedDiagnostics =
        ImmutableArray.Create(BeforeRule);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => CachedSupportedDiagnostics;
}