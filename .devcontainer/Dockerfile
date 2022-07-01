# Adapted from https://github.com/dotnet/dotnet-buildtools-prereqs-docker/blob/main/src/ubuntu/22.04/amd64/Dockerfile

FROM mcr.microsoft.com/vscode/devcontainers/base:ubuntu-22.04

RUN apt-get update \
    && apt-get install -y \
        cmake \
        clang-14 \
        gdb \
        liblldb-14-dev \
        lldb-14 \
        llvm-14 \
        locales \
        make
    
# Install tools used by the VSO build automation.
RUN apt-get update \
    && apt-get install -y \
        git \
        nodejs \
        npm \
        zip \
        curl \
    && rm -rf /var/lib/apt/lists/*

# .NET SDK MSBuild requires US.UTF-8 locale to execute tasks
RUN locale-gen en_US.UTF-8

# Runtime dependencies
RUN apt-get update \
    && apt-get install -y \
        autoconf \
        automake \
        curl \
        build-essential \
        gettext \
        libgdiplus \
        libicu-dev \
        libkrb5-dev \
        liblttng-ust-dev \
        libnuma-dev \
        libssl-dev \
        libtool \
        libunwind8 \
        libunwind8-dev \
        uuid-dev \
    && rm -rf /var/lib/apt/lists/*