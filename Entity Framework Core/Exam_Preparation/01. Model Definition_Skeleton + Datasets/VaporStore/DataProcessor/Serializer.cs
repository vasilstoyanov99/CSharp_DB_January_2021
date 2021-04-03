using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VaporStore.Data.Models.Enums;
using VaporStore.DataProcessor.Dto.Export;
using XmlFacade;

namespace VaporStore.DataProcessor
{
	using System;
	using Data;

	public static class Serializer
	{
		public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {
            var sortedGenres = context
                .Genres
                .ToList()
                .Where(gn => genreNames.Contains(gn.Name))
                .Select(gn => new
                {
                    gn.Id,
                    Genre = gn.Name,
                    Games = gn.Games.Where(g => g.Purchases.Any()).Select(g => new
                        {
                            g.Id,
                            Title = g.Name,
                            Developer = g.Developer.Name,
                            Tags = String.Join(", ", g.GameTags.Select(x => x.Tag.Name)),
                            Players = g.Purchases.Count
                        })
                        .OrderByDescending(g => g.Players)
                        .ThenBy(g => g.Id)
                        .ToList(),
                    TotalPlayers = gn.Games.Sum(g => g.Purchases.Count)
                })
                .OrderByDescending(gn => gn.TotalPlayers)
                .ThenBy(gn => gn.Id)
                .ToList();

            var json = JsonConvert.SerializeObject(sortedGenres, Formatting.Indented);
            return json;
        }

		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
        {
            var purchaseType = Enum.Parse<PurchaseType>(storeType);

            var sortedUsers = context
                .Users
                .ToList()
                .Where(u => u.Cards.Any(c => c.Purchases
                    .Any(p => p.Type.ToString() == storeType)))
                .Select(u => new UserPurchasesByTypeExportModel()
                {
                    Username = u.Username,
                    TotalSpent = u.Cards
                        .Sum(c => c.Purchases.Where(p => p.Type.ToString() == storeType)
                            .Sum(p => p.Game.Price)),
                    Purchases = u.Cards.SelectMany(p => p.Purchases)
                        .Where(p => p.Type.ToString() == storeType)
                        .Select(p => new PurchaseExportModel()
                        {
                            Card = p.Card.Number,
                            Cvc = p.Card.Cvc,
                            Date = p.Date.ToString("yyyy-MM-dd HH:mm",
                                CultureInfo.InvariantCulture),
                            Game = new GameExportModel()
                            {
                                Title = p.Game.Name,
                                Genre = p.Game.Genre.Name,
                                Price = p.Game.Price
                            }
                        })
                        .OrderBy(p => p.Date)
                        .ToArray()
                })
                .OrderByDescending(x => x.TotalSpent)
                .ThenBy(x => x.Username)
                .ToList();

            var xmlResult = XmlConverter.Serialize(sortedUsers, "Users");
            return xmlResult;
        }
	}
}