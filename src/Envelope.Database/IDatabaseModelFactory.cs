namespace Envelope.Database;

public interface IDatabaseModelFactory
{
	bool TryCreate(Config.Model config, out IModel model, bool cloneConfig = true);
}
