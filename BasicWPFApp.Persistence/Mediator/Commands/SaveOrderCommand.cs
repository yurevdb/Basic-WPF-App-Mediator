using BasicWPFApp.Core;
using MediatR;

namespace BasicWPFApp.Persistence;

public record SaveOrderCommand(Order Order) : IRequest { }
