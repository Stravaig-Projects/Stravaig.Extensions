# string.IsBeforeOrEqualTo(...)

An extension method to determine if a string comes before or is equal to another.

---
## `bool IsBeforeOrEqualTo(this string lhs, string rhs, StringComparison comparisonType)`

### Parameters

* `lhs`: The left hand side of the operation.
* `rhs`: The right hand side of the operation.
* `comparisonType`: An enum defining the type of comparison. (See [StringComparison](https://learn.microsoft.com/en-us/dotnet/api/System.StringComparison?view=netstandard-2.0))

### Example

```csharp
string a = "A";
string b = "B";
bool result = a.IsBeforeOrEqualTo(b, StringComparison.OrdinalIgnoreCase); // true
```