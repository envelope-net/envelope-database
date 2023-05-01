namespace Envelope.Database;

public interface IIndex
{
	ITable Table { get; }

	string Name { get; }
	IEnumerable<IColumn> Columns { get; }
}
