using System;

namespace Stravaig.Extensions.Core.Tests;

[TestFixture]
// ReSharper disable once InconsistentNaming
public class StringExtensions_CompareExtensionsTests
{
    [Test]
    public void AIsAfterBReturnsFalse()
    {
        "A".IsAfter("B", StringComparison.Ordinal).ShouldBeFalse();
        "a".IsAfter("B", StringComparison.OrdinalIgnoreCase).ShouldBeFalse();
        "A".IsAfter("b", StringComparison.OrdinalIgnoreCase).ShouldBeFalse();
        "a".IsAfter("b", StringComparison.OrdinalIgnoreCase).ShouldBeFalse();
    }
    
    [Test]
    public void AIsAfterAReturnsFalse()
    {
        "A".IsAfter("A", StringComparison.Ordinal).ShouldBeFalse();
        "a".IsAfter("A", StringComparison.OrdinalIgnoreCase).ShouldBeFalse();
        "A".IsAfter("a", StringComparison.OrdinalIgnoreCase).ShouldBeFalse();
        "a".IsAfter("a", StringComparison.OrdinalIgnoreCase).ShouldBeFalse();
    }
    
    [Test]
    public void BIsAfterAReturnsTrue()
    {
        "B".IsAfter("A", StringComparison.Ordinal).ShouldBeTrue();
        "b".IsAfter("A", StringComparison.OrdinalIgnoreCase).ShouldBeTrue();
        "B".IsAfter("a", StringComparison.OrdinalIgnoreCase).ShouldBeTrue();
        "b".IsAfter("a", StringComparison.OrdinalIgnoreCase).ShouldBeTrue();
    }
    
    [Test]
    public void AIsAfterOrEqualToBReturnsFalse()
    {
        "A".IsAfterOrEqualTo("B", StringComparison.Ordinal).ShouldBeFalse();
        "a".IsAfterOrEqualTo("B", StringComparison.OrdinalIgnoreCase).ShouldBeFalse();
        "A".IsAfterOrEqualTo("b", StringComparison.OrdinalIgnoreCase).ShouldBeFalse();
        "a".IsAfterOrEqualTo("b", StringComparison.OrdinalIgnoreCase).ShouldBeFalse();
    }
    
    [Test]
    public void AIsAfterOrEqualToAReturnsTrue()
    {
        "A".IsAfterOrEqualTo("A", StringComparison.Ordinal).ShouldBeTrue();
        "a".IsAfterOrEqualTo("A", StringComparison.OrdinalIgnoreCase).ShouldBeTrue();
        "A".IsAfterOrEqualTo("a", StringComparison.OrdinalIgnoreCase).ShouldBeTrue();
        "a".IsAfterOrEqualTo("a", StringComparison.OrdinalIgnoreCase).ShouldBeTrue();
    }
    
    [Test]
    public void BIsAfterOrEqualToAReturnsTrue()
    {
        "B".IsAfterOrEqualTo("A", StringComparison.Ordinal).ShouldBeTrue();
        "b".IsAfterOrEqualTo("A", StringComparison.OrdinalIgnoreCase).ShouldBeTrue();
        "B".IsAfterOrEqualTo("a", StringComparison.OrdinalIgnoreCase).ShouldBeTrue();
        "b".IsAfterOrEqualTo("a", StringComparison.OrdinalIgnoreCase).ShouldBeTrue();
    }
    
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

    [Test]
    public void AIsBeforeOrEqualToBReturnsTrue()
    {
        "A".IsBeforeOrEqualTo("B", StringComparison.Ordinal).ShouldBeTrue();
        "a".IsBeforeOrEqualTo("B", StringComparison.OrdinalIgnoreCase).ShouldBeTrue();
        "A".IsBeforeOrEqualTo("b", StringComparison.OrdinalIgnoreCase).ShouldBeTrue();
        "a".IsBeforeOrEqualTo("b", StringComparison.OrdinalIgnoreCase).ShouldBeTrue();
    }
    
    [Test]
    public void AIsBeforeOrEqualToAReturnsTrue()
    {
        "A".IsBeforeOrEqualTo("A", StringComparison.Ordinal).ShouldBeTrue();
        "a".IsBeforeOrEqualTo("A", StringComparison.OrdinalIgnoreCase).ShouldBeTrue();
        "A".IsBeforeOrEqualTo("a", StringComparison.OrdinalIgnoreCase).ShouldBeTrue();
        "a".IsBeforeOrEqualTo("a", StringComparison.OrdinalIgnoreCase).ShouldBeTrue();
    }
    
    [Test]
    public void BIsBeforeOrEqualToAReturnsFalse()
    {
        "B".IsBeforeOrEqualTo("A", StringComparison.Ordinal).ShouldBeFalse();
        "b".IsBeforeOrEqualTo("A", StringComparison.OrdinalIgnoreCase).ShouldBeFalse();
        "B".IsBeforeOrEqualTo("a", StringComparison.OrdinalIgnoreCase).ShouldBeFalse();
        "b".IsBeforeOrEqualTo("a", StringComparison.OrdinalIgnoreCase).ShouldBeFalse();
    }
}