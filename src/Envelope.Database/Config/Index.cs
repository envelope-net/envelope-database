using Envelope.Validation;

namespace Envelope.Database.Config;

public class Index : IValidable
{
	public string Name { get; set; }
	public List<string> Columns { get; set; }

	public List<IValidationMessage>? Validate(
		string? propertyPrefix = null,
		ValidationBuilder? validationBuilder = null,
		Dictionary<string, object>? globalValidationContext = null,
		Dictionary<string, object>? customValidationContext = null)
	{
		validationBuilder ??= new ValidationBuilder();
		validationBuilder.SetValidationMessages(propertyPrefix, globalValidationContext)
			.IfNullOrWhiteSpace(Name)
			.IfNullOrEmpty(
				Columns,
				onSuccess: x =>
				{
					for (int i = 0; i < Columns.Count; i++)
						x.IfNullOrWhiteSpace(Columns[i], $"{nameof(Columns)}[{i}]");
				});

		return validationBuilder.Build();
	}

	public Index Clone()
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
