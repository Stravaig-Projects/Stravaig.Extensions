# IEnumerable<T>.ForEach(...)

An extension method to look over a sequence of elements and perform an action on each element.

## `void ForEach<T>(this IEnumerable<T> sequence, Action<T, int> action)`

* `aSequence` is an `IEnumerable<T>`
* `e` is an individual element in the sequence of type `T`
```csharp
aSequence.ForEach((e) => {
    Console.WriteLine($"Item: {e}");
});
```

## `void ForEach<T>(this IEnumerable<T> sequence, Action<T, int> action)`

* `aSequence` is an `IEnumerable<T>`
* `e` is an individual element in the sequence of type `T`
* `i` is an integer representing the (virtual) index of the element in the sequence.
```csharp
aSequence.ForEach((e,i) => {
    Console.WriteLine($"Item {i}: {e}");
});
```

## `async Task ForEachAsync<T>(this IEnumerable<T> sequence, Func<T, Task> action)`

* `aSequence` is an `IEnumerable<T>`
* `e` is an individual element in the sequence of type `T`
* `aWriter` is a `TextWriter`
```csharp
await aSequence.ForEachAsync(async (e) => {
    await aWriter.WriteLineAsync($"Item: {e}");
});
```

## `async Task ForEachAsync<T>(this IEnumerable<T> sequence, Func<T, int, Task> action)`

* `aSequence` is an `IEnumerable<T>`
* `e` is an individual element in the sequence of type `T`
* `i` is an integer representing the (virtual) index of the element in the sequence.
* `aWriter` is a `TextWriter`
```csharp
await aSequence.ForEachAsync(async (e,i) => {
    await aWriter.WriteLine($"Item {i}: {e}");
});
```
