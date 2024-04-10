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

        /// <summary>
        /// insert a message a position 0 to be shown on top of the list
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool InsertNewMessage(string item)
        {
            this.Insert(0,item);
            return true;
        }
    }
}
