using System.Threading.Tasks;
using NUnit.Framework;
using static Stravaig.Extensions.Core.Analyzer.Tests.CSharpAnalyzerVerifier<Stravaig.Extensions.Core.Analyzer.Sec001XReplaceStringCompareAnalyzer>;

namespace Stravaig.Extensions.Core.Analyzer.Tests.Sec0013;

[TestFixture]
public partial class Sec0013ReplaceStringCompareAnalyzerTests
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
        return (string.Compare(""lhs"", ""rhs"", true) >= 0);
    }
}";
        await VerifyAnalyzerAsync(test);
    }
}