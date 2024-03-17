using BasicWPFApp.Domain;
using MediatR;

namespace BasicWPFApp.Application;

public record CreateOrderCommand(Order Order) : IRequest { }
