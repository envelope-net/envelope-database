using Envelope.Text;
using Envelope.Validation;

namespace Envelope.Database.Config;

public class PrimaryKey : IValidable
{
	public string Name { get; set; }
	public List<string> Columns { get; set; }

	public List<IValidationMessage>? Validate(string? propertyPrefix = null, List<IValidationMessage>? parentErrorBuffer = null, Dictionary<string, object>? validationContext = null)
	{
		parentErrorBuffer ??= new List<IValidationMessage>();

		if (string.IsNullOrWhiteSpace(Name))
			parentErrorBuffer.Add(ValidationMessageFactory.Error($"{propertyPrefix.ConcatIfNotNullOrEmpty(".", nameof(Name))} == null"));

		if (Columns == null)
		{
			parentErrorBuffer.Add(ValidationMessageFactory.Error($"{propertyPrefix.ConcatIfNotNullOrEmpty(".", nameof(Columns))} == null"));
		}
		else if (Columns.Count == 0)
		{
			parentErrorBuffer.Add(ValidationMessageFactory.Error($"{propertyPrefix.ConcatIfNotNullOrEmpty(".", nameof(Columns))}.{nameof(Columns.Count)} == 0"));
		}
		else
		{
			for (int i = 0; i < Columns.Count; i++)
				if (string.IsNullOrWhiteSpace(Columns[i]))
					parentErrorBuffer.Add(ValidationMessageFactory.Error($"{propertyPrefix.ConcatIfNotNullOrEmpty(".", $"{nameof(Columns)}[{i}]")} == null"));
		}

		return parentErrorBuffer;
	}

	public PrimaryKey Clone()
		=> new()
		{
			Name = Name,
			Columns = Columns?.ToList()!
		};

	public override string ToString()
	{
		return $"{Name}: {string.Join(", ", Columns ?? new List<string>())}";
	}
}
