# string.HasContent()

An extension method to determine if a string has content, or is null, empty or consists only of whitespace.

---
## `bool HasContent(this string target)`

### Parameters

* `target`: The target string to test.

### Example

```csharp
var aString = "I have content";
bool hasContent = aString.HasContent(); // true
});
```

### See also

* [Analyser SEC0001](../analysers/SEC0001_UseStringHasContent.md)
