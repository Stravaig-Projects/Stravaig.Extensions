using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using VerifyCS = Stravaig.Extensions.Core.Analyzer.Test.CSharpAnalyzerVerifier<Stravaig.Extensions.Core.Analyzer.Sec0001UseStringHasContentAnalyzer>;

namespace Stravaig.Extensions.Core.Analyzer.Test
{
    public class StravaigExtensionsCoreAnalyzerUnitTest
    {
        //No diagnostics expected to show up
        [Test]
        public async Task EmptyCodeBlockShouldProduceNoFixes()
        {
            const string test = @"";

            await VerifyCS.VerifyAnalyzerAsync(test);
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

            var expected = VerifyCS
                .Diagnostic("SEC0001")
                .WithLocation(7, 17)
                .WithArguments("someString");
            await VerifyCS.VerifyAnalyzerAsync(test, expected);
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

            var expected = VerifyCS
                .Diagnostic("SEC0001")
                .WithLocation(7, 17)
                .WithArguments("someString");
            await VerifyCS.VerifyAnalyzerAsync(test, expected);
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

            var expected = VerifyCS
                .Diagnostic("SEC0001")
                .WithLocation(7, 17)
                .WithArguments("aNumber.ToString()");
            await VerifyCS.VerifyAnalyzerAsync(test, expected);
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
 
            var expected = VerifyCS
                .Diagnostic("SEC0001")
                .WithLocation(7, 17)
                .WithArguments("aNumber.ToString()");
            await VerifyCS.VerifyAnalyzerAsync(test, expected);
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
            await VerifyCS.VerifyAnalyzerAsync(test, expected);
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
            await VerifyCS.VerifyAnalyzerAsync(test);
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
            await VerifyCS.VerifyAnalyzerAsync(test);
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
            await VerifyCS.VerifyAnalyzerAsync(test);
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
            await VerifyCS.VerifyAnalyzerAsync(test);
        }
    }
}
