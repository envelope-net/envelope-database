namespace Envelope.Database;

public interface IColumn
{
	ITable Table { get; }

	string Name { get; }
	string DatabaseType { get; }
	bool IsNullable { get; }
	string? DefaultValue { get; }
	int CharacterMaximumLength { get; }
	int? Precision { get; }
	int? Scale { get; }
	bool IsIdentity { get; }
	long? IdentityStart { get; }
	long? IdentityIncrement { get; }
	long? LastIdentity { get; }
	string? ComputedColumnSql { get; }
}
