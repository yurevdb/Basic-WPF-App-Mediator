using BasicWPFApp.Domain;

namespace BasicWPFApp.Application;

internal abstract class Repository<T> where T : Entity
{
	protected readonly IDataContext _dataContext;

	public Repository(IDataContext dataContext)
	{
		_dataContext = dataContext;
	}

	public async Task<T?> GetById(Guid id)
	{
		return (await _dataContext.Read()).FirstOrDefault(d => d.Id == id) as T;
	}

	public async Task<IEnumerable<T>> GetAll()
	{
		return (await _dataContext.Read()).Cast<T>();
	}

	public async Task Save(T entity)
	{
		await _dataContext.Create(entity);
	}
}
