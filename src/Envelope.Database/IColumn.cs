namespace Envelope.Database;

public interface IColumn
{
	ITable Table { get; }

	string Name { get; }
	string DatabaseType { get; }
	bool IsNotNull { get; }
	string? DefaultValue { get; }
	int CharacterMaximumLength { get; }
	int? Precision { get; }
	int? Scale { get; }
	bool IsIdentity { get; }
	long? IdentityStart { get; }
	long? IdentityIncrement { get; }
	long? LastIdentity { get; }
	string? ComputedColumnSql { get; }

	IPrimaryKey? PrimaryKey { get; }
	IEnumerable<IUniqueConstraint> UniqueConstraints { get; }
	IEnumerable<IIndex> Indexes { get; }
	IForeignKey? TargetForeignKey { get; }
	IEnumerable<IForeignKey>? SourceForeignKeys { get; }
}
