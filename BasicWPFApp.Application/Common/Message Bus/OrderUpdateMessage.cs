using BasicWPFApp.Domain;

namespace BasicWPFApp.Application;

public record OrderUpdateMessage(Order Order) : IMessage {}
