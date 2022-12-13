using System;
using NUnit.Framework;
using Shouldly;

namespace Stravaig.Extensions.Core.Tests;

[TestFixture]
public class IEnumerableOfTExtensions_IsEmptyTests
{
    [Test]
    public void IsEmpty_OnEmptyArray_ReturnsTrue()
    {
        Array.Empty<string>().IsEmpty().ShouldBeTrue();
    }

    [Test]
    public void IsEmpty_OnPopulatedArray_ReturnsFalse()
    {
        new string[1].IsEmpty().ShouldBeFalse();
    }

    [Test]
    public void IsEmpty_OnNull_ThrowsException()
    {
        Should.Throw<ArgumentNullException>(() =>
            ((string[]) null).IsEmpty());
    }

    [Test]
    public void IsNullOrEmpty_OnEmptyArray_ReturnsTrue()
    {
        Array.Empty<string>().IsNullOrEmpty().ShouldBeTrue();
    }

    [Test]
    public void IsNullOrEmpty_OnPopulatedArray_ReturnsFalse()
    {
        new string[1].IsNullOrEmpty().ShouldBeFalse();
    }

    [Test]
    public void IsNullOrEmpty_OnNull_ReturnsTrue()
    {
        ((string[])null).IsNullOrEmpty().ShouldBeTrue();
    }
}