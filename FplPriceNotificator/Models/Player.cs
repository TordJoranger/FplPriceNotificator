namespace FplPriceNotificator.Models
    {
    public class Player
        {

            public int PlayerId { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
            public double PriceChange { get; set; }

            public bool IsAboutToRise(Threshold threshold)
            {
                return PriceChange > (int)threshold;
            }

            public bool IsAboutToDrop(Threshold threshold)
            {
               return PriceChange < (int)threshold*-1;
            }
        }
    }
