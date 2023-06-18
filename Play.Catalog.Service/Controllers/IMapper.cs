namespace Play.Catalog.Service.Controllers
{
    public interface IMapper
    {
        object Map<T>(object item);
    }
}