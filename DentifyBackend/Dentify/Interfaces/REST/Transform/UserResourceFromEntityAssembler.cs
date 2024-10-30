using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Interfaces.REST.Resources;

namespace DentifyBackend.Dentify.Interfaces.REST.Transform;

public class UserResourceFromEntityAssembler
{
    /// <summary>
    /// Assembles a FavoriteSourceResource from a FavoriteSource. 
    /// </summary>
    /// <param name="entity">The FavoriteSource entity</param>
    /// <returns>
    /// A FavoriteSourceResource assembled from the FavoriteSource
    /// </returns>
    
    public static UserResource ToResourceFromEntity(User entity) => 
        new UserResource(entity.id, entity.username, entity.first_name, entity.last_name, entity.email, entity.phone,
            entity.register_date, entity.company, entity.password);
}