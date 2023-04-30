using Envelope.Database.Config;

namespace Envelope.Database.Internal;

internal class ForeignKeyInternal : IForeignKey
{
	private readonly ForeignKey _config;

	public TableInternal Table { get; }

	public string Name => _config.Name;
	public string Column => _config.Column;
	public string ForeignSchemaName => _config.ForeignSchemaName;
	public string ForeignTableName => _config.ForeignTableName;
	public string ForeignColumnName => _config.ForeignColumnName;
	public ReferentialAction? OnUpdateAction => _config.OnUpdateAction;
	public ReferentialAction? OnDeleteAction => _config.OnDeleteAction;

	ITable IForeignKey.Table => Table;

	public ForeignKeyInternal(TableInternal table, ForeignKey config)
	{
		Table = table ?? throw new ArgumentNullException(nameof(table));
		_config = config ?? throw new ArgumentNullException(nameof(config));
	}

	public override string ToString()
	{
		return Name;
	}
}
