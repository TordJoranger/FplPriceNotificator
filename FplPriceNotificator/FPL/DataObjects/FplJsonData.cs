using System.Collections.Generic;
using Newtonsoft.Json;

namespace FplPriceNotificator.Services.FPL.DataObjects
    {
    public class FplJsonData
        {
            [JsonProperty(PropertyName = "elements")]
            public List<PlayerJsonData> PlayerList { get; set; }

        }
        public class PlayerJsonData
        {
            [JsonProperty(PropertyName = "id")]
            public int Id { get; set; }

            [JsonProperty(PropertyName = "first_name")]
            public string FirstName { get; set; }
            [JsonProperty(PropertyName = "second_name")]
            public string LastName { get; set; }
            [JsonProperty(PropertyName = "code")]
            public int Code { get; set; }

        }
    }
