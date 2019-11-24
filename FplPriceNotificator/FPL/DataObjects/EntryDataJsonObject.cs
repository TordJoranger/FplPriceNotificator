using System.Collections.Generic;
using Newtonsoft.Json;

namespace FplPriceNotificator.FPL.DataObjects
    {
    public class EntryDataJsonObject
        {
        [JsonProperty(PropertyName = "picks")]
        public List<Element> Elements { get; set; }
        }

        public class Element
        {
        [JsonProperty(PropertyName = "element")]
        public int Id { get; set; }
        }
    }

