using BasicWPFApp.Application;
using Ninject.Modules;

namespace BasicWPFApp.Infrastructure;

public class InfrastructureModule : NinjectModule
{
	public override void Load()
	{
		Bind<IMessageBus>().To<MessageBus>().InSingletonScope();
		Bind<IDataContext>().To<DataContext>().InSingletonScope();
	}
}
