using NUnit.Framework;
using System.Threading.Tasks;

using static Stravaig.Extensions.Core.Analyzer.Test.CSharpAnalyzerVerifier<Stravaig.Extensions.Core.Analyzer.SEC001x_ReplaceStringCompareAnalyzer>;

namespace Stravaig.Extensions.Core.Analyzer.Tests.Sec0011;

[TestFixture]
public partial class Sec0011ReplaceStringCompareAnalyzerTests
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