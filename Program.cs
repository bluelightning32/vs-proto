using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

using ProtoBuf.Meta;

namespace Proto;

class Program {
  static int Main(string[] args) {
    RuntimeTypeModel model = RuntimeTypeModel.Create();
    SchemaGenerationOptions options = new();
    // This flag is necessary because VS uses the same `None` enum value for
    // multiple enum types.
    options.Flags |= SchemaGenerationFlags.IncludeEnumNamePrefix;
    options.Syntax = ProtoSyntax.Proto2;

    List<Assembly> referencedAssemblies =
        Assembly.GetEntryAssembly()
            .GetReferencedAssemblies()
            .Select(name => Assembly.Load(name))
            .ToList();

    foreach (string arg in args) {
      switch (arg) {
      case "--help":
      case "-h":
        Console.Error.WriteLine("Usage:");
        Console.Error.WriteLine(
            " dotnet run -- [--help] [--proto2 | --proto3] type1 type2 ...");
        return 1;
      case "--proto2":
        options.Syntax = ProtoSyntax.Proto2;
        break;
      case "--proto3":
        options.Syntax = ProtoSyntax.Proto3;
        break;
      default:
        Type t = referencedAssemblies.Select(a => a.GetType(arg))
                     .FirstOrDefault(t => t != null) ??
                 throw new ArgumentException($"Cannot find type {arg}.");
        options.Types.Add(t);
        break;
      }
    }
    if (options.Types.Count == 0) {
      options.Types.Add(typeof(Vintagestory.Server.ServerChunk));
      options.Types.Add(typeof(Vintagestory.Server.ServerMapChunk));
      options.Types.Add(typeof(Vintagestory.Server.ServerMapRegion));
      options.Types.Add(typeof(SaveGame));
      options.Types.Add(typeof(Vintagestory.Server.ServerWorldPlayerData));
      options.Types.Add(typeof(Vintagestory.GameContent.MapPieceDB));
    }

    string schema = model.GetSchema(options);
    // Protobuf-net version 3.2.42 added the IncludeEnumNamePrefix flag.
    // However, it still does not prepend the enum name for default values. Use
    // a regex to look for lines like this:
    //    optional EnumFreeMovAxisLock freeMovePlaneLock = 10 [default = None];
    // And replace them with lines like this:
    //    optional EnumFreeMovAxisLock freeMovePlaneLock = 10 [default =
    //    EnumFreeMovAxisLock_None];
    schema = Regex.Replace(
        schema,
        @" (?<enum>Enum\w+)(?<middle> .* )\[default = (?<value>[A-Za-z]\w+)\]",
        " ${enum}${middle}[default = ${enum}_${value}]");
    Console.WriteLine(schema);
    return 0;
  }
}
