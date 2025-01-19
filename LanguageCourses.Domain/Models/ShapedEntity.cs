namespace LanguageCourses.Domain.Models;

public class ShapedEntity
{
    public ShapedEntity()
    {
        Entity = [];
    }
    public Guid Id { get; set; }
    public Entity Entity { get; set; }
}
