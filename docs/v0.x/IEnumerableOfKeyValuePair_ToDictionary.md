# IEnumerable<KeyValuePair<TKey, TValue>>.ToDictionary(...)

Converts the `KeyValuePair<TKey,TValue>` to a `Dictionary<TKey,TValue>`

---

## Dictionary<TKey,TValue> ToDictionary(this IEnumerable<KeyValuePair<TKey,TValue>> source)

### Parameters

* `source`: Extends the `IEnumerable<KeyValuePair<TKey,TValue>>` interface

### Example

```csharp
KeyValuePair<string, object>[] kvp = 
    {
        new("a", 1),
        new("b", true),
        new("c", 123.45M)
    };

Dictionary<string, object> dictionary = kvp.ToDictionary();
```

## Dictionary<TKey,TValue> ToDictionary(this IEnumerable<KeyValuePair<TKey,TValue>> source, IEqualityComparer<TKey> comparer)

### Parameters

* `source`: Extends the `IEnumerable<KeyValuePair<TKey,TValue>>` interface
* `comparer`: The comparer used to evaluate the keys.

### Example

```csharp
KeyValuePair<string, object>[] kvp = 
    {
        new("a", 1),
        new("b", true),
        new("c", 123.45M)
    };

Dictionary<string, object> dictionary = kvp.ToDictionary(StringComparer.OrdinalIgnoreCase);

var a = dictionary["A"]; // a is populated with 1
```

