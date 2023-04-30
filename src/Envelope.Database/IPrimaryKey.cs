namespace Envelope.Database;

public interface IPrimaryKey
{
	ITable Table { get; }

	string Name { get; }
	IEnumerable<string> Columns { get; }
}
