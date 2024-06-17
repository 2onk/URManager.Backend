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
        private object _data;

        /// <summary>
        /// Serialize Constructor
        /// </summary>
        /// <param name="savePath"></param>
        /// <param name="robots"></param>
        public Json(string savePath, object data)
        {
            _data = data;
            _savePath = savePath;
            _options = new JsonSerializerOptions();
            JsonOptions();
        }

        /// <summary>
        /// Deserilize Constructor
        /// </summary>
        /// <param name="data"></param>
        public Json(string savePath) 
        {
            _savePath = savePath;
        }

        /// <summary>
        /// Create Json file at desired save location and export Data
        /// </summary>
        /// <returns>true if succeded</returns>
        public bool CreateRobotJson() 
        {
            CreateJsonFile(_savePath);
            JsonSerializeRobots(_data);
            return true; 
        }

        /// <summary>
        /// import robot json file 
        /// </summary>
        /// <returns>true if succeded</returns>
        public List<Robot> ImportRobotJson()
        {
            if(_savePath is null) return new List<Robot>();
            string deserilizeString = OpenReadJsonFile(_savePath);

            if (deserilizeString is null) return new List<Robot>();
            List<Robot> robots = JsonDeserializeRobots(deserilizeString);

            return robots;
        }

        private void JsonOptions()
        {
            _options.WriteIndented = true;
            _options.PropertyNameCaseInsensitive = true;
        }

        private bool JsonSerializeRobots(object data)
        {
            JsonSerializer.Serialize(_stream, data, _options);
            _stream.Dispose();
            return true;
        }

        private List<Robot> JsonDeserializeRobots(string deserilizeString)
        {
            var robots = JsonSerializer.Deserialize<List<Robot>>(deserilizeString);
            if (robots is null) return new List<Robot>();
            return robots;
        }

        private bool CreateJsonFile(string savePath)
        {
            _stream = File.Create(savePath);
            return true;
        }

        private string OpenReadJsonFile(string savePath)
        {
            string _deserilizeString = File.ReadAllText(savePath);
            return _deserilizeString;
        }
    }
}