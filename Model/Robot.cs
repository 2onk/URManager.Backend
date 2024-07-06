using System.Security;

namespace URManager.Backend.Model
{
    public class Robot
    {
        public Robot(int id, string robotName, string ip , bool backup=true, bool update=false, string adminPassword = "easybot") 
        {
            Id = id;
            RobotName = robotName;
            IP = ip;
            Backup = backup;
            Update = update;
            AdminPassword = adminPassword;
        }

        public int Id { get; set; }
        public string RobotName { get; set; }
        public string IP { get; set; }
        public bool Backup {  get; set; }
        public bool Update { get; set; }
        public string AdminPassword {get; set; }
    }
}
