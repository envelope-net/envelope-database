namespace Envelope.Database.Internal;

internal class UniqueConstraintInternal : IUniqueConstraint
{
	private readonly Config.UniqueConstraint _config;

	public TableInternal Table { get; }

	public string Name => _config.Name;
	public List<string> Columns => _config.Columns;

	ITable IUniqueConstraint.Table => Table;
	IEnumerable<string> IUniqueConstraint.Columns => Columns;

	public UniqueConstraintInternal(TableInternal table, Config.UniqueConstraint config)
	{
		Table = table ?? throw new ArgumentNullException(nameof(table));
		_config = config ?? throw new ArgumentNullException(nameof(config));
	}

	public override string ToString()
	{
		return $"{Name}: {string.Join(", ", Columns ?? new List<string>())}";
	}
}
