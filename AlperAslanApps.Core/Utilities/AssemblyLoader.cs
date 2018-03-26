using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AlperAslanApps.Core.Utilities
{
    public static class AssemblyLoader
    {
        [DebuggerStepThrough]
        public static IEnumerable<Type> LoadExportedTypes(string assemblyName) =>
            from assembly in SearchAssemblies(assemblyName)
            from type in assembly.GetExportedTypes()
            select type;

        [DebuggerStepThrough]
        public static IEnumerable<Assembly> SearchAssemblies(string filter) =>
            from file in new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).GetFiles()
            where file.Extension.ToLower() == ".dll"
            where file.Name.ToLower().Contains(filter.ToLower())
            select Assembly.Load(AssemblyName.GetAssemblyName(file.FullName));

    }
}
