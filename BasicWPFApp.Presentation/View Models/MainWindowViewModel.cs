using BasicWPFApp.Application;
using BasicWPFApp.Domain;
using BasicWPFApp.Presentation.Windows;
using MediatR;
using System.Windows.Input;

namespace BasicWPFApp.Presentation;

internal class MainWindowViewModel : ViewModel
{
	private IMediator _mediator;

    public ICommand CreateOrderCommand { get; }
    public ICommand ShowWindow2Command { get; }

    public IEnumerable<Order>? Orders { get; private set; }

    public MainWindowViewModel(IMediator mediator)
    {
        _mediator = mediator;
        CreateOrderCommand = new RelayCommand<string>(CreateCommand);
        ShowWindow2Command = new RelayCommand(ShowWindow2);
    }

    private async Task CreateCommand(string title)
    {
		await _mediator.Send(new CreateOrderCommand(new Order() { Title = title }));
        Orders = await _mediator.Send(new GetAllOrdersQuery());
        NotifyPropertyChanged(nameof(Orders));
    }

    private void ShowWindow2()
    {
        var w = new Window2();
        w.Show();
    }
}
