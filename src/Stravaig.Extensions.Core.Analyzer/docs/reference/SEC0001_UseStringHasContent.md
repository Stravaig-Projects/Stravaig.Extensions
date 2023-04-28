# SEC0001: Use String HasContent() 

## Cause

The code contains a `!string.IsNullOrWhiteSpace(aString)` or `string.IsNullOrWhiteSpace(aString) == false`

## Rule description

Replace `!string.IsNullOrWhiteSpace(aString)` or `string.IsNullOrWhiteSpace(aString) == false` with `aString.HasContent()` for readability.

## How to fix violations

1. Add a using declaration for the `Stravaig.Extensions.Core` namespace.
2. Replace any instance of:
  1. `!string.IsNullOrWhiteSpace(aString)` with `aString.HasContent()`
  2. `string.IsNullOrWhiteSpace(aString) == false` with `aString.HasContent()`

## When to suppress warnings

If you prefer the original style of string check.

## Example of a violation

```csharp

if (!string.IsNullOrWhiteSpace(aString))
{
    // Do something conditional on the string having some content.
}
```

