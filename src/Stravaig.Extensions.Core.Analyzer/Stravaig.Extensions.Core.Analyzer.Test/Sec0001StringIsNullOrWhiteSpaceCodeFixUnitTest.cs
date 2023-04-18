using NUnit.Framework;
using System.Threading.Tasks;
using VerifyCS = Stravaig.Extensions.Core.Analyzer.Test.CSharpCodeFixVerifier<
    Stravaig.Extensions.Core.Analyzer.Sec0001UseStringHasContentAnalyzer,
    Stravaig.Extensions.Core.Analyzer.Sec0001UseStringHasContentAnalyzerCodeFixProvider>;

namespace Stravaig.Extensions.Core.Analyzer.Test;

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

        await VerifyCS.VerifyCodeFixAsync(test, fix);
    }

//    [Test]
//    public async Task StringIsNullOrWhiteSpaceStringArgEqualsFalseShouldActivateDiagnostic()
//    {
//        const string test = @"using System;
//namespace MyNamespace;
//class MyClass
//{
//    public bool MyMethod(string someString)
//    {
//        return (string.IsNullOrWhiteSpace(someString) == false);
//    }
//}";

//        var expected = VerifyCS
//            .Diagnostic("SEC0001")
//            .WithLocation(7, 17)
//            .WithArguments("someString");
//        await VerifyCS.VerifyAnalyzerAsync(test, expected);
//    }

//    [Test]
//    public async Task StringIsNullOrWhiteSpaceStringExpressionEqualsFalseShouldActivateDiagnostic()
//    {
//        const string test = @"using System;
//namespace MyNamespace;
//class MyClass
//{
//    public bool MyMethod(int aNumber)
//    {
//        return (string.IsNullOrWhiteSpace(aNumber.ToString()) == false);
//    }
//}";

//        var expected = VerifyCS
//            .Diagnostic("SEC0001")
//            .WithLocation(7, 17)
//            .WithArguments("aNumber.ToString()");
//        await VerifyCS.VerifyAnalyzerAsync(test, expected);
//    }

//    [Test]
//    public async Task NotStringIsNullOrWhiteSpaceStringExpressionShouldActivateDiagnostic()
//    {
//        const string test = @"using System;
//namespace MyNamespace;
//class MyClass
//{
//    public bool MyMethod(int aNumber)
//    {
//        return (!string.IsNullOrWhiteSpace(aNumber.ToString()));
//    }
//}";

//        var expected = VerifyCS
//            .Diagnostic("SEC0001")
//            .WithLocation(7, 17)
//            .WithArguments("aNumber.ToString()");
//        await VerifyCS.VerifyAnalyzerAsync(test, expected);
//    }
}