using Envelope.Text;
using Envelope.Validation;

namespace Envelope.Database.Config;

public class View : IValidable
{
	public string Name { get; set; }
	public int? Id { get; set; }
	public string Definition { get; set; }

	public List<IValidationMessage>? Validate(string? propertyPrefix = null, List<IValidationMessage>? parentErrorBuffer = null, Dictionary<string, object>? validationContext = null)
	{
		if (string.IsNullOrWhiteSpace(Name))
			parentErrorBuffer.Add(ValidationMessageFactory.Error($"{propertyPrefix.ConcatIfNotNullOrEmpty(".", nameof(Name))} == null"));

		if (string.IsNullOrWhiteSpace(Definition))
			parentErrorBuffer.Add(ValidationMessageFactory.Error($"{propertyPrefix.ConcatIfNotNullOrEmpty(".", nameof(Definition))} == null"));

		return parentErrorBuffer;
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
