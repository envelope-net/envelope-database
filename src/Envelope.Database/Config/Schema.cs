using Envelope.Text;
using Envelope.Validation;

namespace Envelope.Database.Config;

public class Schema : IValidable
{
	public string Name { get; set; }
	public string Alias { get; set; }
	public int? Id { get; set; }
	public List<Table>? Tables { get; set; }
	public List<View>? Views { get; set; }

	public List<IValidationMessage>? Validate(
		string? propertyPrefix = null,
		ValidationBuilder? validationBuilder = null,
		Dictionary<string, object>? globalValidationContext = null,
		Dictionary<string, object>? customValidationContext = null)
	{
		validationBuilder ??= new ValidationBuilder();
		validationBuilder.SetValidationMessages(propertyPrefix, globalValidationContext)
			.IfNullOrWhiteSpace(Name)
			.IfNullOrWhiteSpace(Alias)
			.Validate(Tables)
			.Validate(Views)
			;

		return validationBuilder.Build();
	}

	public Schema Clone()
		=> new()
		{
			Name = Name,
			Alias = Alias,
			Tables = Tables?.Select(x => x.Clone()).ToList(),
			Views = Views?.Select(x => x.Clone()).ToList()
		};

	public override string ToString()
	{
		return Name;
	}
}
