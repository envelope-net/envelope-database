using Envelope.Database.Config;

namespace Envelope.Database.Internal;

internal class ViewInternal : IView
{
	public View Config { get; }
	public SchemaInternal Schema { get; }

	public string Name => Config.Name;
	public string Alias => Schema.Name;
	public int? Id => Config.Id;
	public string Definition => Config.Definition;

	ISchema IView.Schema => Schema;

	public ViewInternal(SchemaInternal schema, View config)
	{
		Schema = schema ?? throw new ArgumentNullException(nameof(schema));
		Config = config ?? throw new ArgumentNullException(nameof(config));
	}

	public override string ToString()
	{
		return Name;
	}
}
