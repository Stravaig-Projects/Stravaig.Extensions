using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using NUnit.Framework;

namespace Stravaig.Extensions.Core.Analyzer.Test;

[TestFixture]
public class Sec0001StringIsNullOrWhiteSpaceAnalyzerUnitTest
{
    //No diagnostics expected to show up
    [Test]
    public async Task EmptyCodeBlockShouldProduceNoFixes()
    {
        const string test = @"";

        await CSharpAnalyzerVerifier<Sec0001UseStringHasContentAnalyzer>.VerifyAnalyzerAsync(test);
    }

    [Test]
    public async Task NotStringIsNullOrWhiteSpaceStringArgShouldActivateDiagnostic()
    {
        const string test = @"using System;
namespace MyNamespace;
class MyClass
{
    public bool MyMethod(string someString)
    {
        return (!string.IsNullOrWhiteSpace(someString));
    }
}";

        var expected = CSharpAnalyzerVerifier<Sec0001UseStringHasContentAnalyzer>.Diagnostic("SEC0001")
            .WithLocation(7, 17)
            .WithArguments("someString");
        await CSharpAnalyzerVerifier<Sec0001UseStringHasContentAnalyzer>.VerifyAnalyzerAsync(test, expected);
    }

    [Test]
    public async Task StringIsNullOrWhiteSpaceStringArgEqualsFalseShouldActivateDiagnostic()
    {
        const string test = @"using System;
namespace MyNamespace;
class MyClass
{
    public bool MyMethod(string someString)
    {
        return (string.IsNullOrWhiteSpace(someString) == false);
    }
}";

        var expected = CSharpAnalyzerVerifier<Sec0001UseStringHasContentAnalyzer>.Diagnostic("SEC0001")
            .WithLocation(7, 17)
            .WithArguments("someString");
        await CSharpAnalyzerVerifier<Sec0001UseStringHasContentAnalyzer>.VerifyAnalyzerAsync(test, expected);
    }

    [Test]
    public async Task StringIsNullOrWhiteSpaceStringExpressionEqualsFalseShouldActivateDiagnostic()
    {
        const string test = @"using System;
namespace MyNamespace;
class MyClass
{
    public bool MyMethod(int aNumber)
    {
        return (string.IsNullOrWhiteSpace(aNumber.ToString()) == false);
    }
}";

        var expected = CSharpAnalyzerVerifier<Sec0001UseStringHasContentAnalyzer>.Diagnostic("SEC0001")
            .WithLocation(7, 17)
            .WithArguments("aNumber.ToString()");
        await CSharpAnalyzerVerifier<Sec0001UseStringHasContentAnalyzer>.VerifyAnalyzerAsync(test, expected);
    }
        
    [Test]
    public async Task NotStringIsNullOrWhiteSpaceStringExpressionShouldActivateDiagnostic()
    {
        const string test = @"using System;
namespace MyNamespace;
class MyClass
{
    public bool MyMethod(int aNumber)
    {
        return (!string.IsNullOrWhiteSpace(aNumber.ToString()));
    }
}";
 
        var expected = CSharpAnalyzerVerifier<Sec0001UseStringHasContentAnalyzer>.Diagnostic("SEC0001")
            .WithLocation(7, 17)
            .WithArguments("aNumber.ToString()");
        await CSharpAnalyzerVerifier<Sec0001UseStringHasContentAnalyzer>.VerifyAnalyzerAsync(test, expected);
    }

    [Test]
    public async Task NoArgumentShouldNotActivateDiagnostic()
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
        await CSharpAnalyzerVerifier<Sec0001UseStringHasContentAnalyzer>.VerifyAnalyzerAsync(test, expected);
    }

    [Test]
    public async Task InvertedCheckShouldNotActivateDiagnostic()
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
        await CSharpAnalyzerVerifier<Sec0001UseStringHasContentAnalyzer>.VerifyAnalyzerAsync(test);
    }

    [Test]
    public async Task InvertedEqualityCheckShouldNotActivateDiagnostic()
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
        await CSharpAnalyzerVerifier<Sec0001UseStringHasContentAnalyzer>.VerifyAnalyzerAsync(test);
    }

    [Test]
    public async Task NotOnStringClassShouldNotActivateDiagnostic()
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
        await CSharpAnalyzerVerifier<Sec0001UseStringHasContentAnalyzer>.VerifyAnalyzerAsync(test);
    }

    [Test]
    public async Task ThisMethodCallShouldNotActivateDiagnostic()
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
        await CSharpAnalyzerVerifier<Sec0001UseStringHasContentAnalyzer>.VerifyAnalyzerAsync(test);
    }
}