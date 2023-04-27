using System.Collections.Immutable;
using System.Composition;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;

namespace Stravaig.Extensions.Core.Analyzer;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(SEC0014_ReplaceStringCompareWithAfterAnalyzerCodeFix)), Shared]
public class SEC0014_ReplaceStringCompareWithAfterAnalyzerCodeFix : SEC001x_ReplaceStringCompareAnalyzerCodeFixBase
{
    private static readonly ImmutableArray<string> CachedFixableDiagnostics =
        ImmutableArray.Create(Sec001XReplaceStringCompareAnalyzer.AfterDiagnosticId);
    public sealed override ImmutableArray<string> FixableDiagnosticIds => CachedFixableDiagnostics;
    protected override string Title => CodeFixResources.SEC0014CodeFixTitle;
    protected override string ReplacementCall => "IsAfter";
    protected override string EquivalenceKey => nameof(CodeFixResources.SEC0014CodeFixTitle);
}