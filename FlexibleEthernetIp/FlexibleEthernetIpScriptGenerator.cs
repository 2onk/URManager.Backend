using System.IO;
using System.Reflection;
using URManager.Backend.Model;

namespace URManager.Backend.FlexibleEthernetIp
{
    public class FlexibleEthernetIpScriptGenerator
    {
        private string _scriptContent;
        public FlexibleEthernetIpScriptGenerator(List<FlexibleEthernetIpBytes> inputs, List<FlexibleEthernetIpBytes> outputs) 
        {
            Inputs = inputs;
            Outputs = outputs;
            _scriptContent = string.Empty;
        }

        private List<FlexibleEthernetIpBytes> Inputs { get; set; }
        private List<FlexibleEthernetIpBytes> Outputs { get; set; }

        /// <summary>
        /// Get xmlrpc snippet and exchange params
        /// </summary>
        /// <param name="XmlRpcInit"></param>
        /// <returns>String with replaced sizes</returns>
        private string GenerateScriptFromSnippet(string resourceName)
        {
            string snippetContent = LoadSnippetFromResources(resourceName);

            string initScript = snippetContent
                .Replace("{size_inputs}", Inputs.Count.ToString())
                .Replace("{size_outputs}", Outputs.Count.ToString());

            return initScript;
        }

        /// <summary>
        /// Get example methods with custom names which will be also availe in urp
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns>String with replaced sizes</returns>
        private string GenerateExampleMethodsFromSnippet(string resourceName)
        {
            string snippetContent = LoadSnippetFromResources(resourceName);

            string initScript = snippetContent
                .Replace("{inbitName1}", Inputs[0].Bits[0].BitName)
                .Replace("{inbitName2}", Inputs[0].Bits[1].BitName)
                .Replace("{outbitName1}", Outputs[0].Bits[0].BitName)
                .Replace("{outbitName2}", Outputs[0].Bits[1].BitName);

            return initScript;
        }


        /// <summary>
        /// Get input snippet and add all inputs  
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns>String with all inputs</returns>
        private string GenerateIoScriptFromSnippet(string resourceName, List<FlexibleEthernetIpBytes> ios )
        {
            string snippetContent = LoadSnippetFromResources(resourceName);
            string result = string.Empty;

            foreach (var ioByte in ios)
            {
                int byteIndex = ios.IndexOf(ioByte);

                for (int bitIndex = 0; bitIndex < ioByte.Bits.Count; bitIndex++)
                {
                    var bit = ioByte.Bits[bitIndex];

                    string inputScript = snippetContent
                        .Replace("{byteIndex}", byteIndex.ToString())
                        .Replace("{bitIndex}", bitIndex.ToString())
                        .Replace("{bitName}", bit.BitName);

                    result += inputScript + Environment.NewLine;
                }

                result += Environment.NewLine;
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
        /// Get embedded .urp and save it in the same directory as .script
        /// </summary>
        /// <param name="savePath"></param>
        private void SaveUrp(string savePath)
        {
            string directory = Path.GetDirectoryName(savePath);
            string newPath = Path.Combine(directory, "FlexibleEthernetIpExample.urp");
            var assembly = Assembly.GetExecutingAssembly();
            using Stream resourceStream = assembly.GetManifestResourceStream("URManager.Backend.Snippets.URP.FlexibleEthernetIpExample.urp");
            if (resourceStream == null) return;
            using (FileStream fileStream = new FileStream(newPath, FileMode.Create, FileAccess.Write))
            {
                resourceStream.CopyTo(fileStream);
            }
        }

        /// <summary>
        /// Generate .urscript with bits and bytes names for FEIP
        /// </summary>
        /// <param name="outputFilePath"></param>
        public void SaveScriptToFile(string outputFilePath)
        {
            string scriptContent = GenerateScriptFromSnippet("URManager.Backend.Snippets.URScript.XmlRpcInit.script");
            AddNewContentToScript(scriptContent);
            scriptContent = GenerateScriptFromSnippet("URManager.Backend.Snippets.URScript.MappingHeader.script");
            AddNewContentToScript(scriptContent);
            scriptContent = GenerateIoScriptFromSnippet("URManager.Backend.Snippets.URScript.MappingInputs.script", Inputs);
            AddNewContentToScript(scriptContent);
            scriptContent = LoadSnippetFromResources("URManager.Backend.Snippets.URScript.MappingOutputsComment.script");
            AddNewContentToScript(scriptContent);
            scriptContent = GenerateIoScriptFromSnippet("URManager.Backend.Snippets.URScript.MappingOutputs.script", Outputs);
            AddNewContentToScript(scriptContent);
            scriptContent = LoadSnippetFromResources("URManager.Backend.Snippets.URScript.MappingEnd.script");
            AddNewContentToScript(scriptContent);
            scriptContent = GenerateExampleMethodsFromSnippet("URManager.Backend.Snippets.URScript.ExampleMethods.script");
            AddNewContentToScript(scriptContent);
            File.WriteAllText(outputFilePath, _scriptContent);

            SaveUrp(outputFilePath);
        }

        private bool AddNewContentToScript(string newContent)
        {
            _scriptContent = _scriptContent + newContent;
            return true;
        }
    }
}
