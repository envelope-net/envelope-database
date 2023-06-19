using Envelope.Validation;

namespace Envelope.Database.Config;

public class View : IValidable
{
	public string Name { get; set; }
	public int? Id { get; set; }
	public string Definition { get; set; }

	public List<IValidationMessage>? Validate(
		string? propertyPrefix = null,
		ValidationBuilder? validationBuilder = null,
		Dictionary<string, object>? globalValidationContext = null,
		Dictionary<string, object>? customValidationContext = null)
	{
		validationBuilder ??= new ValidationBuilder();
		validationBuilder.SetValidationMessages(propertyPrefix, globalValidationContext)
			.IfNullOrWhiteSpace(Name)
			.IfNullOrWhiteSpace(Definition)
			;

		return validationBuilder.Build();
	}

	public View Clone()
		=> new()
		{
			Name = Name,
			Definition = Definition
		};

	public override string ToString()
	{
		return Name;
	}
}
