using Envelope.Database.Config;
using Envelope.Validation;

namespace Envelope.Database.Internal;

internal class TableInternal : ITable
{
	public Table Config { get; }
	public SchemaInternal Schema { get; }

	public string Name => Config.Name;
	public string Alias => Schema.Alias;
	public int? Id => Config.Id;
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
		Config = config ?? throw new ArgumentNullException(nameof(config));
		Columns = new();
		ForeignKeys = new();
		UniqueConstraints = new();
		Indexes = new();

		if (0 < Config.Columns?.Count)
			foreach (var column in Config.Columns)
				Columns.Add(new ColumnInternal(this, column));

		if (Config.PrimaryKey != null)
		{
			PrimaryKey = new PrimaryKeyInternal(this, Config.PrimaryKey);
			foreach (var columnName in PrimaryKey.Config.Columns)
			{
				var column = Columns.FirstOrDefault(x => x.Name == columnName);
				if (column == null)
				{
					Schema.Model.AddError(ValidationMessageFactory.Error($"Invalid PK {Schema.Name}.{Name}.{PrimaryKey.Name} | Column = {columnName}"));
					continue;
				}

				column.SetPrimaryKey(PrimaryKey);
				PrimaryKey.AddColumn(column);
			}
		}

		if (0 < Config.ForeignKeys?.Count)
			foreach (var foreignKey in Config.ForeignKeys)
				ForeignKeys.Add(new ForeignKeyInternal(this, foreignKey));

		if (0 < Config.UniqueConstraints?.Count)
			foreach (var uniqueConstraint in Config.UniqueConstraints)
			{
				var uq = new UniqueConstraintInternal(this, uniqueConstraint);
				UniqueConstraints.Add(uq);
				foreach (var columnName in uq.Config.Columns)
				{
					var column = Columns.FirstOrDefault(x => x.Name == columnName);
					if (column == null)
					{
						Schema.Model.AddError(ValidationMessageFactory.Error($"Invalid UniqueConstraint {Schema.Name}.{Name}.{uq.Name} | Column = {columnName}"));
						continue;
					}

					column.AddUniqueConstraint(uq);
					uq.AddColumn(column);
				}
			}

		if (0 < Config.Indexes?.Count)
			foreach (var index in Config.Indexes)
			{
				var idx = new IndexInternal(this, index);
				Indexes.Add(idx);
				foreach (var columnName in idx.Config.Columns)
				{
					var column = Columns.FirstOrDefault(x => x.Name == columnName);
					if (column == null)
					{
						Schema.Model.AddError(ValidationMessageFactory.Error($"Invalid Index {Schema.Name}.{Name}.{idx.Name} | Column = {columnName}"));
						continue;
					}

					column.AddIndex(idx);
					idx.AddColumn(column);
				}
			}
	}

	public override string ToString()
	{
		return Name;
	}
}
