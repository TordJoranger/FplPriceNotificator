using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using FplPriceNotificator.Models;
using Newtonsoft.Json;
using RestSharp;

namespace FplPriceNotificator.Services.FPL
    {
    public static class FplStatisticsService
        {
        private const int IndexPlayerName = 1;
        private const int IndexPlayerPrice = 6;
        private const int IndexPlayerPriceChange = 12;

        public static List<Player> Players()
            {
            var iselRow = GetIselRow();
            var fplStatistics = GetFplStatistics(0, 0, iselRow);
            return ExtractPlayers(fplStatistics).ToList();
            }


        private static int GetIselRow()
            {
            var client = new RestClient("http://www.fplstatistics.co.uk");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            var html = response.Content;
            var index = html.IndexOf("iselRow", StringComparison.Ordinal);
            string iselRowString = html.Substring(index, 30);
            return int.Parse(Regex.Replace(iselRowString, @"[^\d]", String.Empty));
            }

        private static IEnumerable<Player> ExtractPlayers(FPL.DataObjects.FplStatistics statistics)
            {
            var players = new List<Player>();
            statistics.PlayersListsList.ForEach(list =>
            {
                var player = new Player { Name=list[IndexPlayerName] };
                var format = new NumberFormatInfo { NumberDecimalSeparator="." };
                var priceString = list[IndexPlayerPrice];
                player.Price=double.Parse(priceString, format);
                var priceChangeString = list[IndexPlayerPriceChange];
                player.PriceChange=double.Parse(priceChangeString, format);
                players.Add(player);
            });
            return players;
            }

        private static FPL.DataObjects.FplStatistics GetFplStatistics(int offset, int length, int iselRow)
            {
            var url = $"http://www.fplstatistics.co.uk/Home/AjaxPricesCHandler?iselRow={iselRow}&_=1539075258990";
            var wc = new WebClient { Proxy=null };
            var json = wc.DownloadString(url);
            return JsonConvert.DeserializeObject<FPL.DataObjects.FplStatistics>(json);
            }

        }
    }
