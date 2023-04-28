using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;

namespace Stravaig.Extensions.Core.Analyzer.Tests
{
    public static partial class CSharpCodeFixVerifier<TAnalyzer, TCodeFix>
        where TAnalyzer : DiagnosticAnalyzer, new()
        where TCodeFix : CodeFixProvider, new()
    {
        public class Test : CSharpCodeFixTest<TAnalyzer, TCodeFix, NUnitVerifier>
        {
            public Test()
            {
                var referenceAssemblies = ReferenceAssemblies.Default
                    .AddPackages(ImmutableArray.Create(
                        new PackageIdentity("Stravaig.Extensions.Core", "0.1.0")))
                    .AddAssemblies(
                        ImmutableArray.Create(
                            "Stravaig.Extensions.Core")
                    );

                ReferenceAssemblies = referenceAssemblies;
                SolutionTransforms.Add((solution, projectId) =>
                {
                    var compilationOptions = solution.GetProject(projectId)!.CompilationOptions;
                    compilationOptions = compilationOptions!
                        .WithSpecificDiagnosticOptions(
                            compilationOptions.SpecificDiagnosticOptions.SetItems(CSharpVerifierHelper.NullableWarnings));
                    solution = solution.WithProjectCompilationOptions(projectId, compilationOptions);

                    return solution;
                });
            }
        }
    }
}
