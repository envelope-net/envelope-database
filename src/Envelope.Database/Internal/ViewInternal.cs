using Envelope.Database.Config;

namespace Envelope.Database.Internal;

internal class ViewInternal : IView
{
	private readonly View _config;

	public SchemaInternal Schema { get; }

	public string Name => _config.Name;
	public string Alias => Schema.Name;
	public string Definition => _config.Definition;

	ISchema IView.Schema => Schema;

	public ViewInternal(SchemaInternal schema, View config)
	{
		Schema = schema ?? throw new ArgumentNullException(nameof(schema));
		_config = config ?? throw new ArgumentNullException(nameof(config));
	}

	public override string ToString()
	{
		return Name;
	}
}
