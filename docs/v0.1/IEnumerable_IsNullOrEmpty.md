# IEnumerable<T>.IsNullOrEmpty(...)

An extension method to look over a sequence of elements and work out whether it is empty or null.

---
## `bool IsNullOrEmpty<T>(this IEnumerable<T> source)`

### Parameters

* `source`: Extends the `IEnumerable<T>` interface.

### Example

```csharp
var aSource = null;
bool isNullOrEmpty = aSource.IsNullOrEmpty(); // true
});
```