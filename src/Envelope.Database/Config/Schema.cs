using Envelope.Text;
using Envelope.Validation;

namespace Envelope.Database.Config;

public class Schema : IValidable
{
	public string Name { get; set; }
	public string Alias { get; set; }
	public List<Table>? Tables { get; set; }
	public List<View>? Views { get; set; }

	public List<IValidationMessage>? Validate(string? propertyPrefix = null, List<IValidationMessage>? parentErrorBuffer = null, Dictionary<string, object>? validationContext = null)
	{
		parentErrorBuffer ??= new List<IValidationMessage>();

		if (string.IsNullOrWhiteSpace(Name))
			parentErrorBuffer.Add(ValidationMessageFactory.Error($"{propertyPrefix.ConcatIfNotNullOrEmpty(".", nameof(Name))} == null"));

		if (string.IsNullOrWhiteSpace(Alias))
			parentErrorBuffer.Add(ValidationMessageFactory.Error($"{propertyPrefix.ConcatIfNotNullOrEmpty(".", nameof(Alias))} == null"));

		if (0 < Tables?.Count)
			for (int i = 0; i < Tables.Count; i++)
				Tables[i].Validate(propertyPrefix.ConcatIfNotNullOrEmpty(".", $"{nameof(Tables)}[{i}]"), parentErrorBuffer, validationContext);

		if (0 < Views?.Count)
			for (int i = 0; i < Views.Count; i++)
				Views[i].Validate(propertyPrefix.ConcatIfNotNullOrEmpty(".", $"{nameof(Views)}[{i}]"), parentErrorBuffer, validationContext);

		return parentErrorBuffer;
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
