namespace URManager.Backend.Model
{
    public class Robot
    {
        public Robot(int id, string robotName, string ip , bool backup=true, bool update=false) 
        {
            Id = id;
            RobotName = robotName;
            IP = ip;
            Backup = backup;
            Update = update;
        }

        public int Id { get; set; }
        public string RobotName { get; set; }
        public string IP { get; set; }
        public bool Backup {  get; set; }
        public bool Update { get; set; }
    }
}
