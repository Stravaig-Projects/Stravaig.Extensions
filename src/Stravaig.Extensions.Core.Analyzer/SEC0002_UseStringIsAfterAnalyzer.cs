using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Stravaig.Extensions.Core.Analyzer;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class SEC0002_UseStringIsAfterAnalyzer : DiagnosticAnalyzer
{
    public const string BeforeDiagnosticId = "SEC0002";
    public const string BeforeOrEqualDiagnosticId = "SEC0003";
    public const string AfterOrEqualDiagnosticId = "SEC0004";
    public const string AfterDiagnosticId = "SEC0005";

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
        string lhs = null, rhs = null;
        if (string.Compare(lhs, rhs, StringComparison.OrdinalIgnoreCase) > 0) { }
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
        switch (context.Node.Kind())
        {
            case SyntaxKind.GreaterThanExpression:
            case SyntaxKind.GreaterThanOrEqualExpression:
            case SyntaxKind.LessThanExpression:
            case SyntaxKind.LessThanOrEqualExpression:
                break;
            default:
                return;
        }
        string lhsText = "", rhsText = "", comparisonText = "";
        var diagnostic = Diagnostic.Create(BeforeRule, context.Node.GetLocation(), lhsText, rhsText, comparisonText);
        context.ReportDiagnostic(diagnostic);
    }

    private static readonly ImmutableArray<DiagnosticDescriptor> CachedSupportedDiagnostics =
        ImmutableArray.Create(BeforeRule);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => CachedSupportedDiagnostics;
}