using NUnit.Framework;
using System.Threading.Tasks;
using static Stravaig.Extensions.Core.Analyzer.Test.CSharpCodeFixVerifier<
    Stravaig.Extensions.Core.Analyzer.Sec0001UseStringHasContentAnalyzer,
    Stravaig.Extensions.Core.Analyzer.Sec0001UseStringHasContentAnalyzerCodeFixProvider>;

namespace Stravaig.Extensions.Core.Analyzer.Test.Sec0001;

[TestFixture]
public class Sec0001StringIsNullOrWhiteSpaceCodeFixUnitTest
{
    [Test]
    public async Task NotStringIsNullOrWhiteSpaceStringArg()
    {
        const string test = @"using System;
namespace MyNamespace;
class MyClass
{
    public bool MyMethod(string someString)
    {
        return ([|!string.IsNullOrWhiteSpace(someString)|]);
    }
}";

        const string fix = @"using Stravaig.Extensions.Core;
using System;
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
        const string test = @"using System;
namespace MyNamespace;
class MyClass
{
    public bool MyMethod(int aNumber)
    {
        return ([|!string.IsNullOrWhiteSpace(aNumber.ToString())|]);
    }
}";

        const string fix = @"using Stravaig.Extensions.Core;
using System;
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
    public async Task NoUsingDeclarationsNotStringIsNullOrWhiteSpaceStringArg()
    {
        const string test = @"namespace MyNamespace;
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
    public async Task UsingDeclarationExistsButNotInAlphabeticalOrderNotStringIsNullOrWhiteSpaceStringArg()
    {
        const string test = @"using System;
using Stravaig.Extensions.Core;

namespace MyNamespace;
class MyClass
{
    public bool MyMethod(string someString)
    {
        return ([|!string.IsNullOrWhiteSpace(someString)|]);
    }
}";

        const string fix = @"using System;
using Stravaig.Extensions.Core;

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
    public async Task UsingDeclarationsInsideNamespaceNotStringIsNullOrWhiteSpaceStringArg()
    {
        const string test = @"namespace MyNamespace
{
    using System;

    class MyClass
    {
        public bool MyMethod(string someString)
        {
            return ([|!string.IsNullOrWhiteSpace(someString)|]);
        }
    }
}";

        const string fix = @"namespace MyNamespace
{
    using Stravaig.Extensions.Core;
    using System;

    class MyClass
    {
        public bool MyMethod(string someString)
        {
            return (someString.HasContent());
        }
    }
}";

        await VerifyCodeFixAsync(test, fix);
    }

    [Test]
    public async Task NoNamespaceUsingDeclarationPutAtTopOfFile()
    {
        const string test = @"class MyClass
{
    public bool MyMethod(string someString)
    {
        return ([|!string.IsNullOrWhiteSpace(someString)|]);
    }
}";

        const string fix = @"using Stravaig.Extensions.Core;

class MyClass
{
    public bool MyMethod(string someString)
    {
        return (someString.HasContent());
    }
}";

        await VerifyCodeFixAsync(test, fix);
    }
}