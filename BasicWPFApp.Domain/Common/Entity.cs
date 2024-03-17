namespace BasicWPFApp.Domain;

public abstract class Entity
{
	public Guid Id { get; } 

	internal Entity()
	{
		Id = Guid.NewGuid();
	}
}
