using System.Threading.Tasks;
using NUnit.Framework;
using static Stravaig.Extensions.Core.Analyzer.Tests.CSharpCodeFixVerifier<
    Stravaig.Extensions.Core.Analyzer.SEC001x_ReplaceStringCompareAnalyzer,
    Stravaig.Extensions.Core.Analyzer.SEC0014_ReplaceStringCompareWithAfterAnalyzerCodeFix>;

namespace Stravaig.Extensions.Core.Analyzer.Tests.Sec0014;

public class Sec0014StringCompareReplaceWithIsAfterOrEqualToCodeFixUnitTests
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
        return ({|SEC0014:string.Compare(a, b, StringComparison.OrdinalIgnoreCase) > 0|});
    }
}";

        const string fix = @"using Stravaig.Extensions.Core;
using System;

namespace MyNamespace;
class MyClass
{
    public bool Check(string a, string b)
    {
        return (a.IsAfter(b, StringComparison.OrdinalIgnoreCase));
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
        return ({|SEC0014:string.Compare(a.ToString(), b.ToString(), StringComparison.OrdinalIgnoreCase) > 0|});
    }
}";

        const string fix = @"using Stravaig.Extensions.Core;
using System;

namespace MyNamespace;
class MyClass
{
    public bool Check(int a, int b)
    {
        return (a.ToString().IsAfter(b.ToString(), StringComparison.OrdinalIgnoreCase));
    }
}";
        await VerifyCodeFixAsync(test, fix);
    }
}