using Envelope.Database.Config;

namespace Envelope.Database.Internal;

internal class SchemaInternal : ISchema
{
	public Schema Config { get; }
	public ModelInternal Model { get; }

	public string Name => Config.Name;
	public string Alias => Config.Alias;
	public int? Id => Config.Id;
	public List<TableInternal>? Tables { get; }
	public List<ViewInternal>? Views { get; }

	IModel ISchema.Model => Model;
	IEnumerable<ITable>? ISchema.Tables => Tables?.Cast<ITable>()!;
	IEnumerable<IView>? ISchema.Views => Views.Cast<IView>()!;

	public SchemaInternal(ModelInternal model, Schema config)
	{
		Model = model ?? throw new ArgumentNullException(nameof(model));
		Config = config ?? throw new ArgumentNullException(nameof(config));
		Tables = new();
		Views = new();

		if (0 < Config.Tables?.Count)
			foreach (var table in Config.Tables)
				Tables.Add(new TableInternal(this, table));

		if (0 < Config.Views?.Count)
			foreach (var view in Config.Views)
				Views.Add(new ViewInternal(this, view));
	}

	public override string ToString()
	{
		return Name;
	}
}
