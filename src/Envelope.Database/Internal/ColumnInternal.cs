using Envelope.Database.Config;

namespace Envelope.Database.Internal;

internal class ColumnInternal : IColumn
{
	private readonly Column _config;

	public TableInternal Table { get; }

	public string Name => _config.Name;
	public string DatabaseType => _config.DatabaseType;
	public bool IsNullable => _config.IsNullable;
	public string? DefaultValue => _config.DefaultValue;
	public int CharacterMaximumLength => _config.CharacterMaximumLength;
	public int? Precision => _config.Precision;
	public int? Scale => _config.Scale;
	public bool IsIdentity => _config.IsIdentity;
	public long? IdentityStart => _config.IdentityStart;
	public long? IdentityIncrement => _config.IdentityIncrement;
	public long? LastIdentity => _config.LastIdentity;
	public string? ComputedColumnSql => _config.ComputedColumnSql;

	ITable IColumn.Table => Table;

	public ColumnInternal(TableInternal table, Column config)
	{
		Table = table ?? throw new ArgumentNullException(nameof(table));
		_config = config ?? throw new ArgumentNullException(nameof(config));
	}

	public override string ToString()
	{
		return Name;
	}
}
