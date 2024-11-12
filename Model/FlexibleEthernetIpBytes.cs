using System.Collections.ObjectModel;

namespace URManager.Backend.Model
{
    public class FlexibleEthernetIpBytes
    {
        public FlexibleEthernetIpBytes(int byteIndex)
        {
            Bits = new ObservableCollection<FlexibleEthernetIpBit>();
            
            for (int i = 0; i < 8; i++)
            {
                Bits.Add(new FlexibleEthernetIpBit(i, $"Byte{byteIndex}_Bit{i}"));
            }
        }
        public ObservableCollection<FlexibleEthernetIpBit> Bits { get; }
        
    }
}
