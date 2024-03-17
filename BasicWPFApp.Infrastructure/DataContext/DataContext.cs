using BasicWPFApp.Application;
using BasicWPFApp.Domain;

namespace BasicWPFApp.Infrastructure;

internal class DataContext : IDataContext
{
	private MemCache _memCache;

	public DataContext()
	{
		_memCache = new MemCache();
	}

	public async Task Create(Entity entity)
	{
		await _memCache.Set(entity);
	}

	public async Task Delete(Guid id)
	{
		await _memCache.Remove(id);
	}

	public async Task<IEnumerable<Entity>> Read()
	{
		return await _memCache.GetAll();
	}

	public async Task Update(Entity entity)
	{
		await _memCache.Set(entity);
	}
}
