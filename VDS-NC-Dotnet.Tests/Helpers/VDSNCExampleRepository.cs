using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace VDS_NC_Dotnet.Tests
{
    public class VDSNCExampleRepository
    {
        public VDSNCExampleRepository()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var resourceStream = assembly.GetManifestResourceStream(Path);

            if (resourceStream == null)
            {
                throw new Exception($"Resource [{Path}] was not found!");
            }

            using(var reader = new StreamReader(resourceStream))
            {
                var data = reader.ReadToEnd();

                this.ExampleData = JsonConvert.DeserializeObject<IEnumerable<VDS_NC>>(data);
            }
        }

        private const string Path = "VDS_NC_Dotnet.Tests.Helpers.VDS-NC-Examples.json";

        private IEnumerable<VDS_NC> ExampleData { get; }

        public IEnumerable<VDS_NC> GetExampleData()
        {
            return this.ExampleData;
        }
    }
}
