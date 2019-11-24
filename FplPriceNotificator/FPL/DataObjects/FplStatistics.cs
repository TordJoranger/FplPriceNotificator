using System.Collections.Generic;
using Newtonsoft.Json;

namespace FplPriceNotificator.Services.FPL.DataObjects
    {
    public class FplStatistics
        {
            [JsonProperty(PropertyName = "iTotalRecords")]
            public int TotalNumberOfPlayers { get; set; }
            [JsonProperty(PropertyName = "aaData")]
            public List<List<string>> PlayersListsList { get; set; }
        }
    }
