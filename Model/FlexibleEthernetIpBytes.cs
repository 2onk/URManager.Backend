using System.Collections.ObjectModel;

namespace URManager.Backend.Model
{
    public class FlexibleEthernetIpBytes
    {
        public FlexibleEthernetIpBytes()
        {
            Bits = new ObservableCollection<FlexibleEthernetIpBit>();
           
            for (int i = 0; i < 8; i++)
            {
                Bits.Add(new FlexibleEthernetIpBit(i, $"Bit{i + 1}"));
            }
        }
        public ObservableCollection<FlexibleEthernetIpBit> Bits { get; }
        
    }
}
