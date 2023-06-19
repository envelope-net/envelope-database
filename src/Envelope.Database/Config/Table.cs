using Envelope.Validation;

namespace Envelope.Database.Config;

public class Table : IValidable
{
	public string Name { get; set; }
	public int? Id { get; set; }
	public List<Column> Columns { get; set; }
	public PrimaryKey? PrimaryKey { get; set; }
	public List<ForeignKey>? ForeignKeys { get; set; }
	public List<UniqueConstraint>? UniqueConstraints { get; set; }
	public List<Index>? Indexes { get; set; }

	public List<IValidationMessage>? Validate(
		string? propertyPrefix = null,
		ValidationBuilder? validationBuilder = null,
		Dictionary<string, object>? globalValidationContext = null,
		Dictionary<string, object>? customValidationContext = null)
	{
		validationBuilder ??= new ValidationBuilder();
		validationBuilder.SetValidationMessages(propertyPrefix, globalValidationContext)
			.IfNullOrWhiteSpace(Name)
			.Validate(Columns)
			.Validate(PrimaryKey)
			.Validate(UniqueConstraints)
			.Validate(Indexes)
			.Validate(ForeignKeys)
			;

		return validationBuilder.Build();
	}

	public Table Clone()
		=> new()
		{
			Name = Name,
			Columns = Columns?.Select(x => x.Clone()).ToList()!,
			PrimaryKey = PrimaryKey?.Clone(),
			ForeignKeys = ForeignKeys?.Select(x => x.Clone()).ToList()!,
			UniqueConstraints = UniqueConstraints?.Select(x => x.Clone()).ToList()!,
			Indexes = Indexes?.Select(x => x.Clone()).ToList()!
		};

	public override string ToString()
	{
		return Name;
	}
}
