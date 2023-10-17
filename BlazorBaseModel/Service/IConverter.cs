namespace BlazorBaseModel.Sevice
{
    public interface IConverter
    {
        void ModelToDb<T, U>(T modelObj, U dbObj) where U : new();
    }
}