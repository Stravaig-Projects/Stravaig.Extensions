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
            var test = @"";

            await VerifyCS.VerifyAnalyzerAsync(test);
        }

        //Diagnostic and CodeFix both triggered and checked for
        [Test]
        public async Task CodeBlockWithAnalyserTriggerShouldProduceFix()
        {
            var test = @"
    using System;

    namespace MyNamespace
    {
        class MyClass
        {
            public bool MyMethod(string someString)
            {
                return (!string.IsNullOrWhiteSpace(someString));
            }
        }
    }";

            var expected = VerifyCS
                .Diagnostic("SEC0001")
                .WithLocation(10, 25)
                .WithArguments("someString");
            await VerifyCS.VerifyAnalyzerAsync(test, expected);
        }
    }
}
