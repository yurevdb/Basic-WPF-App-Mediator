using MediatR;

namespace BasicWPFApp.Persistence;

internal class SaveOrderHandler : IRequestHandler<SaveOrderCommand>
{
	private readonly IMessageBus _messageBus;

	public SaveOrderHandler(IMessageBus messageBus)
	{
		_messageBus = messageBus;
	}

	public async Task Handle(SaveOrderCommand request, CancellationToken cancellationToken)
	{
		MemCache.Orders.Add(request.Order);
		await _messageBus.Publish(new OrderUpdateMessage(request.Order));
		await Task.FromResult(true);
	}
}
