using NUnit.Framework;
using System.Threading.Tasks;
using static Stravaig.Extensions.Core.Analyzer.Test.CSharpCodeFixVerifier<
    Stravaig.Extensions.Core.Analyzer.Sec0001UseStringHasContentAnalyzer,
    Stravaig.Extensions.Core.Analyzer.Sec0001UseStringHasContentAnalyzerCodeFixProvider>;

namespace Stravaig.Extensions.Core.Analyzer.Test.Sec0001;

[TestFixture]
public partial class Sec0001StringIsNullOrWhiteSpaceCodeFixUnitTest
{
    [Test]
    public async Task NoUsingDeclarationsWithoutNamespace()
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

    [Test]
    public async Task UsingDeclarationExistsWithoutNamespace()
    {
        const string test = @"using System;
using Stravaig.Extensions.Core;

class MyClass
{
    public bool MyMethod(string someString)
    {
        return ([|!string.IsNullOrWhiteSpace(someString)|]);
    }
}";

        const string fix = @"using System;
using Stravaig.Extensions.Core;

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
    public async Task UsingDeclarationMissingWithOthersWithoutNamespace()
    {
        const string test = @"using System;

class MyClass
{
    public bool MyMethod(string someString)
    {
        return ([|!string.IsNullOrWhiteSpace(someString)|]);
    }
}";

        const string fix = @"using Stravaig.Extensions.Core;
using System;

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