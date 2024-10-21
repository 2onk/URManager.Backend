namespace URManager.Backend.Model
{
    public class FlexibleEthernetIpBit
    {
        public FlexibleEthernetIpBit(int bitIndex, string bitName) 
        {
            BitIndex = bitIndex;
            BitName = bitName;
        }

        public int BitIndex { get; private set; }
        public string BitName { get; set; }
    }
}
