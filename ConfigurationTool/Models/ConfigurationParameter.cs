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
        public string KeyName { get; set; }
        public string TaggedValue { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string IncludeVersion { get; set; }
        public string DecrementVersion { get; set; }
    }
}
