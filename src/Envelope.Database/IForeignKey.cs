namespace Envelope.Database;

public interface IForeignKey
{
	ITable Table { get; }

	string Name { get; }
	string Column { get; }
	string ForeignSchemaAlias { get; }
	string ForeignTableName { get; }
	string ForeignColumnName { get; }
	ReferentialAction? OnUpdateAction { get; }
	ReferentialAction? OnDeleteAction { get; }

	IColumn FromColumn { get; }
	IColumn ToColumn { get; }
}
