using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Stravaig.Extensions.Core.Analyzer.Extensions;

public static class CodeFixContextExtensions
{
    public static async Task<CompilationUnitSyntax> GetCompilationRootAsync(this CodeFixContext context)
    {
        return (CompilationUnitSyntax)await context.Document
            .GetSyntaxRootAsync(context.CancellationToken)
            .ConfigureAwait(false);
    }
}