using BasicWPFApp.Persistence;
using Ninject;

namespace BasicWPFApp.WPF;

internal class IocContainer
{
	private IKernel _kernel;

    public MainWindowViewModel MainWindowViewModel => _kernel.Get<MainWindowViewModel>();

    public IocContainer()
    {
        _kernel = new StandardKernel(new PersistenceModule(), new ConfigurationModule(), new ViewModelModule());
    }
}
