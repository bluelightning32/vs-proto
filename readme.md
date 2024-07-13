# Vintage Story Protobuf Schema Generator

This program generates a Protocol Buffer schema for types inside Vintage Story.
This schema can be used to partially decode Vintage Story data in other
applications.

A Visual Studio Code workspace is included. The program can be built through it or
from the command line.

The `VINTAGE_STORY` environment variable must be set before building the
program, running the program, or even before loading the project in VSCode. It
should be set to the install location of Vintage Story (the directory that
contains VintagestoryLib.dll).

After the environmental variable has been set, the program can be run from the
command line through dotnet. A space separated list of classes to generate the
protobufs for can be listed on the command line. If the class list is not
given, then it defaults to `Vintagestory.Server.ServerChunk` and `SaveGame`.
The schema is printed on stdout. The `--proto3` argument can optionally be
added to use Protocol Buffer V3 syntax instead of Protocol Buffer V2 syntax.
```
dotnet run -- Vintagestory.Server.ServerChunk >schema.proto
```
