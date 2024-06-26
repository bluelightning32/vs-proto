# Vintage Story Block Interactions Demo

The program links against Vintage Story, and asks its protobuf serializer to dump the proto schema.

## Building

The `VINTAGE_STORY` environment variable must be set before loading the
project. It should be set to the install location of Vintage Story (the
directory that contains VintagestoryLib.dll).

A Visual Studio Code workspace is included. The program can be built through it or
from the command line.

### Release build from command line

This will produce a zip file in a subfolder of `bin/Release`.
```
dotnet build -c Release
```

### Debug build from command line

This will produce a zip file in a subfolder of `bin/Debug`.
```
dotnet build -c Debug
```
