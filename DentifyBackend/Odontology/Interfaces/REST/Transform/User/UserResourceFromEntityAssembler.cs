using DentifyBackend.Odontology.Interfaces.REST.Resources.User;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.User;

public class UserResourceFromEntityAssembler
{
    /// <summary>
    ///     Assembles a FavoriteSourceResource from a FavoriteSource.
    /// </summary>
    /// <param name="entity">The FavoriteSource entity</param>
    /// <returns>
    ///     A FavoriteSourceResource assembled from the FavoriteSource
    /// </returns>
    public static UserResource ToResourceFromEntity(Domain.Model.Aggregates.User entity)
    {
        return new UserResource(entity.id, entity.username, entity.first_name, entity.last_name, entity.email,
            entity.phone,
            entity.register_date, entity.company, entity.password, entity.trial);
    }
}