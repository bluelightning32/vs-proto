using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Proto;

// This class allows the Vintagestory dlls to be loaded from $(VINTAGE_STORY).
public class AddVintageStoryPath {

  [ModuleInitializer]
  internal static void ModuleInitialize() {
    _assemblyResolveDelegate = new ResolveEventHandler(
        (sender, args) => LoadFromVintageStory(sender, args));
    AppDomain.CurrentDomain.AssemblyResolve += _assemblyResolveDelegate;
  }

  static ResolveEventHandler _assemblyResolveDelegate = null;

  static Assembly LoadFromVintageStory(object sender, ResolveEventArgs args) {
    string vsDir = Environment.GetEnvironmentVariable("VINTAGE_STORY");
    if (vsDir == null) {
      Console.Error.WriteLine(
          "Warning: the VINTAGE_STORY environmental variable is unset. The " +
          "program likely be unable to load the Vintagestory dlls.");
      return null;
    }
    string assemblyFile = Path.Combine(
        vsDir, Path.ChangeExtension(new AssemblyName(args.Name).Name, ".dll"));
    if (!File.Exists(assemblyFile)) {
      assemblyFile = Path.Combine(
          vsDir, "Lib",
          Path.ChangeExtension(new AssemblyName(args.Name).Name, ".dll"));
      if (!File.Exists(assemblyFile)) {
        return null;
      }
    }
    return Assembly.LoadFrom(assemblyFile);
  }
}
