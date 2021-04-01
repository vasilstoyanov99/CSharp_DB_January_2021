using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using VaporStore.Data.Models;
using VaporStore.Data.Models.Enums;
using VaporStore.DataProcessor.Dto.Import;

namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data;

	public static class Deserializer
	{
		public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            var gamesDTOs = JsonConvert
                .DeserializeObject<List<GamesImportModel>>(jsonString);

            var games = new List<Game>();
            var gameTags = new List<GameTag>();
            var tags = new List<Tag>();
            var devs = new List<Developer>();
            var genres = new List<Genre>();
            var result = new StringBuilder();

            foreach (var gameDTO in gamesDTOs)
            {
                if (!IsValid(gameDTO) || gameDTO.Tags.Count == 0)
                {
                    result.AppendLine("Invalid Data");
					continue;
                }

                DateTime releaseDate;
                bool isReleaseDateValid = DateTime.TryParseExact(gameDTO.ReleaseDate, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDate);

                if (!isReleaseDateValid)
                {
                    result.AppendLine("Invalid Data");
                    continue;
                }

                var game = new Game()
                {
                    Name = gameDTO.Name,
                    Price = gameDTO.Price,
                    ReleaseDate = releaseDate,
                };

                var developer = devs
                    .FirstOrDefault(x => x.Name == gameDTO.Developer);

                if (developer == null)
                {
                    developer = new Developer()
                    {
                        Name = gameDTO.Developer
                    };

                    devs.Add(developer);
                    game.Developer = developer;
                }
                else
                {
                    game.Developer = developer;
                }

                foreach (var tagName in gameDTO.Tags)
                {
                    var tag = tags.FirstOrDefault(x => x.Name == tagName);

                    if (tag == null)
                    {
                        tag = new Tag()
                        {
                            Name = tagName
                        };

                        tags.Add(tag);

                        var gameTag = new GameTag()
                        {
                            Game = game,
                            Tag = tag
                        };

                        gameTags.Add(gameTag);
                        game.GameTags.Add(gameTag);
                    }
                    else
                    {
                        var gameTag = gameTags.FirstOrDefault(x => x.Tag.Name == tagName);
                        game.GameTags.Add(gameTag);
                    }
                }

                var genre = genres
                    .FirstOrDefault(x => x.Name == gameDTO.Genre);

                if (genre == null)
                {
                    genre = new Genre()
                    {
                        Name = gameDTO.Genre
                    };

                    genres.Add(genre);
                    game.Genre = genre;
                }
                else
                {
                    game.Genre = genre;
                }

                

                games.Add(game);
                result.AppendLine($"Added {game.Name} ({game.Genre.Name})" +
                                  $" with {game.GameTags.Count} tags"!);
            }

            context.Developers.AddRange(devs);
            context.GameTags.AddRange(gameTags);
            context.Tags.AddRange(tags);
            context.Genres.AddRange(genres);
            context.Games.AddRange(games);
            context.SaveChanges();
            return result.ToString().Trim();
        }

		public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            var usersDTOs = JsonConvert
                .DeserializeObject<List<UserAndCardsImportModel>>(jsonString);

            var result = new StringBuilder();

            foreach (var userDTO in usersDTOs)
            {
                if (!IsValid(userDTO) || !userDTO.Cards.Any(IsValid) 
                                      || userDTO.Cards.Count == 0)
                {
                    result.AppendLine("Invalid Data");
                    continue;
                }

                var user = new User()
                {
                    FullName = userDTO.FullName,
                    Username = userDTO.Username,
                    Email = userDTO.Email,
                    Age = userDTO.Age,
                    Cards = userDTO.Cards.Select(c => new Card()
                        {
                            Number = c.Number,
                            Cvc = c.CVC,
                            Type = Enum.Parse<CardType>(c.Type)
                        })
                        .ToList()
                };

                context.Users.Add(user);
                context.SaveChanges();
                result.AppendLine($"Imported {user.Username}" +
                                  $" with {user.Cards.Count} cards");
            }

            return result.ToString().Trim();
        }

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            var purchasesDTOs = XmlFacade.XmlConverter
                .Deserializer<PurchasesImportModel>(xmlString, "Purchases");

            var result = new StringBuilder();

            var purchases = new List<Purchase>();

            foreach (var purchaseDTO in purchasesDTOs)
            {
                if (!IsValid(purchaseDTO))
                {
                    result.AppendLine("Invalid Data");
                    continue;
                }

                DateTime date;
                bool isDateValid = DateTime.TryParseExact(purchaseDTO.Date, "dd/MM/yyyy HH:mm",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

                if (!isDateValid)
                {
                    result.AppendLine("Invalid Data");
                    continue;
                }

                var purchase = new Purchase()
                {
                    Type = Enum.Parse<PurchaseType>(purchaseDTO.Type),
                    Date = date,
                    ProductKey = purchaseDTO.Key
                };

                var game = context.Games
                    .FirstOrDefault(x => x.Name == purchaseDTO.Title);

                if (game == null)
                {
                    result.AppendLine("Invalid Data");
                    continue;
                }
                else
                {
                    purchase.Game = game;
                }

                var card = context.Cards
                    .FirstOrDefault(x => x.Number == purchaseDTO.Card);

                if (card == null)
                {
                    result.AppendLine("Invalid Data");
                    continue;
                }
                else
                {
                    purchase.Card = card;
                }

                purchases.Add(purchase);
                var userName = purchase.Card.User.Username;
                result.AppendLine($"Imported {purchase.Game.Name}" +
                                  $" for {userName}");
            }

            context.Purchases.AddRange(purchases);
            context.SaveChanges();
            return result.ToString().TrimEnd();
        }

		private static bool IsValid(object dto)
		{
			var validationContext = new ValidationContext(dto);
			var validationResult = new List<ValidationResult>();

			return Validator.TryValidateObject(dto, validationContext, validationResult, true);
		}
	}
}