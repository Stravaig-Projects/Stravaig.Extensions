using System.Threading.Tasks;
using NUnit.Framework;
using static Stravaig.Extensions.Core.Analyzer.Tests.CSharpAnalyzerVerifier<Stravaig.Extensions.Core.Analyzer.SEC001x_ReplaceStringCompareAnalyzer>;

namespace Stravaig.Extensions.Core.Analyzer.Tests.Sec0013;

[TestFixture]
public partial class Sec0013ReplaceStringCompareAnalyzerTests
{
    [Test]
    public async Task CompareGreaterThanOrEqualWithStringLiteralsAndComparison_Matches()
    {
        const string test = @"using System;

namespace MyNamespace;
class MyClass
{
    public bool MyMethod()
    {
        return (string.Compare(""lhs"", ""rhs"", StringComparison.OrdinalIgnoreCase) >= 0);
    }
}";
        var expected = Diagnostic("SEC0013")
            .WithMessageFormat(Localise.Resource("SEC0013_MessageFormat"))
            .WithLocation(8, 17)
            .WithArguments("\"lhs\"", "\"rhs\"", "StringComparison.OrdinalIgnoreCase");
        await VerifyAnalyzerAsync(test, expected);
    }
}