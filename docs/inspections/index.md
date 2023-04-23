# Analysers and Code Fixes

The package comes with a set of analysers and code fixes. These can point out where you can make your code more readable and may also provide an automatic fix.

There is normally nothing additional you need to do to get these to work, by installing the package the analysers will also be installed in Visual Studio or Rider and they will inspect the code.

## IDE specific instructions

* [JetBrains Rider](rider.md)

## .editorconfig file

You can configure the inspections in a `.editorconfig` file. This is compatible with more recent versions of IDEs.

### Hierarchical nature

The `.editorconfig` file is hierarchical, which means that each file overrides the settings in the parent directory. Common settings can go in the top level directory and more specific in the directories below.

If you don't specify, when the IDE builds the settings from the `.editorconfig` file it will keep navigating back until it reaches the top/root level of your drive. To prevent this add the following to the `.editorconfig` file you want to be at the top level, which will usually be the base directory for your repository.

```
root = true
```

This instructs the parser not to go looking for a `.editorconfig` file any further up the directory tree.

### Configuring inspections

If you already have a `.editorconfig` file it may already contain sections that start with a file pattern such as:

```
[*.{cs,vb}]
```

The above example instructs the IDE to only take the rules that follow if the file ends in `.cs` or `.vb`

For the analysers and code fixes in the `Stravaig.Extensions.Core` packate only C# is currently supported, so the section can simply be:

```
[*.cs]
```

The rules are places one per line like this:

```
[*.cs]
dotnet_diagnostic.sec0001.severity = warning
```

This will set the rule with the ID `SEC0001` to a warning. See [Analysers](../analysers/index.md) for a list of the codes that go with each inspection rule.

Other values are `none`, `suggestion`, and `error`

