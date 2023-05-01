using Envelope.Database.Config;

namespace Envelope.Database.Internal;

internal class ForeignKeyInternal : IForeignKey
{
	public ForeignKey Config { get; }
	public TableInternal Table { get; }

	public string Name => Config.Name;
	public string Column => Config.Column;
	public string ForeignSchemaAlias => Config.ForeignSchemaAlias;
	public string ForeignTableName => Config.ForeignTableName;
	public string ForeignColumnName => Config.ForeignColumnName;
	public ReferentialAction? OnUpdateAction => Config.OnUpdateAction;
	public ReferentialAction? OnDeleteAction => Config.OnDeleteAction;

	public ColumnInternal FromColumn { get; private set; }
	public ColumnInternal ToColumn { get; private set; }

	ITable IForeignKey.Table => Table;
	IColumn IForeignKey.FromColumn => FromColumn;
	IColumn IForeignKey.ToColumn => ToColumn;

	public ForeignKeyInternal(TableInternal table, ForeignKey config)
	{
		Table = table ?? throw new ArgumentNullException(nameof(table));
		Config = config ?? throw new ArgumentNullException(nameof(config));
	}

	internal ForeignKeyInternal SetFromColumn(ColumnInternal column)
	{
		FromColumn = column;
		return this;
	}

	internal ForeignKeyInternal SetToColumn(ColumnInternal column)
	{
		ToColumn = column;
		return this;
	}

	public override string ToString()
	{
		return Name;
	}
}
