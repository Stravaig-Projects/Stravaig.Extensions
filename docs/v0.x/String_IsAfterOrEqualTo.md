# string.IsAfterOrEqualTo(...)

An extension method to determine if a string comes after or is equal to another.

---
## `bool IsAfterOrEqualTo(this string lhs, string rhs, StringComparison comparisonType)`

### Parameters

* `lhs`: The left hand side of the operation.
* `rhs`: The right hand side of the operation.
* `comparisonType`: An enum defining the type of comparison. (See [StringComparison](https://learn.microsoft.com/en-us/dotnet/api/System.StringComparison?view=netstandard-2.0))

### Example

```csharp
string a = "A";
string b = "B";
bool result = b.IsAfterOrEqualTo(a, StringComparison.OrdinalIgnoreCase); // true
```

### See also

* [Analyser SEC0013](../analysers/SEC0014_ReplaceStringCompareWithIsAfterOrEqualTo.md)
