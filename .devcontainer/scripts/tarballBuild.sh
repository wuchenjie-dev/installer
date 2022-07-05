#!/usr/bin/env bash

# We want to always have exit code 0 so that we can use the codespace prebuild to diagnose errors.
./build.sh /p:ArcadeBuildTarball=true /p:TarballDir=/workspaces/dotnet-source/ /p:PreserveTarballGitFolders=true || true

# save the commit hash of the currently built repo, so developers know which version was built
git rev-parse HEAD > ./artifacts/prebuild.sha

cd /workspaces/dotnet-source/

./prep.sh
./build.sh --online || true