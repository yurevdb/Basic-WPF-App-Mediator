namespace BasicWPFApp.Application;

public interface IMessageBus
{
	Task Publish(IMessage message);

	void RegisterFor<T>(Action<IMessage> callback) where T : IMessage;
}
