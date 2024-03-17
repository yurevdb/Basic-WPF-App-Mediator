using Ninject.Modules;

namespace BasicWPFApp.Application;

public class ApplicationModule : NinjectModule
{
	public override void Load()
	{
		Kernel.AddMediatr(typeof(CreateOrderCommand).Assembly);
		Bind<OrderRepository>().ToSelf().InTransientScope();
	}
}
