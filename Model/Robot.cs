namespace URManager.Backend.Model
{
    public class Robot
    {
        public Robot(int id, string robotName, string ip) 
        {
            Id = id;
            RobotName = robotName;
            IP = ip;
        }

        public int Id { get; set; }
        public string RobotName { get; set; }
        public string IP { get; set; }
    }
}
