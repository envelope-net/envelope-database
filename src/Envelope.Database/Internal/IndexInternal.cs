namespace Envelope.Database.Internal;

internal class IndexInternal : IIndex
{
	public Config.Index Config { get; }
	public TableInternal Table { get; }

	public string Name => Config.Name;
	public List<ColumnInternal> Columns { get; }

	ITable IIndex.Table => Table;
	IEnumerable<IColumn> IIndex.Columns => Columns;

	public IndexInternal(TableInternal table, Config.Index config)
	{
		Table = table ?? throw new ArgumentNullException(nameof(table));
		Config = config ?? throw new ArgumentNullException(nameof(config));
		Columns = new List<ColumnInternal>();
	}

	internal IndexInternal AddColumn(ColumnInternal column)
	{
		Columns.Add(column);
		return this;
	}

	public override string ToString()
	{
		return $"{Name}: {string.Join(", ", Columns?.Select(x => x.Name) ?? new List<string>())}";
	}
}
