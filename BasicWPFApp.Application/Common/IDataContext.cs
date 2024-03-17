using BasicWPFApp.Domain;

namespace BasicWPFApp.Application;

public interface IDataContext
{
	Task Create(Entity entity);

	Task<IEnumerable<Entity>> Read();
}
