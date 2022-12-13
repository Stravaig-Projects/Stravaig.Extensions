using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace Stravaig.Extensions.Core.Tests;

[TestFixture]
public class IEnumerableOfKeyValuePairExtensions_ToDictionaryTests
{
    private KeyValuePair<string, object>[] StandardSet1 = new KeyValuePair<string, object>[]
    {
        new("a", 1),
        new("b", true),
        new("c", 123.45M)
    };

    [Test]
    public void ParameterlessOverloadCreatesDictionary()
    {
        var dictionary = StandardSet1.ToDictionary();
        
        dictionary["a"].ShouldBe(1);
        dictionary["b"].ShouldBe(true);
        dictionary["c"].ShouldBe(123.45M);

        dictionary.ShouldBeOfType<Dictionary<string, object>>();
    }
    
    [Test]
    public void EqualityComparerOverloadCreatesDictionaryWithComparer()
    {
        var dictionary = StandardSet1.ToDictionary(StringComparer.OrdinalIgnoreCase);
        
        dictionary["A"].ShouldBe(1);
        dictionary["b"].ShouldBe(true);
        dictionary["C"].ShouldBe(123.45M);

        dictionary.ShouldBeOfType<Dictionary<string, object>>();
    }
}