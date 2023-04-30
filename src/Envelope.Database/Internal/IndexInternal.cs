namespace Envelope.Database.Internal;

internal class IndexInternal : IIndex
{
	private readonly Config.Index _config;

	public TableInternal Table { get; }

	public string Name => _config.Name;
	public List<string> Columns => _config.Columns;

	ITable IIndex.Table => Table;
	IEnumerable<string> IIndex.Columns => Columns;

	public IndexInternal(TableInternal table, Config.Index config)
	{
		Table = table ?? throw new ArgumentNullException(nameof(table));
		_config = config ?? throw new ArgumentNullException(nameof(config));
	}

	public override string ToString()
	{
		return $"{Name}: {string.Join(", ", Columns ?? new List<string>())}";
	}
}
