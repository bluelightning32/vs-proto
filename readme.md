# Vintage Story Dimensions Demo

This mod shows and tests the behavior of Vintage Story dimensions, especially the Mini Dimensions.

## Building

The `VINTAGE_STORY` environment variable must be set before loading the
project. It should be set to the install location of Vintage Story (the
directory that contains VintagestoryLib.dll).

A Visual Studio Code workspace is included. The mod can be built through it or
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

### Run unit tests

```
dotnet test -c Debug --logger:"console;verbosity=detailed"
```
