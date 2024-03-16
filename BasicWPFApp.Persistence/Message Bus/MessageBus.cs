namespace BasicWPFApp.Persistence;

internal class MessageBus : IMessageBus
{
	private readonly Dictionary<Type, List<Action<IMessage>>> _receivers;

    public MessageBus()
    {
		_receivers = new Dictionary<Type, List<Action<IMessage>>>();
	}

    public async Task Publish(IMessage message)
	{
		var receivers = _receivers.GetValueOrDefault(message.GetType());
		if (receivers == null)
			await Task.FromResult(true);

		foreach (Action<IMessage> receiver in receivers!)
			receiver(message);
	}

	public void RegisterFor<T>(Action<IMessage> callback) where T : IMessage
	{
		if (!_receivers.ContainsKey(typeof(T)))
			_receivers.Add(typeof(T), [callback]);
		else
			_receivers[typeof(T)].Add(callback);
	}
}
