# IEnumerable<T>.ForEach(...)

An extension method to look over a sequence of elements and perform an action on each element.

---
## `void ForEach<T>(this IEnumerable<T> sequence, Action<T, int> action)`

### Parameters

* `sequence`: Extends the `IEnumerable<T>` interface.
* `action`: An action to perform on each element in the `sequence`. It takes two parameters
  * `T`: A single element from the sequence.
  * `int`: The (virtual) index of the element in the sequence.

### Example

```csharp
aSequence.ForEach((e) => {
    Console.WriteLine($"Item: {e}");
});
```

## `void ForEach<T>(this IEnumerable<T> sequence, Action<T, int> action)`
### Parameters

* `sequence`: Extends the `IEnumerable<T>` interface.
* `action`: An action to perform on each element in the `sequence`. It takes a single parameter
  * `T`: A single element from the sequence.

### Example

sequence.
```csharp
aSequence.ForEach((e,i) => {
    Console.WriteLine($"Item {i}: {e}");
});
```
