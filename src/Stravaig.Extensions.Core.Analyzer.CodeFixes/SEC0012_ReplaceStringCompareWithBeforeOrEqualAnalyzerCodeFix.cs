using System.Collections.Immutable;
using System.Composition;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;

namespace Stravaig.Extensions.Core.Analyzer;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(SEC0012_ReplaceStringCompareWithBeforeOrEqualAnalyzerCodeFix)), Shared]
public class SEC0012_ReplaceStringCompareWithBeforeOrEqualAnalyzerCodeFix : SEC001x_ReplaceStringCompareAnalyzerCodeFixBase
{
    private static readonly ImmutableArray<string> CachedFixableDiagnostics =
        ImmutableArray.Create(Sec001XReplaceStringCompareAnalyzer.BeforeOrEqualDiagnosticId);
    public sealed override ImmutableArray<string> FixableDiagnosticIds => CachedFixableDiagnostics;
    protected override string Title => CodeFixResources.SEC0012CodeFixTitle;
    protected override string ReplacementCall => "IsBeforeOrEqualTo";
    protected override string EquivalenceKey => nameof(CodeFixResources.SEC0012CodeFixTitle);
}