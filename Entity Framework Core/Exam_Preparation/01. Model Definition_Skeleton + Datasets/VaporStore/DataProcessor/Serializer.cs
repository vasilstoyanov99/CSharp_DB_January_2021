using System.Linq;
using Newtonsoft.Json;
using VaporStore.Data.Models.Enums;
using VaporStore.DataProcessor.Dto.Export;

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
                .Where(u => u.Cards.Any(c => c.Purchases.Any()))
                .ToArray()
                .Select(u => new UserPurchasesByTypeExportModel()
                {
                    Username = u.Username,
                    Purchases = context
                        .Purchases
                        .ToArray()
                        .Where(p => p.Card.User.Username == u.Username 
                                    && p.Type == purchaseType)
                        .OrderBy(p => p.Date)
                        .Select(p => new PurchaseExportModel()
                        {

                        }
                })
		}
	}
}