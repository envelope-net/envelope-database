namespace Envelope.Database.Internal;

internal class DatabaseModelFactory : IDatabaseModelFactory
{
	public IModel Create(Config.Model config, bool cloneConfig = true)
		=> new ModelInternal(config, cloneConfig).Build();
}
