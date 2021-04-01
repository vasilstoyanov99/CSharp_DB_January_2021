using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaporStore.Data.Models
{
    public class Game
    {
        public Game()
        {
            GameTags = new HashSet<GameTag>();
            Purchases = new HashSet<Purchase>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [ForeignKey(nameof(Developer))]
        public int DeveloperId { get; set; }

        [Required]
        public Developer Developer { get; set; }

        [Required]
        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Required]
        public ICollection<Purchase> Purchases { get; set; }

        public ICollection<GameTag> GameTags { get; set; }
    }
}
