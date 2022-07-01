#!/usr/bin/env bash

set -e pipefail

./build.sh /p:ArcadeBuildTarball=true /p:TarballDir=/workspaces/dotnet-source/ /p:PreserveTarballGitFolders=true

cd /workspaces/dotnet-source/

./prep.sh
./build.sh --online

# save the commit hash of the currently built repo, so developers know which version was built
git rev-parse HEAD > ./artifacts/prebuild.sha