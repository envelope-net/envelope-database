namespace Envelope.Database;

public interface IView
{
	ISchema Schema { get; }

	string Name { get; }
	string Alias { get; }
	string Definition { get; }
}
