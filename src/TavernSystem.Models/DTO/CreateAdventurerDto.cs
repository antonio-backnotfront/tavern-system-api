using System.ComponentModel.DataAnnotations;

namespace TavernSystem_Training_1.Models.DTO;

public class CreateAdventurerDto
{
    public int? Id { get; set; }
    public required string Nickname { get; set; }
    public required int RaceId { get; set; }
    public required int ExperienceLevelId { get; set; }
    public required string PersonDataId { get; set; }
}

