using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stravaig.Extensions.Core.Analyzer
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(Sec0001UseStringHasContentAnalyzerCodeFixProvider)), Shared]
    public class Sec0001UseStringHasContentAnalyzerCodeFixProvider : CodeFixProvider
    {
        private static readonly ImmutableArray<string> CachedFixableDiagnostics =
            ImmutableArray.Create(Sec0001UseStringHasContentAnalyzer.DiagnosticId);
        public sealed override ImmutableArray<string> FixableDiagnosticIds => CachedFixableDiagnostics;

        public sealed override FixAllProvider GetFixAllProvider()
        {
            // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            // TODO: Replace the following code with your own analysis, generating a CodeAction for each fix to suggest
            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            // Find the type declaration identified by the diagnostic.
            var declaration = root.FindToken(diagnosticSpan.Start);

            var declarationKind = declaration.Kind();
            switch (declarationKind)
            {
                case SyntaxKind.ExclamationToken:
                    var notExpression = (PrefixUnaryExpressionSyntax) declaration.Parent;
                    context.RegisterCodeFix(
                        CodeAction.Create(
                            title: CodeFixResources.CodeFixTitle,
                            createChangedDocument: ct => FixNotStringIsNullOrWhiteSpaceAsync(context.Document, notExpression, ct),
                            equivalenceKey: nameof(CodeFixResources.CodeFixTitle)),
                        diagnostic);
                    break;
                case SyntaxKind.EqualsExpression:
                    var equalsExpression = (BinaryExpressionSyntax) declaration.Parent;
                    break;
            }

            // Register a code action that will invoke the fix.
            //context.RegisterCodeFix(
            //    CodeAction.Create(
            //        title: CodeFixResources.CodeFixTitle,
            //        createChangedSolution: c => MakeUppercaseAsync(context.Document, declaration, c),
            //        equivalenceKey: nameof(CodeFixResources.CodeFixTitle)),
            //    diagnostic);
        }

        private async Task<Document> FixNotStringIsNullOrWhiteSpaceAsync(Document contextDocument, PrefixUnaryExpressionSyntax notExpression, CancellationToken ct)
        {
            int aNumber = 123;
            string someString = "someString";
            if (!string.IsNullOrWhiteSpace(someString)){ }
            if (someString.Contains("s")) { }
            if (aNumber.ToString().Contains("1")) { }

            var invocation = notExpression.ChildNodes().OfType<InvocationExpressionSyntax>().First();
            var stringObjectToken = invocation.ArgumentList.Arguments.First().ChildNodes().First();


            //var parent = notExpression.Parent.ReplaceNode(notExpression, )

            await Task.CompletedTask;
            return null;
        }

        private async Task<Solution> MakeUppercaseAsync(Document document, TypeDeclarationSyntax typeDecl, CancellationToken cancellationToken)
        {
            // Compute new uppercase name.
            var identifierToken = typeDecl.Identifier;
            var newName = identifierToken.Text.ToUpperInvariant();

            // Get the symbol representing the type to be renamed.
            var semanticModel = await document.GetSemanticModelAsync(cancellationToken);
            var typeSymbol = semanticModel.GetDeclaredSymbol(typeDecl, cancellationToken);

            // Produce a new solution that has all references to that type renamed, including the declaration.
            var originalSolution = document.Project.Solution;
            var optionSet = originalSolution.Workspace.Options;
            var newSolution = await Renamer.RenameSymbolAsync(document.Project.Solution, typeSymbol, newName, optionSet, cancellationToken).ConfigureAwait(false);

            // Return the new solution with the now-uppercase type name.
            return newSolution;
        }
    }
}
