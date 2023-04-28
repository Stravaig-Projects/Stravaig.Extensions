using System.Collections.Immutable;
using System.Composition;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;

namespace Stravaig.Extensions.Core.Analyzer;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(Sec0011ReplaceStringCompareWithBeforeAnalyzerCodeFix)), Shared]
public class Sec0011ReplaceStringCompareWithBeforeAnalyzerCodeFix : Sec001XReplaceStringCompareAnalyzerCodeFixBase
{
    private static readonly ImmutableArray<string> CachedFixableDiagnostics =
        ImmutableArray.Create(Sec001XReplaceStringCompareAnalyzer.BeforeDiagnosticId);
    public sealed override ImmutableArray<string> FixableDiagnosticIds => CachedFixableDiagnostics;
    protected override string Title => CodeFixResources.SEC0011CodeFixTitle;
    protected override string ReplacementCall => "IsBefore";
    protected override string EquivalenceKey => nameof(CodeFixResources.SEC0011CodeFixTitle);
}