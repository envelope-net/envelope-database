using Envelope.Database.Config;

namespace Envelope.Database.Internal;

internal class ColumnInternal : IColumn
{
	public Column Config { get; }
	public TableInternal Table { get; }

	public string Name => Config.Name;
	public string DatabaseType => Config.DatabaseType;
	public bool IsNotNull => Config.IsNotNull;
	public string? DefaultValue => Config.DefaultValue;
	public int CharacterMaximumLength => Config.CharacterMaximumLength;
	public int? Precision => Config.Precision;
	public int? Scale => Config.Scale;
	public bool IsIdentity => Config.IsIdentity;
	public long? IdentityStart => Config.IdentityStart;
	public long? IdentityIncrement => Config.IdentityIncrement;
	public long? LastIdentity => Config.LastIdentity;
	public string? ComputedColumnSql => Config.ComputedColumnSql;

	public PrimaryKeyInternal? PrimaryKey { get; private set; }
	public List<UniqueConstraintInternal> UniqueConstraints { get; }
	public List<IndexInternal> Indexes { get; }
	public ForeignKeyInternal? TargetForeignKey { get; private set; }
	public List<ForeignKeyInternal> SourceForeignKeys { get; }

	ITable IColumn.Table => Table;
	IPrimaryKey? IColumn.PrimaryKey => PrimaryKey;
	IEnumerable<IUniqueConstraint> IColumn.UniqueConstraints => UniqueConstraints;
	IEnumerable<IIndex> IColumn.Indexes => Indexes;
	IForeignKey? IColumn.TargetForeignKey => TargetForeignKey;
	IEnumerable<IForeignKey> IColumn.SourceForeignKeys => SourceForeignKeys;

	public ColumnInternal(TableInternal table, Column config)
	{
		Table = table ?? throw new ArgumentNullException(nameof(table));
		Config = config ?? throw new ArgumentNullException(nameof(config));
		UniqueConstraints = new List<UniqueConstraintInternal>();
		Indexes = new List<IndexInternal>();
		SourceForeignKeys = new List<ForeignKeyInternal>();
	}

	internal ColumnInternal SetPrimaryKey(PrimaryKeyInternal primaryKey)
	{
		PrimaryKey = primaryKey;
		return this;
	}

	internal ColumnInternal AddUniqueConstraint(UniqueConstraintInternal uq)
	{
		UniqueConstraints.Add(uq);
		return this;
	}

	internal ColumnInternal AddIndex(IndexInternal idx)
	{
		Indexes.Add(idx);
		return this;
	}

	internal ColumnInternal SetTargetForeignKey(ForeignKeyInternal foreignKey)
	{
		TargetForeignKey = foreignKey;
		return this;
	}

	internal ColumnInternal AddSourceForeignKey(ForeignKeyInternal foreignKey)
	{
		SourceForeignKeys.Add(foreignKey);
		return this;
	}

	public override string ToString()
	{
		return Name;
	}
}
