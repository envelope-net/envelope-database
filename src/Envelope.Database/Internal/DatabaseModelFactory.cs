namespace Envelope.Database.Internal;

internal class DatabaseModelFactory : IDatabaseModelFactory
{
	public bool TryCreate(Config.Model config, out IModel model, bool cloneConfig = true)
	{
		model = new ModelInternal(config, cloneConfig);
		return model.Build(out model);
	}
}
