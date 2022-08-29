namespace Api.Infrastructure;

public interface IRepository
{
	//Task<T> GetById<T>(string id);
	Task Add<T>(T item);
	IQueryable<T> Query<T>();
	Task Update<T>(T item);
}