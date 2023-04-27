using System.Threading.Tasks;
using NUnit.Framework;
using static Stravaig.Extensions.Core.Analyzer.Tests.CSharpCodeFixVerifier<
    Stravaig.Extensions.Core.Analyzer.Sec001XReplaceStringCompareAnalyzer,
    Stravaig.Extensions.Core.Analyzer.SEC0012_ReplaceStringCompareWithBeforeOrEqualAnalyzerCodeFix>;

namespace Stravaig.Extensions.Core.Analyzer.Tests.Sec0012;

public class Sec0012StringCompareReplaceWithIsBeforeOrEqualToCodeFixUnitTests
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
        return ({|SEC0012:string.Compare(a, b, StringComparison.OrdinalIgnoreCase) <= 0|});
    }
}";

        const string fix = @"using Stravaig.Extensions.Core;
using System;

namespace MyNamespace;
class MyClass
{
    public bool Check(string a, string b)
    {
        return (a.IsBeforeOrEqualTo(b, StringComparison.OrdinalIgnoreCase));
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
        return ({|SEC0012:string.Compare(a.ToString(), b.ToString(), StringComparison.OrdinalIgnoreCase) <= 0|});
    }
}";

        const string fix = @"using Stravaig.Extensions.Core;
using System;

namespace MyNamespace;
class MyClass
{
    public bool IsBefore(int a, int b)
    {
        return (a.ToString().IsBeforeOrEqualTo(b.ToString(), StringComparison.OrdinalIgnoreCase));
    }
}";
        await VerifyCodeFixAsync(test, fix);
    }
}