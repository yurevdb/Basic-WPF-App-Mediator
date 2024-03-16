using BasicWPFApp.Core;
using BasicWPFApp.Persistence;
using MediatR;
using System.Windows.Input;

namespace BasicWPFApp.WPF;

internal class MainWindowViewModel : ViewModel
{
	private IMediator _mediator;

    public ICommand CreateOrderCommand { get; }

    public IEnumerable<Order>? Orders { get; private set; }

    public MainWindowViewModel(IMediator mediator)
    {
        _mediator = mediator;
        CreateOrderCommand = new RelayCommand<string>(CreateCommand);
    }

    private async Task CreateCommand(string title)
    {
		await _mediator.Send(new SaveOrderCommand(new Order() { Title = title }));
        Orders = await _mediator.Send(new GetOrdersQuery());
        NotifyPropertyChanged(nameof(Orders));
    }
}
