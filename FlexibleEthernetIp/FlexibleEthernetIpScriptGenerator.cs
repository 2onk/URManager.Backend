using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using URManager.Backend.Model;

namespace URManager.Backend.FlexibleEthernetIp
{
    public class FlexibleEthernetIpScriptGenerator
    {
        public FlexibleEthernetIpScriptGenerator(List<FlexibleEthernetIpBytes> inputs, List<FlexibleEthernetIpBytes> outputs) 
        {
            Inputs = inputs;
            Outputs = outputs;
        }

        private List<FlexibleEthernetIpBytes> Inputs { get; set; }
        private List<FlexibleEthernetIpBytes> Outputs { get; set; }


        /// <summary>
        /// Get snippet and exchange size  
        /// </summary>
        /// <param name="XmlRpcInit"></param>
        /// <returns>String with replaced sizes</returns>
        private string GenerateInitScriptFromSnippet(string resourceName)
        {
            // Snippet-Datei einlesen
            string snippetContent = LoadSnippetFromResources(resourceName);

            // Berechne die Größen von Inputs und Outputs
            int sizeInputs = Inputs.Count;
            int sizeOutputs = Outputs.Count;

            // Ersetze Platzhalter im Snippet
            string initScript = snippetContent
                .Replace("{size_inputs}", sizeInputs.ToString())
                .Replace("{size_outputs}", sizeOutputs.ToString());

            return initScript;
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

        // Methode zum Speichern des generierten Skripts in einer Datei
        public void SaveScriptToFile(string outputFilePath)
        {
            string scriptContent = GenerateInitScriptFromSnippet("URManager.Backend.Snippets.URScript.XmlRpcInit.script");
            File.WriteAllText(outputFilePath, scriptContent);
            //Console.WriteLine("URScript-Datei erfolgreich generiert!");
        }
    }
}
