using BasicWPFApp.Application;
using BasicWPFApp.Domain;
using MediatR;

namespace BasicWPFApp.Presentation;

internal class Window2ViewModel: ViewModel
{
	private readonly IMediator _mediator;
    private readonly IMessageBus _messageBus;

    public IEnumerable<Order>? Orders { get; private set; }

    public Window2ViewModel(IMediator mediator, IMessageBus messageBus)
    {
        _mediator = mediator;
        _messageBus = messageBus;

        Orders = _mediator.Send(new GetAllOrdersQuery()).Result;

		_messageBus.RegisterFor<OrderUpdateMessage>((msg) => UpdateOrders());
    }

    private void UpdateOrders()
    {
        Orders = _mediator.Send(new GetAllOrdersQuery()).Result;
        NotifyPropertyChanged(nameof(Orders));
    }
}
