# Release Notes

## Version 0.1.1

Date: Sunday, 23 April, 2023 at 20:49:47 +00:00

### Features

- #49: Add an analyser and code fix for detecting the use of `!string.IsNullOrWhiteSpace()` and suggesting the use of the `.HasContent()` extension method instead to improve code readability.

### Miscellaneous

- #43: Update the build actions that used a deprecated version of Node.js
- #45: Automatically update the metadata in the documentation on a release.
- #50: Update the template for the docs & get it running as a GitHub Action instead of the classic pipeline.

