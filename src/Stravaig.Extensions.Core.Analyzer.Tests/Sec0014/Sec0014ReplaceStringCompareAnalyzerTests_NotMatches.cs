using System.Threading.Tasks;
using NUnit.Framework;
using static Stravaig.Extensions.Core.Analyzer.Tests.CSharpAnalyzerVerifier<Stravaig.Extensions.Core.Analyzer.SEC001x_ReplaceStringCompareAnalyzer>;

namespace Stravaig.Extensions.Core.Analyzer.Tests.Sec0014;

[TestFixture]
public partial class Sec0014ReplaceStringCompareAnalyzerTests
{
    [Test]
    public async Task CompareWithStringLiteralsAndBoolean_NotMatches()
    {
        // This is not a recognised overload.
        const string test = @"namespace MyNamespace;
class MyClass
{
    public bool MyMethod()
    {
        return (string.Compare(""lhs"", ""rhs"", true) > 0);
    }
}";
        await VerifyAnalyzerAsync(test);
    }
}