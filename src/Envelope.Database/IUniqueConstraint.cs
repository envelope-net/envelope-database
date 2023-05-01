namespace Envelope.Database;

public interface IUniqueConstraint
{
	ITable Table { get; }

	string Name { get; }
	IEnumerable<IColumn> Columns { get; }
}
