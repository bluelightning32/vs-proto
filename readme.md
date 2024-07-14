# Vintage Story Protobuf Schema Generator

This program extracts a Protocol Buffer schema for protobuf-net annotated types
inside the Vintage Story game. This schema can be used to partially decode
Vintage Story data in other applications, such as with
[protoc](https://stackoverflow.com/a/50740236).

A Visual Studio Code workspace is included. The program can be built through it
or from the command line.

The 3.2.42 version of protobuf-net is required. This is newer than what's
included in Vintage Story, and it is newer than what is currently available
from NuGet. So before building or running the program, the
[package](https://www.myget.org/feed/protobuf-net/package/nuget/protobuf-net)
must be installed from MyGet. The following command will install the package in
the local NuGet package cache.
```
dotnet add package protobuf-net --version 3.2.42 --source https://www.myget.org/F/protobuf-net/api/v3/index.json
```

The `VINTAGE_STORY` environment variable must be set before building the
program, running the program, or even before loading the project in VSCode. It
should be set to the install location of Vintage Story (the directory that
contains VintagestoryLib.dll).

After the environmental variable has been set, the program can be run from the
command line through dotnet. A space separated list of classes to generate the
protobufs for can be listed on the command line. If the class list is not
given, then it defaults to `Vintagestory.GameContent.MapPieceDB`,
`Vintagestory.Server.ServerWorldPlayerData`, `Vintagestory.Server.ServerChunk`,
`Vintagestory.Server.ServerMapChunk`, `Vintagestory.Server.ServerMapRegion`,
and `SaveGame`. The schema is printed on stdout. The `--proto3` argument can
optionally be added to use Protocol Buffer V3 syntax instead of Protocol Buffer
V2 syntax.
```
dotnet run -- Vintagestory.Server.ServerChunk >schema.proto
```

# Pregenerated schema

The schema-1.19.8.proto file in the source tree contains the schema of the
default types extracted from Vintage Story version 1.19.8.
