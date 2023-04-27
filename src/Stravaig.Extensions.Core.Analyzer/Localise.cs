using Microsoft.CodeAnalysis;

namespace Stravaig.Extensions.Core.Analyzer;

internal static class Localise
{
    internal static LocalizableResourceString Resource(string name)
    {
        return new LocalizableResourceString(name, Resources.ResourceManager, typeof(Resources));
    }
}