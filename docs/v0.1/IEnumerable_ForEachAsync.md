# IEnumerable<T>.ForEach(...)

An extension method to look over a sequence of elements and perform an async action on each element.

---

### `async Task ForEachAsync<T>(this IEnumerable<T> sequence, Func<T, int, Task> action)`

### Paramters

* `sequence`: Extends the `IEnumerable<T>` interface.
* `action`: An async action to perform on each element in the `sequence`. It takes two parameters and returns a `Task`
  * `T`: A single element from the sequence.
  * `int`: The (virtual) index of the element in the sequence.

### Example
```csharp
await aSequence.ForEachAsync(async (e,i) => {
    await aWriter.WriteLine($"Item {i}: {e}");
});
```

## `async Task ForEachAsync<T>(this IEnumerable<T> sequence, Func<T, Task> action)`

### Paramters

* `sequence`: Extends the `IEnumerable<T>` interface.
* `action`: An action to perform on each element in the `sequence`. It takes two parameters
  * `T`: A single element from the sequence.
  * `int`: The (virtual) index of the element in the sequence.

### Example

```csharp
await aSequence.ForEachAsync(async (e) => {
    await aWriter.WriteLineAsync($"Item: {e}");
});
```

