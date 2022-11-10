using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.DotNet.TestFramework;
using Microsoft.DotNet.Tools.Test.Utilities;
using Xunit;

namespace EndToEnd.Tests
{
    public class VersionTests : TestBase
    {
        [Fact]
        public void DotnetVersionReturnsCorrectVersion()
        {
            var result = new DotnetCommand()
                .ExecuteWithCapturedOutput("--version");

            result.Should().Pass();

            var dotnetFolder = Path.GetDirectoryName(RepoDirectoriesProvider.DotnetUnderTest);

            var sdkFolders = Directory.GetDirectories(Path.Combine(dotnetFolder, "sdk"));

            sdkFolders.Length.Should().Be(1, "Only one SDK folder is expected in the layout");

            var expectedSdkVersion = Path.GetFileName(sdkFolders.Single());

            result.StdOut.Trim().Should().Be(expectedSdkVersion);
        }

        /// <summary>
        /// Whenever a workload manifest is renamed, we should update the resolver to exclude the old name for loading
        /// </summary>
        [Fact]
        public void DotnetWorkloadDirectorsHaveNotChanged()
        {
            var expectedWorkloadManifests = new string[]
            {
                "microsoft.net.sdk.android","microsoft.net.sdk.ios","microsoft.net.sdk.maccatalyst","microsoft.net.sdk.macos","microsoft.net.sdk.maui","microsoft.net.sdk.tvos",
                "microsoft.net.workload.mono.toolchain.net6","microsoft.net.workload.mono.toolchain.net7","microsoft.net.workload.emscripten.net6","microsoft.net.workload.emscripten.net7"
            };

            var dotnetFolder = Path.GetDirectoryName(RepoDirectoriesProvider.DotnetUnderTest);

            var sdkFolders = Directory.GetDirectories(Path.Combine(dotnetFolder, "sdk"));

            var sdkVersion = Path.GetFileName(sdkFolders.Single());

            var includedWorkloadManifestsFile = Path.Combine(dotnetFolder, "sdk", sdkVersion, "IncludedWorkloadManifests.txt");

            var includedWorkloadManifests = File.ReadAllLines(includedWorkloadManifestsFile);

            includedWorkloadManifests.ShouldBeEquivalentTo(expectedWorkloadManifests);

        }

        /// <summary>
        /// Whenever a workload has changed, we should update the IPA list and review the VS authoring
        /// I did not include the experimental workload or runtime-workload as customers should not install those manually
        /// </summary>
        [Fact]
        public void DotnetWorkloadsHaveNotChanged()
        {
            var expectedWorkloads = new string[]
            { 
                "android", "ios", "maccatalyst", "macos", "maui", "maui-android","maui-desktop", "maui-ios", "maui-maccatalyst", "maui-mobile", 
                "maui-tizen", "maui-windows", "tvos", "wasm-tools","wasm-tools-net6"
            };

            var result = new DotnetCommand()
                .ExecuteWithCapturedOutput("workload search");

            result.Should().Pass();

            foreach (var expectedWorkload in expectedWorkloads)
            {
                result.StdOut.Trim().Should().Contain(expectedWorkload);
            }            
        }
    }
}
