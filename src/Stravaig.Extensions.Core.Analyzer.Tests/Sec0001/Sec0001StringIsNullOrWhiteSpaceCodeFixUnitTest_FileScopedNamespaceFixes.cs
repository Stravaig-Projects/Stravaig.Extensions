﻿using NUnit.Framework;
using System.Threading.Tasks;
using static Stravaig.Extensions.Core.Analyzer.Test.CSharpCodeFixVerifier<
    Stravaig.Extensions.Core.Analyzer.SEC0001_UseStringHasContentAnalyzer,
    Stravaig.Extensions.Core.Analyzer.SEC0001_UseStringHasContentAnalyzerCodeFixProvider>;

namespace Stravaig.Extensions.Core.Analyzer.Test.Sec0001;

[TestFixture]
public partial class Sec0001StringIsNullOrWhiteSpaceCodeFixUnitTest
{
    [Test]
    public async Task NoUsingDeclarationsWithFileScopedNamespace()
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
    public async Task UsingDeclarationExistsBeforeTheFileScopedNamespace()
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
    public async Task UsingDeclarationExistsAfterTheFileScopedNamespace()
    {
        const string test = @"namespace MyNamespace;

using System;
using Stravaig.Extensions.Core;

class MyClass
{
    public bool MyMethod(string someString)
    {
        return ([|!string.IsNullOrWhiteSpace(someString)|]);
    }
}";

        const string fix = @"namespace MyNamespace;

using System;
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
    public async Task UsingDeclarationMissingWithOthersBeforeTheFileScopedNamespace()
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
    public async Task UsingDeclarationMissingWithOthersAfterTheFileScopedNamespace()
    {
        const string test = @"namespace MyNamespace;

using System;

class MyClass
{
    public bool MyMethod(string someString)
    {
        return ([|!string.IsNullOrWhiteSpace(someString)|]);
    }
}";

        const string fix = @"namespace MyNamespace;

using Stravaig.Extensions.Core;
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