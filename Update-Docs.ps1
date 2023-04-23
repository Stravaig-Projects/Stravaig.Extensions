Copy-Item "$PSScriptRoot/contributors.md" "$PSScriptRoot/docs/contributors.md"

Copy-Item "$PSScriptRoot/release-notes/release-notes-*.md" "$PSScriptRoot/docs/release-notes/"

$releaseNotesIndexFile = @(
"# Release Notes"
""
"The releases on this package most recent first."
""
);

Get-ChildItem "$PSScriptRoot/release-notes/release-notes*.md" |
    Sort-Object -Descending -Property Name |
    ForEach-Object -Process {
        $name = $_.Name;
        $releaseDate = $_.LastWriteTimeUtc.ToString("dddd, d MMMM yyyy 'at' HH:mm");
        $null = $name -match "release-notes-(?<version>[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3})\.md";
        $version = $Matches.version;
        $releaseNotesIndexFile += "- **[v$version]($name) released on $releaseDate UTC**";
        $releaseNotesIndexFile += "  - [GitHub Release](https://github.com/Stravaig-Projects/Stravaig.Extensions.Core/releases/tag/v$version)"
        $releaseNotesIndexFile += "  - [Nuget Package](https://www.nuget.org/packages/Stravaig.Extensions.Core/$version)"
    }

Set-Content "$PSScriptRoot/docs/release-notes/index.md" $releaseNotesIndexFile -Encoding UTF8 -Force

$releaseNotesIndexFile