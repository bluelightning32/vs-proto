using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Proto;

// This class allows the Vintagestory dlls to be loaded from $(VINTAGE_STORY).
public class AddVintageStoryPath {

  [ModuleInitializer]
  internal static void ModuleInitialize() {
    s_assemblyResolveDelegate = new ResolveEventHandler(
        (sender, args) => LoadFromVintageStory(sender, args));
    AppDomain.CurrentDomain.AssemblyResolve += s_assemblyResolveDelegate;
  }

  static ResolveEventHandler s_assemblyResolveDelegate = null;

  static readonly string[] SearchSubDirs = new string[] { "", "Lib", "Mods" };

  static Assembly LoadFromVintageStory(object sender, ResolveEventArgs args) {
    string vsDir = Environment.GetEnvironmentVariable("VINTAGE_STORY");
    if (vsDir == null) {
      Console.Error.WriteLine(
          "Warning: the VINTAGE_STORY environmental variable is unset. The " +
          "program likely be unable to load the Vintagestory dlls.");
      return null;
    }
    string filename = new AssemblyName(args.Name).Name + ".dll";
    foreach (string subdir in SearchSubDirs) {
      string assemblyFile = Path.Combine(
          vsDir, subdir, filename);
      if (File.Exists(assemblyFile)) {
        return Assembly.LoadFrom(assemblyFile);
      }
    }
    return null;
  }
}
