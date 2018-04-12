using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationTool.Models
{
    public class ConfigurationParameter
    {
        public string TagName { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string IncludeVersion { get; set; }
        public string DecrementVersion { get; set; }
    }
}
