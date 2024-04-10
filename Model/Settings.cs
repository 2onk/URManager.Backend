namespace URManager.Backend.Model
{
    public class Settings
    {
        public Settings()
        {
            SelectedsavePath = "";
            IsBackupSelected = true;
            SelectedBackupIntervall = new("7 days", 7);
        }
        public string SelectedsavePath { get; set; }
        public bool IsBackupSelected { get; set; }
        public bool IsUpdateSelected { get; set; }
        public BackupIntervall SelectedBackupIntervall{ get; set; }
    }
}
