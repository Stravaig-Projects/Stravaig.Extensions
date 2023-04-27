using System.Threading.Tasks;
using NUnit.Framework;
using static Stravaig.Extensions.Core.Analyzer.Tests.CSharpCodeFixVerifier<
    Stravaig.Extensions.Core.Analyzer.Sec001XReplaceStringCompareAnalyzer,
    Stravaig.Extensions.Core.Analyzer.SEC0011_ReplaceStringCompareWithBeforeAnalyzerCodeFix>;

namespace Stravaig.Extensions.Core.Analyzer.Tests.Sec0011;

public class Sec0011StringCompareReplaceWithIsBeforeCodeFixUnitTests
{
    [Test]
    public async Task HappyPathWithStringVariableArgs()
    {
        const string test = @"using System;

namespace MyNamespace;
class MyClass
{
    public bool Check(string a, string b)
    {
        return ({|SEC0011:string.Compare(a, b, StringComparison.OrdinalIgnoreCase) < 0|});
    }
}";

        const string fix = @"using Stravaig.Extensions.Core;
using System;

namespace MyNamespace;
class MyClass
{
    public bool Check(string a, string b)
    {
        return (a.IsBefore(b, StringComparison.OrdinalIgnoreCase));
    }
}";
        await VerifyCodeFixAsync(test, fix);
    }

    [Test]
    public async Task HappyPathWithStringExpressionArgs()
    {
        const string test = @"using System;

namespace MyNamespace;
class MyClass
{
    public bool IsBefore(int a, int b)
    {
        return ({|SEC0011:string.Compare(a.ToString(), b.ToString(), StringComparison.OrdinalIgnoreCase) < 0|});
    }
}";

        const string fix = @"using Stravaig.Extensions.Core;
using System;

namespace MyNamespace;
class MyClass
{
    public bool IsBefore(int a, int b)
    {
        return (a.ToString().IsBefore(b.ToString(), StringComparison.OrdinalIgnoreCase));
    }
}";
        await VerifyCodeFixAsync(test, fix);
    }
}