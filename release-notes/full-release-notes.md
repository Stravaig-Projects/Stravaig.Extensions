# Full Release Notes

## Version 0.1.2

Date: Sunday, 30 April, 2023 at 09:58:52 +00:00

### Bugs

### Features

- #56: Add analysers & code fixes for string.Compare(...) replacement extensions.

### Miscellaneous

- #46: Fix the contributions page in the documentation.
- #57: Call the update docs script from the GitHub documentation workflow to regenerate some document files on doc build.

---


## Version 0.1.1

Date: Sunday, 23 April, 2023 at 20:49:47 +00:00

### Features

- #49: Add an analyser and code fix for detecting the use of `!string.IsNullOrWhiteSpace()` and suggesting the use of the `.HasContent()` extension method instead to improve code readability.

### Miscellaneous

- #43: Update the build actions that used a deprecated version of Node.js
- #45: Automatically update the metadata in the documentation on a release.
- #50: Update the template for the docs & get it running as a GitHub Action instead of the classic pipeline.

## Version 0.1.0

Date: Thursday, 13 April, 2023 at 20:49:50 +00:00

### Features

- Added `HasContent` string extension methods.
- Added `ForEach` and `ForEachAsync` extension methods.
- #2: Added `IsEmpty` and `IsNullOrEmpty` extension methods.
- #31: Added `ToDictionary` extension methods.
- #40: Added `IsAfter`, `IsAfterOrEqualTo`, `IsBefore` and `IsBeforeOrEqualTo` string extensions.



