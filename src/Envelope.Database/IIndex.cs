namespace Envelope.Database;

public interface IIndex
{
	ITable Table { get; }

	string Name { get; }
	IEnumerable<string> Columns { get; }
}
