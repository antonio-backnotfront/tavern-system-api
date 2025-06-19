using System.ComponentModel.DataAnnotations;

namespace TavernSystem_Training_1.Models.DTO;

public class CreateAdventurerDto
{
    public int? Id { get; set; }
    [Required]
    public string Nickname { get; set; }
    [Required]
    public int RaceId { get; set; }
    [Required]
    public int ExperienceLevelId { get; set; }
    [Required]
    [RegularExpression("[A-Z]{2}(000[1-9]{1}|00[1-9]{1}[0-9]{1}|0[1-9]{1}[0-9]{2}|[1-9]{1}[0-9]{3})(0[1-9]{1}|1[0-2]{1})(0[1-9]{1}|1[0-9]{1}|2[0-8]{1})[0-9]{4}[A-Z]{2}")]
    public string PersonDataId { get; set; }
}

