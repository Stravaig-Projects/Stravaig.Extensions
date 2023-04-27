using System;
using System.Collections.Immutable;
using System.Linq;
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
            Localise.Resource(nameof(Resources.SEC0011_AnalyzerTitle)),
            Localise.Resource(nameof(Resources.SEC0011_MessageFormat)),
            Category,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: Localise.Resource(nameof(Resources.SEC0011_Description)));

    private static readonly DiagnosticDescriptor BeforeOrEqualRule =
        new(BeforeOrEqualDiagnosticId,
            Localise.Resource(nameof(Resources.SEC0012_AnalyzerTitle)),
            Localise.Resource(nameof(Resources.SEC0012_MessageFormat)),
            Category,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: Localise.Resource(nameof(Resources.SEC0012_Description)));

    private static readonly DiagnosticDescriptor AfterOrEqualRule =
        new(AfterOrEqualDiagnosticId,
            Localise.Resource(nameof(Resources.SEC0013_AnalyzerTitle)),
            Localise.Resource(nameof(Resources.SEC0013_MessageFormat)),
            Category,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: Localise.Resource(nameof(Resources.SEC0013_Description)));

    private static readonly DiagnosticDescriptor AfterRule =
        new(AfterDiagnosticId,
            Localise.Resource(nameof(Resources.SEC0014_AnalyzerTitle)),
            Localise.Resource(nameof(Resources.SEC0014_MessageFormat)),
            Category,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: Localise.Resource(nameof(Resources.SEC0014_Description)));

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


        DiagnosticDescriptor rule = null;
        switch (binaryExpression.Kind())
        {
            case SyntaxKind.GreaterThanExpression:
                rule = AfterRule;
                break;
            case SyntaxKind.GreaterThanOrEqualExpression:
                rule = AfterOrEqualRule;
                break;
            case SyntaxKind.LessThanExpression:
                rule = BeforeRule;
                break;
            case SyntaxKind.LessThanOrEqualExpression:
                rule = BeforeOrEqualRule;
                break;
        }

        if (rule == null)
            return;

        string lhsText = argLhs.GetText().ToString(),
            rhsText = argRhs.GetText().ToString(),
            comparisonText = argComparison.GetText().ToString();
        var diagnostic = Diagnostic.Create(rule, context.Node.GetLocation(), lhsText, rhsText, comparisonText);
        context.ReportDiagnostic(diagnostic);
    }

    private static readonly ImmutableArray<DiagnosticDescriptor> CachedSupportedDiagnostics =
        ImmutableArray.Create(BeforeRule, BeforeOrEqualRule, AfterOrEqualRule, AfterRule);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => CachedSupportedDiagnostics;
}