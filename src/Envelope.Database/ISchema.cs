namespace Envelope.Database;

public interface ISchema
{
	IModel Model { get; }

	string Name { get; }
	string Alias { get; }
	IEnumerable<ITable>? Tables { get; }
	IEnumerable<IView>? Views { get; }
}
