namespace Api.Infrastructure;

public interface IRepository
{
	Task<T> GetById<T>(string id);
	Task Add<T>(T item);
}