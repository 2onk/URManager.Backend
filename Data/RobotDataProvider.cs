﻿using URManager.Backend.Model;

namespace URManager.Backend.Data
{
    public interface IRobotDataProvider
    {
        Task<IEnumerable<Robot>?> GetAll();
    }

    public class RobotDataProvider : IRobotDataProvider
    {
        /// <summary>
        /// Dummy robot data 
        /// </summary>
        /// <returns>IEnumerable of type Robot</returns>
        public async Task<IEnumerable<Robot>?> GetAll()
        {
            await Task.Delay(100);

            return new List<Robot>
            {
                new Robot(1, "Handling", "10.10.10.10"),
                //new Robot{Id=2, RobotName="Handling2",IP="192.168.1.11"},
                //new Robot{Id=3, RobotName="Handling3",IP="192.168.1.12"},
                //new Robot{Id=4, RobotName="Palletizer",IP="192.168.1.13"},
                //new Robot{Id=5, RobotName="Palletizer2",IP="192.168.1.14"},
                //new Robot{Id=6, RobotName="Palletizer3",IP="192.168.1.15"}
            };
        }
    }
}
