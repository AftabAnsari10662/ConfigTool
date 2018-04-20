using ConfigurationTool.Models;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System;

namespace ConfigurationTool.Service
{
    public class ConfigurationService
    {
        public ConfigurationService()
        {

        }

        public List<ConfigurationParameter> QueryXml(string filePath)
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
                TagName = x.Element("TagName").Value,                
                Value = x.Element("Value").Value,
                SampleValue = x.Element("SampleValue").Value,
                Description = x.Element("Description").Value,
                VersionAdded = x.Element("VersionAdded").Value,
                VersionDeprecated = x.Element("VersionDeprecated").Value,
            };
        }

        public void SaveConfigurationParameters(
            List<ConfigurationParameter> configParameters, string path)
        {
            var document = new XDocument();
            var parameters = new XElement("ConfigurationParameters");
            foreach (var parameter in configParameters)
            {
                var configurationParameter = new XElement("ConfigurationParameter");
                var applicationName = new XElement("ApplicationName", parameter.ApplicationName);
                var tagName = new XElement("TagName", parameter.TagName);
                var value = new XElement("Value", parameter.Value);
                var sampleValue = new XElement("SampleValue", parameter.SampleValue);
                var description = new XElement("Description", parameter.Description);
                var versionAdded = new XElement("VersionAdded", parameter.VersionAdded);
                var versionDeprecated = new XElement("VersionDeprecated", parameter.VersionDeprecated);

                configurationParameter.Add(applicationName);
                configurationParameter.Add(tagName);
                configurationParameter.Add(value);
                configurationParameter.Add(sampleValue);
                configurationParameter.Add(description);
                configurationParameter.Add(versionAdded);
                configurationParameter.Add(versionDeprecated);
                parameters.Add(configurationParameter);

            }
            document.Add(parameters);
            document.Save(path);
        }

        public bool IsXmlFileContentNeedsToBeSaved(
          List<ConfigurationParameter> parametersFromUnmodifiedXmlFile,
          List<ConfigurationParameter> parameters)
        {
            var isParametersHaveBeenModified = parameters
                                         .SequenceEqual(parametersFromUnmodifiedXmlFile);
            return !isParametersHaveBeenModified;
        }

        public List<ConfigurationParameter> ReplaceValuesFromOldTaggedConfigurationIntoLatestTaggedConfiguration(
                List<ConfigurationParameter> oldTaggedConfigurations,
                List<ConfigurationParameter> latestTaggedConfigurations
            )
        {

            var taggedConfigValues = new List<ConfigurationParameter>();

            foreach (var tagConfig in latestTaggedConfigurations)
            {
                var oldTagConfig = oldTaggedConfigurations.FirstOrDefault(o => o.TagName == tagConfig.TagName);
                if (oldTagConfig!=null)
                {
                    tagConfig.Value = oldTagConfig.Value;
                }

            }

            return taggedConfigValues;
        }
    }
}
