using NUnit.Framework;
using System.Threading.Tasks;
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
    }
}
