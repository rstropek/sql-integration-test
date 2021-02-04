using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AnimalCounter.Api.Data
{
    public class AnimalFrequency
    {
        public int Id { get; set; }

        [JsonPropertyName("animal_name")]
        [MaxLength(250)]
        [Required]
        public string AnimalName { get; set; } = string.Empty;

        [JsonPropertyName("scientific_name")]
        [MaxLength(250)]
        [Required]
        public string ScientificName { get; set; } = string.Empty;

        [JsonPropertyName("country_code")]
        [MaxLength(5)]
        [Required]
        public string CountryCode { get; set; } = string.Empty;

        [JsonPropertyName("country")]
        [MaxLength(100)]
        [Required]
        public string Country { get; set; } = string.Empty;

        [JsonPropertyName("number_of_animals")]
        public int? NumberOfAnimals { get; set; }
    }
}
