using Envelope.Validation;

namespace Envelope.Database.Config;

public class Model : IValidable
{
	public ProviderType ProviderType { get; set; }
	public string Name { get; set; }
	public int? Id { get; set; }
	public string? CollationName { get; set; }
	public string? DefaultSchema { get; set; }
	public DateTime? CreationDate { get; set; }
	public List<Schema> Schemas { get; set; }

	public List<IValidationMessage>? Validate(
		string? propertyPrefix = null,
		ValidationBuilder? validationBuilder = null,
		Dictionary<string, object>? globalValidationContext = null,
		Dictionary<string, object>? customValidationContext = null)
	{
		validationBuilder ??= new ValidationBuilder();
		validationBuilder.SetValidationMessages(propertyPrefix, globalValidationContext)
			.IfNullOrWhiteSpace(Name)
			.Validate(Schemas)
			;

		return validationBuilder.Build();
	}

	public Model Clone()
		=> new()
		{
			ProviderType = ProviderType,
			Name = Name,
			Schemas = Schemas?.Select(x => x.Clone()).ToList()!
		};

	public override string ToString()
	{
		return Name;
	}
}
