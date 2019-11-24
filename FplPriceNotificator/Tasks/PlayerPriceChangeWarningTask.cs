using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FplPriceNotificator.Data;
using FplPriceNotificator.Models;
using FplPriceNotificator.Scheduler;
using FplPriceNotificator.Services.FPL;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FplPriceNotificator.Tasks
    {
    public class PlayerPriceChangeWarningTask : IScheduledTask
        {
        private readonly IEmailSender _emailSender;
        private readonly IServiceProvider _serviceProvider;
        public PlayerPriceChangeWarningTask(
            IEmailSender emailSender,
            IServiceProvider serviceProvider)
            {
            _emailSender=emailSender;
            _serviceProvider=serviceProvider;
            }
        public string Schedule => "3 22 * * *";

        public async Task ExecuteAsync(CancellationToken cancellationToken)
            {               
                using (var scope = _serviceProvider.CreateScope())
                    {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var emailInfoList = context.EmailInfo.ToList();

                    var entryInfoList = FplService.GetEntryInfo(emailInfoList);

                    foreach (var entryInfo in entryInfoList)
                        {
                        if(entryInfo.Risers.Count > 0 || entryInfo.Droppers.Count > 0)
                             await SendMessageToUserEmail(entryInfo.Email, entryInfo.Risers, entryInfo.Droppers);
                        }
                    }
            }

        private Task SendMessageToUserEmail(string userEmail, List<Player> risers, List<Player> droppers)
            {
            return _emailSender.SendEmailAsync(userEmail,
                "Players at risk",
                CreateMessage(risers, droppers));
            }

        private static string CreateMessage(List<Player> risers, List<Player> droppers)
            {
            var builder = new StringBuilder();
            builder.Append("<html><body>");
            CreateTable(droppers, builder,"Could drop");
            builder.Append("<br><br>");
            CreateTable(risers,builder,"Could rise");
            builder.Append("</body></html>");
            return builder.ToString();
            }

        private static void CreateTable(List<Player> players, StringBuilder builder, string title)
            {
            builder.Append($"<h3>{title}</h3>");
            builder.Append("<table style='table-layout:fixed'>");
            builder.Append("<tr style='text-align:left'>");    
            builder.Append("<th>Spiller</th>");
            builder.Append("<th>Pris</th>");
            builder.Append("<th>Prisendring</th></tr>");
            players.ForEach(p =>
            {
                builder.Append("<tr>");
                builder.Append($"<td>{p.Name}</td>");
                builder.Append($"<td>{p.Price}</td>");
                builder.Append($"<td>{p.PriceChange}</td>");
                builder.Append("</tr>");
            });
            builder.Append("</table>");
            }
        }
    }
