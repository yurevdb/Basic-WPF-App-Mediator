using MediatR;

namespace BasicWPFApp.Application;

internal class CreateOrderHandler : IRequestHandler<CreateOrderCommand>
{
	private readonly IMessageBus _messageBus;
	private readonly OrderRepository _orderRepository;

	public CreateOrderHandler(IMessageBus messageBus, OrderRepository orderRepository)
	{
		_messageBus = messageBus;
		_orderRepository = orderRepository;
	}

	public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
	{
		await _orderRepository.Save(request.Order);
		await _messageBus.Publish(new OrderUpdateMessage(request.Order));
		await Task.FromResult(true);
	}
}
