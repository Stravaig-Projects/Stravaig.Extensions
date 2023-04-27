using NUnit.Framework;
using System.Threading.Tasks;

using static Stravaig.Extensions.Core.Analyzer.Tests.CSharpAnalyzerVerifier<Stravaig.Extensions.Core.Analyzer.SEC001x_ReplaceStringCompareAnalyzer>;

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
    public async Task CompareLessThanWithStringVarsAndComparison_Matches()
    {
        const string test = @"using System;

namespace MyNamespace;
class MyClass
{
    public bool MyMethod(string lhs, string rhs)
    {
        return (string.Compare(lhs, rhs, StringComparison.OrdinalIgnoreCase) < 0);
    }
}";
        var expected = Diagnostic("SEC0011")
            .WithMessageFormat(Localise.Resource("SEC0011_MessageFormat"))
            .WithLocation(8, 17)
            .WithArguments("lhs", "rhs", "StringComparison.OrdinalIgnoreCase");
        await VerifyAnalyzerAsync(test, expected);
    }

    [Test]
    public async Task CompareLessThanWithStringExpressionAndComparison_Matches()
    {
        const string test = @"using System;

namespace MyNamespace;
class MyClass
{
    public bool MyMethod(int lhs, int rhs)
    {
        return (string.Compare(lhs.ToString(), rhs.ToString(), StringComparison.OrdinalIgnoreCase) < 0);
    }
}";
        var expected = Diagnostic("SEC0011")
            .WithMessageFormat(Localise.Resource("SEC0011_MessageFormat"))
            .WithLocation(8, 17)
            .WithArguments("lhs.ToString()", "rhs.ToString()", "StringComparison.OrdinalIgnoreCase");
        await VerifyAnalyzerAsync(test, expected);
    }

    [Test]
    public async Task CompareLessThanWithStringExpressionAndCastComparison_Matches()
    {
        const string test = @"using System;

namespace MyNamespace;
class MyClass
{
    public bool MyMethod(int lhs, int rhs)
    {
        const int ordinalIgnoreCase = 5;
        return (string.Compare(lhs.ToString(), rhs.ToString(), (StringComparison)ordinalIgnoreCase) < 0);
    }
}";
        var expected = Diagnostic("SEC0011")
            .WithMessageFormat(Localise.Resource("SEC0011_MessageFormat"))
            .WithLocation(9, 17)
            .WithArguments("lhs.ToString()", "rhs.ToString()", "(StringComparison)ordinalIgnoreCase");
        await VerifyAnalyzerAsync(test, expected);
    }
}