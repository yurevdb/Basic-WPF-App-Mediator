using BasicWPFApp.Core;
using MediatR;

namespace BasicWPFApp.Persistence;

public record GetOrdersQuery(): IRequest<IEnumerable<Order>> { }
