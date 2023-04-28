using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Stravaig.Extensions.Core.Analyzer.Extensions;

public static class QualifiedNameSyntaxExtensions
{
    public static string AsString(this QualifiedNameSyntax qualifiedName)
    {
        string left = qualifiedName.Left.Kind() == SyntaxKind.QualifiedName
            ? ((QualifiedNameSyntax)qualifiedName.Left).AsString()
            : ((IdentifierNameSyntax)qualifiedName.Left).Identifier.Text;
        string right = ((IdentifierNameSyntax)qualifiedName.Right).Identifier.Text;
        return $"{left}.{right}";
    }
}