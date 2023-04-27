using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;
using System;

namespace Stravaig.Extensions.Core.Analyzer;

public static class CompilationUnitSyntaxExtensions
{
    public static CompilationUnitSyntax UseStravaigExtensionsCore(this CompilationUnitSyntax oldRoot)
    {
        SyntaxList<UsingDirectiveSyntax> usingDeclarations;
        if (oldRoot.Usings.Count != 0)
        {
            usingDeclarations = oldRoot.Usings;
        }
        else
        {
            var searchStart = GetNamespaceDeclaration(oldRoot);
            usingDeclarations = searchStart?.Usings ?? oldRoot.Usings;
        }

        (bool usingExists, SyntaxNode insertBefore) = FindInsertionPoint(usingDeclarations);

        if (usingExists)
            return oldRoot;

        return insertBefore != null
            ? oldRoot.InsertNodesBefore(insertBefore, UsingStravaigExtensionsCore())
            : oldRoot.AddUsings(UsingStravaigExtensionsCore());
    }

    private static BaseNamespaceDeclarationSyntax GetNamespaceDeclaration(CompilationUnitSyntax oldRoot)
    {

        var childNodes = oldRoot.Members;
        var fileScopesNamespaceNodes = childNodes.OfType<FileScopedNamespaceDeclarationSyntax>();
        var namespaceDeclaration = fileScopesNamespaceNodes.FirstOrDefault();
        if (namespaceDeclaration != null)
            return namespaceDeclaration;

        var traditionalNamespaceNodes = childNodes.OfType<NamespaceDeclarationSyntax>();
        return traditionalNamespaceNodes.FirstOrDefault();
    }

    private static (bool, SyntaxNode) FindInsertionPoint(SyntaxList<UsingDirectiveSyntax> usings)
    {
        SyntaxNode insertBefore = null;
        foreach (var usingDirective in usings)
        {
            string usingNamespace = "";
            if (usingDirective.Name.Kind() == SyntaxKind.IdentifierName)
            {
                var identifierName = (IdentifierNameSyntax)usingDirective.Name;
                usingNamespace = identifierName.Identifier.Text;
            }
            else if (usingDirective.Name.Kind() == SyntaxKind.QualifiedName)
            {
                var qualifiedIdentifierName = (QualifiedNameSyntax)usingDirective.Name;
                usingNamespace = qualifiedIdentifierName.AsString();
            }

            if (usingNamespace.Equals("Stravaig.Extensions.Core"))
                return (true, null);

            if (usingNamespace.IsAfter("Stravaig.Extensions.Core", StringComparison.Ordinal))
            {
                insertBefore ??= usingDirective;
            }
        }

        return (false, insertBefore);
    }

    private static UsingDirectiveSyntax[] UsingStravaigExtensionsCore()
    {
        SimpleNameSyntax stravaig = SyntaxFactory.IdentifierName("Stravaig");
        SimpleNameSyntax extensions = SyntaxFactory.IdentifierName("Extensions");
        SimpleNameSyntax core = SyntaxFactory.IdentifierName("Core");
        QualifiedNameSyntax stravaigExtensions = SyntaxFactory.QualifiedName(stravaig, extensions);
        NameSyntax name = SyntaxFactory.QualifiedName(stravaigExtensions, core);
        UsingDirectiveSyntax usingStatement = SyntaxFactory.UsingDirective(name);
        return new[] { usingStatement };
    }
}