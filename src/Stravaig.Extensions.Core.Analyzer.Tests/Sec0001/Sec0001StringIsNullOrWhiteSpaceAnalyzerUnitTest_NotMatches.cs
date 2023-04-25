using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using NUnit.Framework;
using static Stravaig.Extensions.Core.Analyzer.Test.CSharpAnalyzerVerifier<Stravaig.Extensions.Core.Analyzer.SEC0001_UseStringHasContentAnalyzer>;

namespace Stravaig.Extensions.Core.Analyzer.Tests.Sec0001;

[TestFixture]
public partial class Sec0001StringIsNullOrWhiteSpaceAnalyzerUnitTest
{
    //No diagnostics expected to show up
    [Test]
    public async Task EmptyCodeBlock_NotMatches()
    {
        const string test = @"";

        await VerifyAnalyzerAsync(test);
    }
    [Test]
    public async Task NoArgument_OtherError()
    {
        const string test = @"using System;
namespace MyNamespace;
class MyClass
{
    public bool MyMethod(int aNumber)
    {
        return (!string.IsNullOrWhiteSpace(/* Missing argument here */));
    }
}";
        var expected = new DiagnosticResult("CS7036", DiagnosticSeverity.Error)
            .WithLocation(7, 25); // Missing argument
        await VerifyAnalyzerAsync(test, expected);
    }

    [Test]
    public async Task InvertedCheck_NotMatches()
    {
        const string test = @"using System;
namespace MyNamespace;
class MyClass
{
    public bool MyMethod(string someString)
    {
        return string.IsNullOrWhiteSpace(someString);
    }
}";
        await VerifyAnalyzerAsync(test);
    }

    [Test]
    public async Task InvertedEqualityCheck_NotMatches()
    {
        const string test = @"using System;
namespace MyNamespace;
class MyClass
{
    public bool MyMethod(string someString)
    {
        return string.IsNullOrWhiteSpace(someString) == true;
    }
}";
        await VerifyAnalyzerAsync(test);
    }

    [Test]
    public async Task NotOnStringClass_NotMatches()
    {
        const string test = @"using System;
namespace MyNamespace;
class MyClass
{
    public bool MyMethod(string someString)
    {
        return !MyClass.IsNullOrWhiteSpace(someString);
    }

    public static bool IsNullOrWhiteSpace(string stuff) => false;
}";
        await VerifyAnalyzerAsync(test);
    }

    [Test]
    public async Task ImplicitThisMethodCall_NotMatches()
    {
        const string test = @"using System;
namespace MyNamespace;
class MyClass
{
    public bool MyMethod(string someString)
    {
        return !IsNullOrWhiteSpace(someString);
    }

    public bool IsNullOrWhiteSpace(string stuff) => false;
}";
        await VerifyAnalyzerAsync(test);
    }

    [Test]
    public async Task ExplicitThisMethodCall_NotMatches()
    {
        const string test = @"using System;
namespace MyNamespace;
class MyClass
{
    public bool MyMethod(string someString)
    {
        return !this.IsNullOrWhiteSpace(someString);
    }

    public bool IsNullOrWhiteSpace(string stuff) => false;
}";
        await VerifyAnalyzerAsync(test);
    }
}