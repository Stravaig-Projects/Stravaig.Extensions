﻿using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using NUnit.Framework;
using static Stravaig.Extensions.Core.Analyzer.Test.CSharpAnalyzerVerifier<Stravaig.Extensions.Core.Analyzer.Sec0001UseStringHasContentAnalyzer>;

namespace Stravaig.Extensions.Core.Analyzer.Test.Sec0001;

[TestFixture]
public partial class Sec0001StringIsNullOrWhiteSpaceAnalyzerUnitTest
{
    [Test]
    public async Task NotStringIsNullOrWhiteSpaceStringArg_Matches()
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

        var expected = Diagnostic("SEC0001")
            .WithLocation(7, 17)
            .WithArguments("someString");
        await VerifyAnalyzerAsync(test, expected);
    }

    [Test]
    public async Task StringIsNullOrWhiteSpaceStringArgEqualsFalse_Matches()
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

        var expected = Diagnostic("SEC0001")
            .WithLocation(7, 17)
            .WithArguments("someString");
        await VerifyAnalyzerAsync(test, expected);
    }

    [Test]
    public async Task StringIsNullOrWhiteSpaceStringExpressionEqualsFalse_Matches()
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

        var expected = Diagnostic("SEC0001")
            .WithLocation(7, 17)
            .WithArguments("aNumber.ToString()");
        await VerifyAnalyzerAsync(test, expected);
    }

    [Test]
    public async Task NotStringIsNullOrWhiteSpaceStringExpression_Matches()
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

        var expected = Diagnostic("SEC0001")
            .WithLocation(7, 17)
            .WithArguments("aNumber.ToString()");
        await VerifyAnalyzerAsync(test, expected);
    }
}