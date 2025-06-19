namespace TavernSystem_Training_1.Models.DTO;

public class GetAdventurerDto
{
    public int Id { get; set; }
    public string Nickname { get; set; }
    public string Race { get; set; }
    public string ExperienceLevel { get; set; }
    public GetPersonDto PersonData { get; set; }
}

