using Envelope.Text;
using Envelope.Validation;

namespace Envelope.Database.Config;

public class Column : IValidable
{
	public string Name { get; set; }
	public string DatabaseType { get; set; }
	public bool IsNullable { get; set; }
	public string? DefaultValue { get; set; }
	public int CharacterMaximumLength { get; set; }
	public int? Precision { get; set; }
	public int? Scale { get; set; }
	public bool IsIdentity { get; set; }
	public long? IdentityStart { get; set; }
	public long? IdentityIncrement { get; set; }
	public long? LastIdentity { get; set; }
	public string? ComputedColumnSql { get; set; }

	public List<IValidationMessage>? Validate(string? propertyPrefix = null, List<IValidationMessage>? parentErrorBuffer = null, Dictionary<string, object>? validationContext = null)
	{
		parentErrorBuffer ??= new List<IValidationMessage>();

		if (string.IsNullOrWhiteSpace(Name))
			parentErrorBuffer.Add(ValidationMessageFactory.Error($"{propertyPrefix.ConcatIfNotNullOrEmpty(".", nameof(Name))} == null"));

		if (string.IsNullOrWhiteSpace(DatabaseType))
			parentErrorBuffer.Add(ValidationMessageFactory.Error($"{propertyPrefix.ConcatIfNotNullOrEmpty(".", nameof(DatabaseType))} == null"));

		return parentErrorBuffer;
	}

	public Column Clone()
		=> new()
		{
			Name = Name,
			DatabaseType = DatabaseType,
			IsNullable = IsNullable,
			DefaultValue = DefaultValue,
			CharacterMaximumLength = CharacterMaximumLength,
			Precision = Precision,
			Scale = Scale,
			IsIdentity = IsIdentity,
			IdentityStart = IdentityStart,
			IdentityIncrement = IdentityIncrement,
			LastIdentity = LastIdentity,
			ComputedColumnSql = ComputedColumnSql
		};

	public override string ToString()
	{
		return Name;
	}
}
