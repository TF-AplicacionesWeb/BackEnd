namespace DentifyBackend.Dentify.Domain.Model.Queries;

public record GetUserByUsernameAndPasswordQuery(string username, string password);