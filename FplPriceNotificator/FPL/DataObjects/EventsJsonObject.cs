using Newtonsoft.Json;

namespace FplPriceNotificator.Services.FPL.DataObjects
    {
    internal class EventsJsonObject
        {
        [JsonProperty(PropertyName = "id")]
        public int Gameweek { get; set; }

        [JsonProperty(PropertyName = "is_current")]
        public bool IsCurrent { get; set; }

        }
    }