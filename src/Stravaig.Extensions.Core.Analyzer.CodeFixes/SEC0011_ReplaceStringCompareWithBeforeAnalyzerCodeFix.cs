using System.Collections.Immutable;
using System.Composition;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;

namespace Stravaig.Extensions.Core.Analyzer;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(SEC0011_ReplaceStringCompareWithBeforeAnalyzerCodeFix)), Shared]
public class SEC0011_ReplaceStringCompareWithBeforeAnalyzerCodeFix : SEC001x_ReplaceStringCompareAnalyzerCodeFixBase
{
    private static readonly ImmutableArray<string> CachedFixableDiagnostics =
        ImmutableArray.Create(SEC001x_ReplaceStringCompareAnalyzer.BeforeDiagnosticId);
    public sealed override ImmutableArray<string> FixableDiagnosticIds => CachedFixableDiagnostics;
    protected override string Title => CodeFixResources.SEC0011CodeFixTitle;
    protected override string ReplacementCall => "IsBefore";
    protected override string EquivalenceKey => nameof(CodeFixResources.SEC0011CodeFixTitle);
}