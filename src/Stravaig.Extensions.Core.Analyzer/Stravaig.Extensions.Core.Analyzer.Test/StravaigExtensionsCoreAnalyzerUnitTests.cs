using NUnit.Framework;
using System.Threading.Tasks;
using VerifyCS = Stravaig.Extensions.Core.Analyzer.Test.CSharpCodeFixVerifier<
    Stravaig.Extensions.Core.Analyzer.Sec0001UseStringHasContentAnalyzer,
    Stravaig.Extensions.Core.Analyzer.StravaigExtensionsCoreAnalyzerCodeFixProvider>;

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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
        class {|#0:TypeName|}
        {   
        }
    }";

            var fixtest = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
        class TYPENAME
        {   
        }
    }";

            var expected = VerifyCS
                .Diagnostic("SEC0001")
                .WithLocation(0)
                .WithArguments("TypeName");
            await VerifyCS.VerifyCodeFixAsync(test, expected, fixtest);
        }
    }
}
