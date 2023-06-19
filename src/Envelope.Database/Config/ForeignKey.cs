using Envelope.Validation;

namespace Envelope.Database.Config;

public class ForeignKey : IValidable
{
	public string Name { get; set; }
	public string Column { get; set; }
	public string ForeignSchemaAlias { get; set; }
	public string ForeignTableName { get; set; }
	public string ForeignColumnName { get; set; }
	public ReferentialAction? OnUpdateAction { get; set; }
	public ReferentialAction? OnDeleteAction { get; set; }

	public List<IValidationMessage>? Validate(
		string? propertyPrefix = null,
		ValidationBuilder? validationBuilder = null,
		Dictionary<string, object>? globalValidationContext = null,
		Dictionary<string, object>? customValidationContext = null)
	{
		validationBuilder ??= new ValidationBuilder();
		validationBuilder.SetValidationMessages(propertyPrefix, globalValidationContext)
			.IfNullOrWhiteSpace(Name)
			.IfNullOrWhiteSpace(Column)
			.IfNullOrWhiteSpace(ForeignSchemaAlias)
			.IfNullOrWhiteSpace(ForeignTableName)
			.IfNullOrWhiteSpace(ForeignColumnName)
			;

		return validationBuilder.Build();
	}

	public ForeignKey Clone()
		=> new()
		{
			Name = Name,
			Column = Column,
			ForeignSchemaAlias = ForeignSchemaAlias,
			ForeignTableName = ForeignTableName,
			ForeignColumnName = ForeignColumnName,
			OnUpdateAction = OnUpdateAction,
			OnDeleteAction = OnDeleteAction
		};

	public override string ToString()
	{
		return Name;
	}
}
