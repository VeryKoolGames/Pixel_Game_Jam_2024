namespace DefaultNamespace
{
    public class Fish
    {
        public FishTypes FishType { get; private set; }
        public string FishName { get; private set; }
        public string FishDescription { get; private set; }
        public int FishRarety { get; private set; }
        
        public Fish(FishTypes fishType, string fishName, int fishRarety, string fishDescription)
        {
            this.FishType = fishType;
            this.FishName = fishName;
            this.FishRarety = fishRarety;
            this.FishDescription = fishDescription;
        }
    }
}