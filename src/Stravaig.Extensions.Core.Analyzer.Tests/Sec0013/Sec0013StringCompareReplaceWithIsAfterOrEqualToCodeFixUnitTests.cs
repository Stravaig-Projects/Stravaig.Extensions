using System.Threading.Tasks;
using NUnit.Framework;
using static Stravaig.Extensions.Core.Analyzer.Tests.CSharpCodeFixVerifier<
    Stravaig.Extensions.Core.Analyzer.Sec001XReplaceStringCompareAnalyzer,
    Stravaig.Extensions.Core.Analyzer.Sec0013ReplaceStringCompareWithAfterOrEqualAnalyzerCodeFix>;

namespace Stravaig.Extensions.Core.Analyzer.Tests.Sec0013;

public class Sec0013StringCompareReplaceWithIsAfterOrEqualToCodeFixUnitTests
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
        return ({|SEC0013:string.Compare(a, b, StringComparison.OrdinalIgnoreCase) >= 0|});
    }
}";

        const string fix = @"using Stravaig.Extensions.Core;
using System;

namespace MyNamespace;
class MyClass
{
    public bool Check(string a, string b)
    {
        return (a.IsAfterOrEqualTo(b, StringComparison.OrdinalIgnoreCase));
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
    public bool Check(int a, int b)
    {
        return ({|SEC0013:string.Compare(a.ToString(), b.ToString(), StringComparison.OrdinalIgnoreCase) >= 0|});
    }
}";

        const string fix = @"using Stravaig.Extensions.Core;
using System;

namespace MyNamespace;
class MyClass
{
    public bool Check(int a, int b)
    {
        return (a.ToString().IsAfterOrEqualTo(b.ToString(), StringComparison.OrdinalIgnoreCase));
    }
}";
        await VerifyCodeFixAsync(test, fix);
    }
}