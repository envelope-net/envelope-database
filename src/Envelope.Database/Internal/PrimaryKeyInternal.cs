using Envelope.Database.Config;

namespace Envelope.Database.Internal;

internal class PrimaryKeyInternal : IPrimaryKey
{
	private readonly PrimaryKey _config;

	public TableInternal Table { get; }

	public string Name => _config.Name;
	public List<string> Columns => _config.Columns;

	ITable IPrimaryKey.Table => Table;
	IEnumerable<string> IPrimaryKey.Columns => Columns;

	public PrimaryKeyInternal(TableInternal table, PrimaryKey config)
	{
		Table = table ?? throw new ArgumentNullException(nameof(table));
		_config = config ?? throw new ArgumentNullException(nameof(config));
	}

	public override string ToString()
	{
		return $"{Name}: {string.Join(", ", Columns ?? new List<string>())}";
	}
}
