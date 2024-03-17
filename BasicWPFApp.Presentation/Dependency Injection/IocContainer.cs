using BasicWPFApp.Application;
using BasicWPFApp.Infrastructure;
using Ninject;

namespace BasicWPFApp.Presentation;

internal class IocContainer
{
	private IKernel _kernel;

    public MainWindowViewModel MainWindowViewModel => _kernel.Get<MainWindowViewModel>();
    public Window2ViewModel Window2ViewModel => _kernel.Get<Window2ViewModel>();

    public IocContainer()
    {
        _kernel = new StandardKernel(new ApplicationModule(), new ConfigurationModule(), new ViewModelModule(), new InfrastructureModule());
    }
}
