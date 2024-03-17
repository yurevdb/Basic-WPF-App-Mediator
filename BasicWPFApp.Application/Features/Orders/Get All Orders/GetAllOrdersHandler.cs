using BasicWPFApp.Domain;
using MediatR;

namespace BasicWPFApp.Application;

internal class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<Order>>
{
	private readonly OrderRepository _repository;

	public GetAllOrdersHandler(OrderRepository repository)
	{
		_repository = repository;
	}
	public async Task<IEnumerable<Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
	{
		return await Task.FromResult(await _repository.GetAll());
	}
}
