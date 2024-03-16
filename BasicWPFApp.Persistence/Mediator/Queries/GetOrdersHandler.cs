using BasicWPFApp.Core;
using MediatR;

namespace BasicWPFApp.Persistence;

internal class GetOrdersHandler : IRequestHandler<GetOrdersQuery, IEnumerable<Order>>
{
	public async Task<IEnumerable<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
	{
		return await Task.FromResult(MemCache.Orders.ToList());
	}
}
