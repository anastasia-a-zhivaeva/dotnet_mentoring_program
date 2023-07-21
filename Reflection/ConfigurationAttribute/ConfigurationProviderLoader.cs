using System.Reflection;
using System.Runtime.Loader;
using ConfigurationProviderBaseLibrary;

namespace ConfigurationAttribute
{
    public static class ConfigurationProviderLoader
    {
        public static Dictionary<ConfigurationProviderType, IConfigurationProvider> providers { get; private set; } = new Dictionary<ConfigurationProviderType, IConfigurationProvider>();
        public static readonly string ProvidersDirectory = "Providers";

        private readonly static string _providersPath = Path.Combine(Directory.GetCurrentDirectory(), ProvidersDirectory).ToString();

        static ConfigurationProviderLoader()
        {
            var providersLibraryFileInfos = new DirectoryInfo(_providersPath).GetFiles();
            if (providersLibraryFileInfos.Length == 0)
            {
                throw new DllNotFoundException($"Configuration Providers were not found in {ProvidersDirectory} directory");
            }
            
            foreach (var providerInfo in providersLibraryFileInfos)
            {
                var providerAssembly = LoadConfigurationProviderAssembly(providerInfo.FullName);
                foreach (var type in providerAssembly.GetTypes())
                {
                    if (typeof(IConfigurationProvider).IsAssignableFrom(type))
                    {
                        IConfigurationProvider? provider = (IConfigurationProvider?)Activator.CreateInstance(type);
                        if (provider != null)
                        {
                            providers.Add(EnumExtensions.GetValueFromDescription<ConfigurationProviderType>(type.Name), provider);
                        }
                    }
                }
            }
        }

        private static Assembly LoadConfigurationProviderAssembly(string path)
        {
            ConfigurationProviderLoadContext loadContext = new ConfigurationProviderLoadContext(path);
            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(path)));
        }

        private class ConfigurationProviderLoadContext : AssemblyLoadContext
        {
            private AssemblyDependencyResolver _resolver;

            public ConfigurationProviderLoadContext(string pluginPath)
            {
                _resolver = new AssemblyDependencyResolver(pluginPath);
            }

            protected override Assembly? Load(AssemblyName assemblyName)
            {
                string? assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
                if (assemblyPath != null)
                {
                    return LoadFromAssemblyPath(assemblyPath);
                }

                return null;
            }

            protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
            {
                string? libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
                if (libraryPath != null)
                {
                    return LoadUnmanagedDllFromPath(libraryPath);
                }

                return IntPtr.Zero;
            }
        }
    }

}
