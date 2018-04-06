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

        public void SaveConfigurationParameters(
            List<ConfigurationParameter> configParameters, string path)
        {
            var document = new XDocument();
            var parameters = new XElement("ConfigurationParameters");
            foreach (var parameter in configParameters)
            {
                var configurationParameter = new XElement("ConfigurationParameter");
                var name = new XElement("Name", parameter.Name);
                var value = new XElement("Value", parameter.Value);
                var description = new XElement("Description", parameter.Description);
                var includeVersion = new XElement("IncludeVersion", parameter.IncludeVersion);
                var decrementVersion = new XElement("DecrementVersion", parameter.DecrementVersion);
                configurationParameter.Add(name);
                configurationParameter.Add(value);
                configurationParameter.Add(description);
                configurationParameter.Add(includeVersion);
                configurationParameter.Add(decrementVersion);
                parameters.Add(configurationParameter);

            }
            document.Add(parameters);
            document.Save("configurationParameters.xml");
        }
    }
}
