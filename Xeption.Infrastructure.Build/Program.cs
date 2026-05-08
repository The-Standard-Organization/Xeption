// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
// Licensed under The Standard Software License (TSSL).
// See License.txt in the project root for license information.
// ----------------------------------------------------------------------------------

using Xeption.Infrastructure.Build.Services;

namespace Xeptions.Infrastructure.Build
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var scriptGenerationService = new ScriptGenerationService();

            scriptGenerationService.GenerateBuildScript(
                branchName: "main",
                projectName: "Xeption",
                dotNetVersion: "10.x");

            scriptGenerationService.GeneratePrLintScript(branchName: "main");
        }
    }
}
