using Microsoft.CodeAnalysis;

namespace Stravaig.Extensions.Core.Analyzer;

public static class Localise
{
    public static LocalizableResourceString Resource(string name)
    {
        return new LocalizableResourceString(name, Resources.ResourceManager, typeof(Resources));
    }
}