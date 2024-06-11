using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using URManager.Backend.Model;

namespace URManager.Backend.JSON
{
    public class Json
    {
        private JsonSerializerOptions _options;
        private FileStream _stream;
        private string _savePath;
        private object _robots;

        public Json(string savePath, object robots)
        {
            _robots = robots;
            _savePath = savePath;
            _options = new JsonSerializerOptions();
            JsonOptions();
        }

        /// <summary>
        /// Create Json file at desired save location and export Data
        /// </summary>
        /// <returns>true if succeded</returns>
        public bool CreateRobotJson() 
        {
            CreateJsonFile(_savePath);
            JsonSerializeRobots(_robots);
            return true; 
        }

        internal void JsonOptions()
        {
            _options.WriteIndented = true;
            _options.PropertyNameCaseInsensitive = true;
        }

        internal bool JsonSerializeRobots(object robots)
        {
            JsonSerializer.Serialize(_stream, robots, _options);
            _stream.Dispose();
            return true;
        }

        internal bool CreateJsonFile(string savePath)
        {
            _stream = File.Create(savePath);
            return true;
        }


    }
}