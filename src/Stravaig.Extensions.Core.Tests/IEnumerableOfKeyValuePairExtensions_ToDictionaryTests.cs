using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace Stravaig.Extensions.Core.Tests;

[TestFixture]
// ReSharper disable once InconsistentNaming
public class IEnumerableOfKeyValuePairExtensions_ToDictionaryTests
{
    private readonly KeyValuePair<string, object>[]? _nullSet = null;
    private readonly KeyValuePair<string, object>[] _standardSet1 = 
    {
        new("a", 1),
        new("b", true),
        new("c", 123.45M)
    };

    [Test]
    public void ParameterlessOverloadCreatesDictionary()
    {
        var dictionary = _standardSet1.ToDictionary();
        
        dictionary["a"].ShouldBe(1);
        dictionary["b"].ShouldBe(true);
        dictionary["c"].ShouldBe(123.45M);

        dictionary.ShouldBeOfType<Dictionary<string, object>>();
    }
    
    [Test]
    public void EqualityComparerOverloadCreatesDictionaryWithComparer()
    {
        var dictionary = _standardSet1.ToDictionary(StringComparer.OrdinalIgnoreCase);
        
        dictionary["A"].ShouldBe(1);
        dictionary["b"].ShouldBe(true);
        dictionary["C"].ShouldBe(123.45M);

        dictionary.ShouldBeOfType<Dictionary<string, object>>();
    }

    [Test]
    public void NullKvpThrowsException()
    {
        Should.Throw<ArgumentNullException>(() => _nullSet!.ToDictionary());
    }

    [Test]
    public void NullComparerThrowsException()
    {
        Should.Throw<ArgumentNullException>(() => _standardSet1.ToDictionary(null!));
    }
}