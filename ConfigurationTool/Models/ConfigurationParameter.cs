using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationTool.Models
{
    public class ConfigurationParameter
    {
        public string ApplicationName { get; set; }
        public string TagName { get; set; }
        public string Value { get; set; }
        public string SampleValue { get; set; }
        public string Description { get; set; }
        public string VersionAdded { get; set; }
        public string VersionDeprecated { get; set; }
    }
}
