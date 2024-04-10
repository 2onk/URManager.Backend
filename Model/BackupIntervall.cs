namespace URManager.Backend.Model
{
    public class BackupIntervall
    {

        public string Name { get; }
        public int Intervall { get; }

        public BackupIntervall(string name, int intervall)
        {
            Name = name;
            Intervall = intervall;
        }
    }
}
