using System;

namespace Stravaig.Extensions.Core.Tests;

[TestFixture]
// ReSharper disable once InconsistentNaming
public class StringExtensions_CompareExtensionsTests
{
    [Test]
    public void AIsBeforeBReturnsTrue()
    {
        "A".IsBefore("B", StringComparison.Ordinal).ShouldBeTrue();
        "a".IsBefore("B", StringComparison.OrdinalIgnoreCase).ShouldBeTrue();
        "A".IsBefore("b", StringComparison.OrdinalIgnoreCase).ShouldBeTrue();
        "a".IsBefore("b", StringComparison.OrdinalIgnoreCase).ShouldBeTrue();
    }
    
    [Test]
    public void AIsBeforeAReturnsFalse()
    {
        "A".IsBefore("A", StringComparison.Ordinal).ShouldBeFalse();
        "a".IsBefore("A", StringComparison.OrdinalIgnoreCase).ShouldBeFalse();
        "A".IsBefore("a", StringComparison.OrdinalIgnoreCase).ShouldBeFalse();
        "a".IsBefore("a", StringComparison.OrdinalIgnoreCase).ShouldBeFalse();
    }
    
    [Test]
    public void BIsBeforeAReturnsFalse()
    {
        "B".IsBefore("A", StringComparison.Ordinal).ShouldBeFalse();
        "b".IsBefore("A", StringComparison.OrdinalIgnoreCase).ShouldBeFalse();
        "B".IsBefore("a", StringComparison.OrdinalIgnoreCase).ShouldBeFalse();
        "b".IsBefore("a", StringComparison.OrdinalIgnoreCase).ShouldBeFalse();
    }

}