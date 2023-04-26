using System;
using NUnit.Framework;
using System.Threading.Tasks;

using static Stravaig.Extensions.Core.Analyzer.Test.CSharpAnalyzerVerifier<Stravaig.Extensions.Core.Analyzer.SEC001x_ReplaceStringCompareAnalyzer>;

namespace Stravaig.Extensions.Core.Analyzer.Tests.Sec0011;

[TestFixture]
public partial class Sec0011ReplaceStringCompareAnalyzerTests
{
    [Test]
    public async Task CompareLessThanWithStringLiteralsAndComparison_Matches()
    {
        const string test = @"using System;

namespace MyNamespace;
class MyClass
{
    public bool MyMethod()
    {
        return (string.Compare(""lhs"", ""rhs"", StringComparison.OrdinalIgnoreCase) < 0);
    }
}";
        var expected = Diagnostic("SEC0011")
            .WithMessageFormat(Localise.Resource("SEC0011_MessageFormat"))
            .WithLocation(8, 17)
            .WithArguments("\"lhs\"", "\"rhs\"", "StringComparison.OrdinalIgnoreCase");
        await VerifyAnalyzerAsync(test, expected);
    }

    [Test]
    public async Task CompareLessThanOrEqualWithStringLiteralsAndComparison_Matches()
    {
        const string test = @"using System;

namespace MyNamespace;
class MyClass
{
    public bool MyMethod()
    {
        return (string.Compare(""lhs"", ""rhs"", StringComparison.OrdinalIgnoreCase) <= 0);
    }
}";
        var expected = Diagnostic("SEC0012")
            .WithMessageFormat(Localise.Resource("SEC0012_MessageFormat"))
            .WithLocation(8, 17)
            .WithArguments("\"lhs\"", "\"rhs\"", "StringComparison.OrdinalIgnoreCase");
        await VerifyAnalyzerAsync(test, expected);
    }

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