using Envelope.Text;
using Envelope.Validation;

namespace Envelope.Database.Config;

public class Table : IValidable
{
	public string Name { get; set; }
	public List<Column> Columns { get; set; }
	public PrimaryKey? PrimaryKey { get; set; }
	public List<ForeignKey>? ForeignKeys { get; set; }
	public List<UniqueConstraint>? UniqueConstraints { get; set; }
	public List<Index>? Indexes { get; set; }

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
				Columns[i].Validate(propertyPrefix.ConcatIfNotNullOrEmpty(".", $"{nameof(Columns)}[{i}]"), parentErrorBuffer, validationContext);
		}

		if (PrimaryKey == null)
			parentErrorBuffer.Add(ValidationMessageFactory.Warning($"{propertyPrefix.ConcatIfNotNullOrEmpty(".", nameof(PrimaryKey))} == null"));
		else
			PrimaryKey.Validate(propertyPrefix.ConcatIfNotNullOrEmpty(".", nameof(PrimaryKey)), parentErrorBuffer, validationContext);

		if (0 < UniqueConstraints?.Count)
			for (int i = 0; i < UniqueConstraints.Count; i++)
				UniqueConstraints[i].Validate(propertyPrefix.ConcatIfNotNullOrEmpty(".", $"{nameof(UniqueConstraints)}[{i}]"), parentErrorBuffer, validationContext);

		if (0 < Indexes?.Count)
			for (int i = 0; i < Indexes.Count; i++)
				Indexes[i].Validate(propertyPrefix.ConcatIfNotNullOrEmpty(".", $"{nameof(Indexes)}[{i}]"), parentErrorBuffer, validationContext);

		if (0 < ForeignKeys?.Count)
			for (int i = 0; i < ForeignKeys.Count; i++)
				ForeignKeys[i].Validate(propertyPrefix.ConcatIfNotNullOrEmpty(".", $"{nameof(ForeignKeys)}[{i}]"), parentErrorBuffer, validationContext);

		return parentErrorBuffer;
	}

	public Table Clone()
		=> new()
		{
			Name = Name,
			Columns = Columns?.Select(x => x.Clone()).ToList()!,
			PrimaryKey = PrimaryKey?.Clone(),
			ForeignKeys = ForeignKeys?.Select(x => x.Clone()).ToList()!,
			UniqueConstraints = UniqueConstraints?.Select(x => x.Clone()).ToList()!,
			Indexes = Indexes?.Select(x => x.Clone()).ToList()!
		};

	public override string ToString()
	{
		return Name;
	}
}
