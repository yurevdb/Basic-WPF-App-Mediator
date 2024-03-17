using BasicWPFApp.Domain;
using MediatR;

namespace BasicWPFApp.Application;

public record GetAllOrdersQuery(): IRequest<IEnumerable<Order>> { }
