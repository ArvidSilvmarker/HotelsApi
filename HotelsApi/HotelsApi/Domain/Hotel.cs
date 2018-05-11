namespace HotelsApi.Domain
{
    public class Hotel
    {
        public string Name { get; set; }
        public Region Region { get; set; }
        public int RoomsAvailable { get; set; }
        public int RegionValue { get; set; }
    }
}