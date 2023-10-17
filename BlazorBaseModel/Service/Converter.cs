using System.Reflection;
using BlazorBaseModel.Sevice;

public class Converter : IConverter
{
    /// <summary>
    /// Permet de convertir un objet model en objet de base de données. Tous les champs de model qui existent dans db sont transférés sur db.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="modelObj"></param>
    /// <param name="dbObj"></param>
    /// <exception cref="Exception"></exception>
    public void ModelToDb<T, U>(T modelObj, U dbObj) where U : new()
    {
        if (modelObj == null)
        {
            throw new Exception("'modelObj' ne doit pas être null");
        }
        if (dbObj == null)
        {
            dbObj = new U();
        }
        List<PropertyInfo> dbObjPropList = modelObj.GetType().GetProperties().ToList();
        foreach (var prop in modelObj.GetType().GetProperties())
        {
            Console.WriteLine("{0}={1}", prop.Name, prop.GetValue(modelObj, null));
            PropertyInfo? dbProp = dbObjPropList.Where(p => p.Name == prop.Name).FirstOrDefault();
            if (dbProp != null)
            {
                dbProp.SetValue(dbObj, prop.GetValue(modelObj, null));
            }
        }
    }
}