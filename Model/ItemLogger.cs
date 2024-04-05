using System.Collections.ObjectModel;

namespace URManager.Backend.Model
{
    public class ItemLogger : ObservableCollection<string>
    {
        public ItemLogger()
        {

        }
        /// <summary>
        /// Add any Information as string to the ObservableCollection
        /// </summary>
        /// <param name="item"></param>
        public ItemLogger(string item)
        {
            this.Add(item);
        }
    }
}
