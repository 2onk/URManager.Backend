using System.Collections.ObjectModel;
using URManager.Backend.Model;

namespace URManager.Backend.Data
{
    public interface IBackupDataProvider
    {
        IEnumerable<BackupIntervall> GetAll();
    }

    public class BackupDataProvider : IBackupDataProvider
    {
        public IEnumerable<BackupIntervall> GetAll()
        {
            return new List<BackupIntervall>
            {
                new BackupIntervall("1 day", 1),
                new BackupIntervall("7 days", 7),
                new BackupIntervall("14 days", 14),
                new BackupIntervall("31 days", 31),
            };
        }
    }
}
