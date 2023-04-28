# SEC0013: String can be compared with .IsAfterOrEqualTo()

## Cause

The code contains an expression like `string.Compare(a, b, comparison) >= 0`

## Rule description

Replace `string.Compare(a, b, comparison) >= 0` with `a.IsAfterOrEqualTo(b, comparison)`

## How to fix violations

1. Add a using declaration for the `Stravaig.Extensions.Core` namespace if it does not already exist.
2. Replace any instance of `string.Compare(a, b, comparison) >= 0` with `a.IsAfterOrEqualTo(b, comparison)`

## When to suppress warnings

If you prefer the orginal style of string check.

## Example of a violation

```csharp

if (string.Compare(a, b, StringComparison.OrdinalIgnoreCase) >= 0)
{
    // Perform work required when a is after or equal to b.
}
```

## Related rules

* [SEC0011: String can be compared with .IsBefore()](SEC0011_ReplaceStringCompareWithIsBeforeAnalyzer.md)
* [SEC0012: String can be compared with .IsBeforeOrEqualTo()](SEC0012_ReplaceStringCompareWithIsBeforeOrEqualToAnalyzer.md)
* [SEC0014: String can be compared with .IsAfter()](SEC0014_ReplaceStringCompareWithIsAfterAnalyzer.md)
