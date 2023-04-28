# string.IsAfter(...)

An extension method to determine if a string comes after another.

---
## `bool IsAfter(this string lhs, string rhs, StringComparison comparisonType)`

### Parameters

* `lhs`: The left hand side of the operation.
* `rhs`: The right hand side of the operation.
* `comparisonType`: An enum defining the type of comparison. (See [StringComparison](https://learn.microsoft.com/en-us/dotnet/api/System.StringComparison?view=netstandard-2.0))

### Example

```csharp
string a = "A";
string b = "B";
bool result = b.IsAfter(a, StringComparison.OrdinalIgnoreCase); // true
```

### See also

* [Analyser SEC0014](../analysers/SEC0014_ReplaceStringCompareWithIsAfter.md)
