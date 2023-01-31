using System.Reflection;
using Microsoft.EntityFrameworkCore;

public static class ModelBuilderExtensions
{
    public static void RegisterAllEntities<GenericObject>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
    {
        IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes())
          .Where(
            c => c.IsClass && !c.IsAbstract && c.IsPublic
            && typeof(GenericObject).IsAssignableFrom(c)
          );
        foreach (Type type in types)
            modelBuilder.Entity(type);
    }
}
