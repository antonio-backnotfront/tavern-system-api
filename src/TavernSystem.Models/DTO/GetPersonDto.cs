namespace TavernSystem_Training_1.Models.DTO;

public class GetPersonDto
{
    public required string Id { get; set; }
    public required string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public required string LastName { get; set; }
    public required bool HasBounty { get; set; }
}