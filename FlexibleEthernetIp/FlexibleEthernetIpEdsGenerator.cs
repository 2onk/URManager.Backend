using System.Reflection;
using URManager.Backend.Model;

namespace URManager.Backend.FlexibleEthernetIp
{
    public class FlexibleEthernetIpEdsGenerator
    {
        private string _edsContent;
        private int _byteIndex = 1000;
        public FlexibleEthernetIpEdsGenerator(List<FlexibleEthernetIpBytes> inputs, List<FlexibleEthernetIpBytes> outputs)
        {
            Inputs = inputs;
            Outputs = outputs;
            _edsContent = string.Empty;
        }
        private List<FlexibleEthernetIpBytes> Inputs { get; set; }
        private List<FlexibleEthernetIpBytes> Outputs { get; set; }

        /// <summary>
        /// Get Eds fix snippet
        /// </summary>
        /// <param name="XmlRpcInit"></param>
        /// <returns>String with replaced sizes</returns>
        private string GenerateEdsFromSnippet(string resourceName)
        {
            string snippetContent = LoadSnippetFromResources(resourceName);
            return snippetContent;
        }

        /// <summary>
        /// Get input snippet and add all inputs  
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns>String with all inputs</returns>
        private string GenerateIoEdsFromSnippet(string resourceName, List<FlexibleEthernetIpBytes> ios)
        {
            string snippetContent = LoadSnippetFromResources(resourceName);
            string result = string.Empty;

            foreach (var ioByte in ios)
            {
                int byteIndex = ios.IndexOf(ioByte);

                string inputScript = snippetContent
                    .Replace("{byteIndex}", _byteIndex.ToString())
                    .Replace("{byteName}", "Byte" + byteIndex.ToString());

                _byteIndex++;
                result += inputScript + Environment.NewLine;
                result += Environment.NewLine;
            }
            return result;
        }

        /// <summary>
        /// Get input snippet and add all inputs  
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns>String with all inputs</returns>
        private string GenerateAssemblyEdsFromSnippet(string resourceName, List<FlexibleEthernetIpBytes> ios)
        {
            string snippetContent = LoadSnippetFromResources(resourceName);
            string result = string.Empty;
            string inputScript = string.Empty;

            result += snippetContent;

            for (int i = 0; i < ios.Count; i++)
            {

                if (i == ios.Count - 1)
                {
                   inputScript = "8,Param" + _byteIndex + ";";
                }
                else
                {
                   inputScript = "8,Param" + _byteIndex + ",";
                }

                _byteIndex++;
                result += inputScript + Environment.NewLine;
            }
            return result;
        }
        /// <summary>
        /// Load snippet and read all 
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns>string from file</returns>
        private string LoadSnippetFromResources(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null) return resourceName;
            using StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }


        /// <summary>
        /// Generate .urscript with bits and bytes names for FEIP
        /// </summary>
        /// <param name="outputFilePath"></param>
        public void SaveEdsToFile(string outputFilePath)
        {
            string scriptContent = GenerateEdsFromSnippet("URManager.Backend.Snippets.EDS.UniversalRobotsHeader.eds");
            AddNewContentToEds(scriptContent);
            scriptContent = GenerateIoEdsFromSnippet("URManager.Backend.Snippets.EDS.NewByteParamInputs.eds", Inputs);
            AddNewContentToEds(scriptContent);
            scriptContent = GenerateIoEdsFromSnippet("URManager.Backend.Snippets.EDS.NewByteParamOutputs.eds", Outputs);
            _byteIndex = 1000; //reste byteindex counter for EDS
            AddNewContentToEds(scriptContent);
            scriptContent = GenerateEdsFromSnippet("URManager.Backend.Snippets.EDS.AssemblyPartFix.eds");
            AddNewContentToEds(scriptContent);
            scriptContent = GenerateAssemblyEdsFromSnippet("URManager.Backend.Snippets.EDS.AssemblyPartRobotToScanner.eds", Inputs);
            AddNewContentToEds(scriptContent);
            scriptContent = GenerateAssemblyEdsFromSnippet("URManager.Backend.Snippets.EDS.AssemblyPartScannerToRobot.eds", Outputs);
            AddNewContentToEds(scriptContent);
            scriptContent = GenerateEdsFromSnippet("URManager.Backend.Snippets.EDS.UniversalRobotsEnd.eds");
            AddNewContentToEds(scriptContent);

            outputFilePath = Path.Combine(outputFilePath, "UniversalRobots+CustomConfig.eds");
            File.WriteAllText(outputFilePath, _edsContent);
        }

        private bool AddNewContentToEds(string newContent)
        {
            _edsContent = _edsContent + newContent;
            return true;
        }
    }
}
