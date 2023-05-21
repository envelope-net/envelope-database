using Envelope.Text;
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

	public List<IValidationMessage>? Validate(string? propertyPrefix = null, List<IValidationMessage>? parentErrorBuffer = null, Dictionary<string, object>? validationContext = null)
	{
		parentErrorBuffer ??= new List<IValidationMessage>();

		if (string.IsNullOrWhiteSpace(Name))
			parentErrorBuffer.Add(ValidationMessageFactory.Error($"{propertyPrefix.ConcatIfNotNullOrEmpty(".", nameof(Name))} == null"));

		if (Schemas == null)
		{
			parentErrorBuffer.Add(ValidationMessageFactory.Error($"{propertyPrefix.ConcatIfNotNullOrEmpty(".", nameof(Schemas))} == null"));
		}
		else if (Schemas.Count == 0)
		{
			parentErrorBuffer.Add(ValidationMessageFactory.Error($"{propertyPrefix.ConcatIfNotNullOrEmpty(".", nameof(Schemas))}.{nameof(Schemas.Count)} == 0"));
		}
		else
		{
			for (int i = 0; i < Schemas.Count; i++)
				Schemas[i].Validate(propertyPrefix.ConcatIfNotNullOrEmpty(".", $"{nameof(Schemas)}[{i}]"), parentErrorBuffer, validationContext);
		}

		return parentErrorBuffer;
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
