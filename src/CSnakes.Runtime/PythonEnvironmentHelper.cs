using CSnakes.Runtime.EnvironmentManagement;
using CSnakes.Runtime.Locators;
using CSnakes.Runtime.PackageManagement;
using Microsoft.Extensions.Logging;

namespace CSnakes.Runtime;
public class PythonEnvironmentHelper
{
    public static IPythonEnvironment CreateEnvironment(string environmentName, string pythonPath = null, string modulePath = null, string requirements = "requirements.txt", string version = null, ILogger logger = default)
    {
        var pythonVersion = ServiceCollectionExtensions.ParsePythonVersion(version);
        var environmentPath = Path.Join(modulePath, $".{environmentName}");
        var pythonLocator = new FolderLocator(pythonPath, pythonVersion);
        var packageInstaller = new PipInstaller(logger, requirements);
        var environmentOptions = new PythonEnvironmentOptions(modulePath, []);
        var environmentManager = new VenvEnvironmentManagement(logger, environmentPath, true);
        return new PythonEnvironment([pythonLocator], [packageInstaller], environmentOptions, logger, environmentManager);
    }
}
