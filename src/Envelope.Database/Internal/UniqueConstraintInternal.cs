namespace Envelope.Database.Internal;

internal class UniqueConstraintInternal : IUniqueConstraint
{
	public Config.UniqueConstraint Config { get; }
	public TableInternal Table { get; }

	public string Name => Config.Name;
	public List<ColumnInternal> Columns { get; }

	ITable IUniqueConstraint.Table => Table;
	IEnumerable<IColumn> IUniqueConstraint.Columns => Columns;

	public UniqueConstraintInternal(TableInternal table, Config.UniqueConstraint config)
	{
		Table = table ?? throw new ArgumentNullException(nameof(table));
		Config = config ?? throw new ArgumentNullException(nameof(config));
		Columns = new List<ColumnInternal>();
	}

	internal UniqueConstraintInternal AddColumn(ColumnInternal column)
	{
		Columns.Add(column);
		return this;
	}

	public override string ToString()
	{
		return $"{Name}: {string.Join(", ", Columns?.Select(x => x.Name) ?? new List<string>())}";
	}
}
