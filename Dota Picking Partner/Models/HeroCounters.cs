namespace Dota_Picking_Partner.Models
{

    public class HeroCounters
    {
        public Dictionary<string, object>? heroOneCounters { get; set; }
        public Dictionary<string, object>? heroTwoCounters { get; set; }
        public Dictionary<string, object>? heroThreeCounters { get; set; }
        public Dictionary<string, object>? heroFourCounters { get; set; }
        public Dictionary<string, object>? heroFiveCounters { get; set; }
    }

    public class CombinedHeroCounters
    {
        public Dictionary<string, object>? combinedHeroCounters { get; set; }
    }

    

}
