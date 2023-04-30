namespace Envelope.Database;

public interface IForeignKey
{
	ITable Table { get; }

	string Name { get; }
	string Column { get; }
	string ForeignSchemaName { get; }
	string ForeignTableName { get; }
	string ForeignColumnName { get; }
	ReferentialAction? OnUpdateAction { get; }
	ReferentialAction? OnDeleteAction { get; }
}
