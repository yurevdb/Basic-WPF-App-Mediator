using BasicWPFApp.Domain;

namespace BasicWPFApp.Application;

internal class OrderRepository : Repository<Order>
{
	public OrderRepository(IDataContext dataContext) : base(dataContext)
	{
	}
}
