using Envelope.Database.Config;

namespace Envelope.Database.Internal;

internal class TableInternal : ITable
{
	private readonly Table _config;

	public SchemaInternal Schema { get; }

	public string Name => _config.Name;
	public string Alias => Schema.Alias;
	public List<ColumnInternal> Columns { get; }
	public PrimaryKeyInternal? PrimaryKey { get; }
	public List<ForeignKeyInternal>? ForeignKeys { get; }
	public List<UniqueConstraintInternal>? UniqueConstraints { get; }
	public List<IndexInternal>? Indexes { get; }

	ISchema ITable.Schema => Schema;
	IEnumerable<IColumn> ITable.Columns => Columns;
	IPrimaryKey? ITable.PrimaryKey => PrimaryKey;
	IEnumerable<IForeignKey>? ITable.ForeignKeys => ForeignKeys;
	IEnumerable<IUniqueConstraint>? ITable.UniqueConstraints => UniqueConstraints;
	IEnumerable<IIndex>? ITable.Indexes => Indexes;

	public TableInternal(SchemaInternal schema, Table config)
	{
		Schema = schema ?? throw new ArgumentNullException(nameof(schema));
		_config = config ?? throw new ArgumentNullException(nameof(config));
		Columns = new();
		ForeignKeys = new();
		UniqueConstraints = new();
		Indexes = new();

		if (0 < _config.Columns?.Count)
			foreach (var column in _config.Columns)
				Columns.Add(new ColumnInternal(this, column));

		if (_config.PrimaryKey != null)
			PrimaryKey = new PrimaryKeyInternal(this, _config.PrimaryKey);

		if (0 < _config.ForeignKeys?.Count)
			foreach (var foreignKey in _config.ForeignKeys)
				ForeignKeys.Add(new ForeignKeyInternal(this, foreignKey));

		if (0 < _config.UniqueConstraints?.Count)
			foreach (var uniqueConstraint in _config.UniqueConstraints)
				UniqueConstraints.Add(new UniqueConstraintInternal(this, uniqueConstraint));

		if (0 < _config.Indexes?.Count)
			foreach (var index in _config.Indexes)
				Indexes.Add(new IndexInternal(this, index));
	}

	public override string ToString()
	{
		return Name;
	}
}
