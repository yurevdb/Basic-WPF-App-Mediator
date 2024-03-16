using Ninject.Modules;

namespace BasicWPFApp.Persistence;

public class PersistenceModule : NinjectModule
{
	public override void Load()
	{
		Kernel.AddMediatr(typeof(SaveOrderCommand).Assembly);
	}
}
