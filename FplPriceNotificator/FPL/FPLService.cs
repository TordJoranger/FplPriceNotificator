using System.Collections.Generic;
using System.Linq;
using FplPriceNotificator.FPL.DataObjects;
using FplPriceNotificator.Models;
using FplPriceNotificator.Services.FPL.DataObjects;
using Newtonsoft.Json;
using RestSharp;

namespace FplPriceNotificator.Services.FPL
    {
    public static class FplService
        {
        public static List<EntryInfo> GetEntryInfo(IEnumerable<EmailInfo> emailInfoList)
            {
            var players = FplStatisticsService.Players();
            var entryInfoList = new List<EntryInfo>();

            foreach (var emailInfo in emailInfoList)
                {
                var names = GetPlayerNames(emailInfo.Entry);
                var entryPlayersAtRisk = players.Where(p => names.Contains(p.Name)).ToList();
                entryInfoList.Add(new EntryInfo
                    {
                    Droppers=entryPlayersAtRisk.Where(p => p.IsAboutToDrop(emailInfo.Threshold)).ToList(),
                    Risers=players.Where(p => p.IsAboutToRise(emailInfo.Threshold)).ToList(),
                    Email= emailInfo.Email
                    });
                }
            return entryInfoList;
            }

        private static List<string> GetPlayerNames(int entry)
        {
            var gw = GetCurrentGameWeek();
            var playerIds = GetPlayerIds(entry,gw);
            /*api to get data about all players*/
            var url = "https://fantasy.premierleague.com/drf/bootstrap-static";

            return GetJsonResult<FplJsonData>(url).PlayerList
                .Where(pd => playerIds.Contains(pd.Id))
                .Select(pd => pd.LastName)
                .ToList();
            }

        private static List<int> GetPlayerIds(int entry, int gw)
            {
            /*api to get info about an entry based on game week, players played, points etc*/
            var url = $"https://fantasy.premierleague.com/drf/entry/{entry}/event/{gw}/picks";
            var entryData = GetJsonResult<EntryDataJsonObject>(url);   
            return entryData.Elements.Select(e => e.Id).ToList();
            }

            private static int GetCurrentGameWeek()
            {   /*api for metadata about all game weeks, used to find which is the current*/
                var url = "https://fantasy.premierleague.com/drf/events";
                var json = GetJsonResult<List<EventsJsonObject>>(url);
                return json.First(e => e.IsCurrent).Gameweek;
            }

            private static T GetJsonResult<T>(string url)
            {
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/json");
                IRestResponse response = client.Execute(request);
                var json = response.Content;
                return JsonConvert.DeserializeObject<T>(json);
            }
        }
    }
