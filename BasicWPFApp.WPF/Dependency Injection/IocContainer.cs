using BasicWPFApp.Persistence;
using Ninject;

namespace BasicWPFApp.WPF;

internal class IocContainer
{
	private IKernel _kernel;

    public MainWindowViewModel MainWindowViewModel => _kernel.Get<MainWindowViewModel>();
    public Window2ViewModel Window2ViewModel => _kernel.Get<Window2ViewModel>();

    public IocContainer()
    {
        _kernel = new StandardKernel(new PersistenceModule(), new ConfigurationModule(), new ViewModelModule());
    }
}
