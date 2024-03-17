using BasicWPFApp.Domain;

namespace BasicWPFApp.Infrastructure;

internal class MemCache
{
	private Dictionary<Guid, Entity> _orders { get; } = new Dictionary<Guid, Entity>();

	public async Task<Entity> Get(Guid id)
	{
		if (!_orders.ContainsKey(id))
			throw new Exception($"{id} was not found");

		return await Task.FromResult(_orders[id]);
	}

	public async Task Set(Entity entity)
	{
		if (!_orders.ContainsKey(entity.Id))
			_orders.Add(entity.Id, entity);
		else
		{
			_orders[entity.Id] = entity;
		}

		await Task.FromResult(true);
	}

	public async Task<IEnumerable<Entity>> GetAll()
	{
		return await Task.FromResult(_orders.Values.ToList());
	}
}
