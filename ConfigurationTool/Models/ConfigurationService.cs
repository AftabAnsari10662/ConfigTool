using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace ConfigurationTool.Models
{
    public class ConfigurationService
    {
        public ConfigurationService()
        {

        }

        public IEnumerable<ConfigurationParameter> QueryXml(string filePath)
        {
            var document = XDocument.Load(filePath);
            var configurationParameters =
                document
                .Element("ConfigurationParameters")
                .Elements("ConfigurationParameter")
                .Select(TransformToConfigurationParameter)
                .ToList();

            return configurationParameters;
        }

        private ConfigurationParameter TransformToConfigurationParameter(XElement x)
        {
            return new ConfigurationParameter
            {
                ApplicationName = x.Element("ApplicationName").Value,
                KeyName = x.Element("KeyName").Value,
                Description = x.Element("Description").Value,
                Value = x.Element("Value").Value,
                TaggedValue = x.Element("TaggedValue").Value,
                DecrementVersion = x.Element("DecrementVersion").Value,
                IncludeVersion = x.Element("IncludeVersion").Value,
            };
        }

        public void SaveConfigurationParameters(
            List<ConfigurationParameter> configParameters, string path)
        {
            var filePath = path.Replace(@"\\", @"\");
            var document = new XDocument();
            var parameters = new XElement("ConfigurationParameters");
            foreach (var parameter in configParameters)
            {
                var configurationParameter = new XElement("ConfigurationParameter");
                var applicationName = new XElement("ApplicationName", parameter.ApplicationName);
                var keyName = new XElement("KeyName", parameter.KeyName);
                var value = new XElement("Value", parameter.Value);
                var taggedValue = new XElement("TaggedValue", parameter.TaggedValue);
                var description = new XElement("Description", parameter.Description);
                var includeVersion = new XElement("IncludeVersion", parameter.IncludeVersion);
                var decrementVersion = new XElement("DecrementVersion", parameter.DecrementVersion);
                configurationParameter.Add(applicationName);
                configurationParameter.Add(keyName);
                configurationParameter.Add(value);
                configurationParameter.Add(taggedValue);
                configurationParameter.Add(description);
                configurationParameter.Add(includeVersion);
                configurationParameter.Add(decrementVersion);
                parameters.Add(configurationParameter);

            }
            document.Add(parameters);
            document.Save("test90.xml", SaveOptions.None);
        }
    }
}
