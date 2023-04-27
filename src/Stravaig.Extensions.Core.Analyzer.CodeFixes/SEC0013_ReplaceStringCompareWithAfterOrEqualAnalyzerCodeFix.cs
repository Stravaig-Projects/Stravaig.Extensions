using System.Collections.Immutable;
using System.Composition;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;

namespace Stravaig.Extensions.Core.Analyzer;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(SEC0013_ReplaceStringCompareWithAfterOrEqualAnalyzerCodeFix)), Shared]
public class SEC0013_ReplaceStringCompareWithAfterOrEqualAnalyzerCodeFix : SEC001x_ReplaceStringCompareAnalyzerCodeFixBase
{
    private static readonly ImmutableArray<string> CachedFixableDiagnostics =
        ImmutableArray.Create(Sec001XReplaceStringCompareAnalyzer.AfterOrEqualDiagnosticId);
    public sealed override ImmutableArray<string> FixableDiagnosticIds => CachedFixableDiagnostics;
    protected override string Title => CodeFixResources.SEC0013CodeFixTitle;
    protected override string ReplacementCall => "IsAfterOrEqualTo";
    protected override string EquivalenceKey => nameof(CodeFixResources.SEC0013CodeFixTitle);
}