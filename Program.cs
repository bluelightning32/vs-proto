using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using ProtoBuf.Meta;

namespace Proto;

class Program {
  static int Main(string[] args) {
    RuntimeTypeModel model = RuntimeTypeModel.Create();
    bool anyLoaded = false;
    ProtoSyntax syntax = ProtoSyntax.Proto2;

    List<Assembly> referencedAssemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies().Select(name => Assembly.Load(name)).ToList();

    foreach (string arg in args) {
      switch (arg) {
        case "--help":
        case "-h":
          Console.Error.WriteLine("Usage:");
          Console.Error.WriteLine(" dotnet run -- [--help] [--proto2 | --proto3] type1 type2 ...");
          return 1;
        case "--proto2":
          syntax = ProtoSyntax.Proto2;
          break;
        case "--proto3":
          syntax = ProtoSyntax.Proto3;
          break;
        default:
          Type t = referencedAssemblies.Select(a => a.GetType(arg)).FirstOrDefault(t => t != null) ?? throw new ArgumentException($"Cannot find type {arg}.");
          // The index operator forces the type to get loaded in the model. A schema will later be generated for all loaded types.
          MetaType unused = model[t];
          anyLoaded = true;
          break;
      }
    }
    if (!anyLoaded) {
      MetaType unused = model[typeof(Vintagestory.Server.ServerChunk)];
      unused = model[typeof(SaveGame)];
    }

    string schema = model.GetSchema(null, syntax);
    Console.WriteLine(schema);
    return 0;
  }
}
