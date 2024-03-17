using BasicWPFApp.Domain;

namespace BasicWPFApp.Application;

public interface IDataContext
{
	Task Create(Entity entity);

	Task<IEnumerable<Entity>> Read();

	Task Update(Entity entity);

	Task Delete(Guid id);
}
