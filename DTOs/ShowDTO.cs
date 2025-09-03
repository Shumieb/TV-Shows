using TV_Shows.Models;

namespace TV_Shows.DTOs
{
    public class ShowDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public int Season { get; set; }
        public int Episode { get; set; }
        public int PlatformId { get; set; }
        public Platform? Platform { get; set; }
        public string? Note { get; set; }
        public bool Like { get; set; } = false;

        public ShowDTO() { }

        public ShowDTO(Show show)
        {
            Id = show.Id;
            Name = show.Name;
            Season= show.Season;
            Episode = show.Episode;
            Note = show.Note;
            Like= show.Like;
            Platform = show.Platform;
            PlatformId=show.PlatformID;
        }
    }

   


    }
