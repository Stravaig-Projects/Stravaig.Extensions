using System.Threading.Tasks;
using NUnit.Framework;
using static Stravaig.Extensions.Core.Analyzer.Tests.CSharpAnalyzerVerifier<Stravaig.Extensions.Core.Analyzer.Sec001XReplaceStringCompareAnalyzer>;

namespace Stravaig.Extensions.Core.Analyzer.Tests.Sec0014;

[TestFixture]
public partial class Sec0014ReplaceStringCompareAnalyzerTests
{
    [Test]
    public async Task CompareGreaterThanWithStringLiteralsAndComparison_Matches()
    {
        const string test = @"using System;

namespace MyNamespace;
class MyClass
{
    public bool MyMethod()
    {
        return (string.Compare(""lhs"", ""rhs"", StringComparison.OrdinalIgnoreCase) > 0);
    }
}";
        var expected = Diagnostic("SEC0014")
            .WithMessageFormat(Localise.Resource("SEC0014_MessageFormat"))
            .WithLocation(8, 17)
            .WithArguments("\"lhs\"", "\"rhs\"", "StringComparison.OrdinalIgnoreCase");
        await VerifyAnalyzerAsync(test, expected);
    }
}