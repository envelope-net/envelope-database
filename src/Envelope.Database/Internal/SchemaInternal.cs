using Envelope.Database.Config;

namespace Envelope.Database.Internal;

internal class SchemaInternal : ISchema
{
	private readonly Schema _config;

	public ModelInternal Model { get; }

	public string Name => _config.Name;
	public string Alias => _config.Alias;
	public List<TableInternal>? Tables { get; }
	public List<ViewInternal>? Views { get; }

	IModel ISchema.Model => Model;
	IEnumerable<ITable>? ISchema.Tables => Tables?.Cast<ITable>()!;
	IEnumerable<IView>? ISchema.Views => Views.Cast<IView>()!;

	public SchemaInternal(ModelInternal model, Schema config)
	{
		Model = model ?? throw new ArgumentNullException(nameof(model));
		_config = config ?? throw new ArgumentNullException(nameof(config));
		Tables = new();
		Views = new();

		if (0 < _config.Tables?.Count)
			foreach (var table in _config.Tables)
				Tables.Add(new TableInternal(this, table));

		if (0 < _config.Views?.Count)
			foreach (var view in _config.Views)
				Views.Add(new ViewInternal(this, view));
	}

	public override string ToString()
	{
		return Name;
	}
}
