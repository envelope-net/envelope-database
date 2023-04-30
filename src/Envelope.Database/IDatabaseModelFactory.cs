namespace Envelope.Database;

public interface IDatabaseModelFactory
{
	IModel Create(Config.Model config, bool cloneConfig = true);
}
