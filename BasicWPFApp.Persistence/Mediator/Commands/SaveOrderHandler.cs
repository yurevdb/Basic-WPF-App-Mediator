using MediatR;

namespace BasicWPFApp.Persistence;

internal class SaveOrderHandler : IRequestHandler<SaveOrderCommand>
{
	public async Task Handle(SaveOrderCommand request, CancellationToken cancellationToken)
	{
		MemCache.Orders.Add(request.Order);
		await Task.FromResult(true);
	}
}
