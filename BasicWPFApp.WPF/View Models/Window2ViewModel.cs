using BasicWPFApp.Core;
using BasicWPFApp.Persistence;
using MediatR;

namespace BasicWPFApp.WPF;

internal class Window2ViewModel: ViewModel
{
	private readonly IMediator _mediator;
    private readonly IMessageBus _messageBus;

    public IEnumerable<Order>? Orders { get; private set; }

    public Window2ViewModel(IMediator mediator, IMessageBus messageBus)
    {
        _mediator = mediator;
        _messageBus = messageBus;

        Orders = _mediator.Send(new GetOrdersQuery()).Result;

		_messageBus.RegisterFor<OrderUpdateMessage>((msg) => UpdateOrders());
    }

    private void UpdateOrders()
    {
        Orders = _mediator.Send(new GetOrdersQuery()).Result;
        NotifyPropertyChanged(nameof(Orders));
    }
}
