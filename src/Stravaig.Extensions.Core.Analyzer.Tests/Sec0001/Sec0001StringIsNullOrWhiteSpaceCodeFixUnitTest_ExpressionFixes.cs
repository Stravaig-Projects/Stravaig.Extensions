using NUnit.Framework;
using System.Threading.Tasks;
using static Stravaig.Extensions.Core.Analyzer.Test.CSharpCodeFixVerifier<
    Stravaig.Extensions.Core.Analyzer.SEC0001_UseStringHasContentAnalyzer,
    Stravaig.Extensions.Core.Analyzer.SEC0001_UseStringHasContentAnalyzerCodeFixProvider>;

namespace Stravaig.Extensions.Core.Analyzer.Test.Sec0001;

[TestFixture]
public partial class Sec0001StringIsNullOrWhiteSpaceCodeFixUnitTest
{
    [Test]
    public async Task NotStringIsNullOrWhiteSpaceStringArg()
    {
        const string test = @"using Stravaig.Extensions.Core;

namespace MyNamespace;
class MyClass
{
    public bool MyMethod(string someString)
    {
        return ([|!string.IsNullOrWhiteSpace(someString)|]);
    }
}";

        const string fix = @"using Stravaig.Extensions.Core;

namespace MyNamespace;
class MyClass
{
    public bool MyMethod(string someString)
    {
        return (someString.HasContent());
    }
}";

        await VerifyCodeFixAsync(test, fix);
    }

    [Test]
    public async Task NotStringIsNullOrWhiteSpaceStringArgAsExpression()
    {
        const string test = @"using Stravaig.Extensions.Core;

namespace MyNamespace;
class MyClass
{
    public bool MyMethod(int aNumber)
    {
        return ([|!string.IsNullOrWhiteSpace(aNumber.ToString())|]);
    }
}";

        const string fix = @"using Stravaig.Extensions.Core;

namespace MyNamespace;
class MyClass
{
    public bool MyMethod(int aNumber)
    {
        return (aNumber.ToString().HasContent());
    }
}";

        await VerifyCodeFixAsync(test, fix);
    }

    [Test]
    public async Task StringIsNullOrWhiteSpaceStringArgEqualsFalse()
    {
        const string test = @"using Stravaig.Extensions.Core;

namespace MyNamespace;
class MyClass
{
    public bool MyMethod(string someString)
    {
        return ([|string.IsNullOrWhiteSpace(someString) == false|]);
    }
}";

        const string fix = @"using Stravaig.Extensions.Core;

namespace MyNamespace;
class MyClass
{
    public bool MyMethod(string someString)
    {
        return (someString.HasContent());
    }
}";

        await VerifyCodeFixAsync(test, fix);
    }

    [Test]
    public async Task StringIsNullOrWhiteSpaceStringExpressionArgEqualsFalse()
    {
        const string test = @"using Stravaig.Extensions.Core;

namespace MyNamespace;
class MyClass
{
    public bool MyMethod(int aNumber)
    {
        return ([|string.IsNullOrWhiteSpace(aNumber.ToString()) == false|]);
    }
}";

        const string fix = @"using Stravaig.Extensions.Core;

namespace MyNamespace;
class MyClass
{
    public bool MyMethod(int aNumber)
    {
        return (aNumber.ToString().HasContent());
    }
}";

        await VerifyCodeFixAsync(test, fix);
    }

    [Test]
    public async Task FalseEqualsStringIsNullOrWhiteSpaceStringArg()
    {
        const string test = @"using Stravaig.Extensions.Core;

namespace MyNamespace;
class MyClass
{
    public bool MyMethod(string someString)
    {
        return ([|false == string.IsNullOrWhiteSpace(someString)|]);
    }
}";

        const string fix = @"using Stravaig.Extensions.Core;

namespace MyNamespace;
class MyClass
{
    public bool MyMethod(string someString)
    {
        return (someString.HasContent());
    }
}";

        await VerifyCodeFixAsync(test, fix);
    }

    [Test]
    public async Task FalseEqualsStringIsNullOrWhiteSpaceStringExpressionArg()
    {
        const string test = @"using Stravaig.Extensions.Core;

namespace MyNamespace;
class MyClass
{
    public bool MyMethod(int aNumber)
    {
        return ([|false == string.IsNullOrWhiteSpace(aNumber.ToString())|]);
    }
}";

        const string fix = @"using Stravaig.Extensions.Core;

namespace MyNamespace;
class MyClass
{
    public bool MyMethod(int aNumber)
    {
        return (aNumber.ToString().HasContent());
    }
}";

        await VerifyCodeFixAsync(test, fix);
    }
}