using Envelope.Database.Config;

namespace Envelope.Database.Internal;

internal class PrimaryKeyInternal : IPrimaryKey
{
	public PrimaryKey Config { get; }
	public TableInternal Table { get; }

	public string Name => Config.Name;
	public List<ColumnInternal> Columns { get; }

	ITable IPrimaryKey.Table => Table;
	IEnumerable<IColumn> IPrimaryKey.Columns => Columns;

	public PrimaryKeyInternal(TableInternal table, PrimaryKey config)
	{
		Table = table ?? throw new ArgumentNullException(nameof(table));
		Config = config ?? throw new ArgumentNullException(nameof(config));
		Columns = new List<ColumnInternal>();
	}

	internal PrimaryKeyInternal AddColumn(ColumnInternal column)
	{
		Columns.Add(column);
		return this;
	}

	public override string ToString()
	{
		return $"{Name}: {string.Join(", ", Columns?.Select(x => x.Name) ?? new List<string>())}";
	}
}
