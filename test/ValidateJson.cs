using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lambda.Tests;

[TestClass]
public class ValidateJson {
  // This property is set by the test framework:
  // https://learn.microsoft.com/en-us/visualstudio/test/how-to-create-a-data-driven-unit-test?view=vs-2022#add-a-testcontext-to-the-test-class
  public TestContext TestContext { get; set; } = null!;

  private string ResourcesPath() {
    string path = (string)TestContext.Properties["ResourcesPath"];
    if (path is null) {
      return null;
    }

    return Path.GetFullPath(
        Path.Combine(TestContext.DeploymentDirectory, path));
  }

  [TestMethod]
  public void Validate() {
    string resources = ResourcesPath();
    Assert.IsNotNull(resources);
    TestContext.WriteLine($"Json path: resources");
    int validated = 0;
    foreach (string file in Directory.EnumerateFiles(
                 resources, "*.json", SearchOption.AllDirectories)) {
      try {
        using StreamReader stream = File.OpenText(file);
        using JsonTextReader reader = new(stream);
        JToken.ReadFrom(reader);
      } catch (JsonException ex) {
        Assert.Fail("Validation failed for JSON file: {0}\n{1}", file, ex);
      }

      ++validated;
    }

    // Note, to run this test from the command line and see the log messages,
    // run:
    // dotnet test -c Debug --logger:"console;verbosity=detailed"
    TestContext.WriteLine($"Validated {validated} files");
  }
}
