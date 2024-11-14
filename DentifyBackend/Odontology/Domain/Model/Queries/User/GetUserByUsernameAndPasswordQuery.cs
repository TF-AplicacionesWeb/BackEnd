namespace DentifyBackend.Odontology.Domain.Model.Queries.User;

public record GetUserByUsernameAndPasswordQuery(string username, string password);