namespace Envelope.Database;

public interface ITable
{
	ISchema Schema { get; }

	string Name { get; }
	string Alias { get; }
	IEnumerable<IColumn> Columns { get; }
	IPrimaryKey? PrimaryKey { get; }
	IEnumerable<IForeignKey>? ForeignKeys { get; }
	IEnumerable<IUniqueConstraint>? UniqueConstraints { get; }
	IEnumerable<IIndex>? Indexes { get; }
}
