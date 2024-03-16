using BasicWPFApp.Core;

namespace BasicWPFApp.Persistence;

public record OrderUpdateMessage(Order Order) : IMessage {}
