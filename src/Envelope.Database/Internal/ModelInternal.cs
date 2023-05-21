using Envelope.Database.Config;
using Envelope.Validation;

namespace Envelope.Database.Internal;

internal class ModelInternal : IModel
{
	public Model Config { get; }
	public ProviderType ProviderType => Config.ProviderType;
	public string Name => Config.Name;
	public int? Id => Config.Id;
	public string? CollationName => Config.CollationName;
	public string? DefaultSchema => Config.DefaultSchema;
	public DateTime? CreationDate => Config.CreationDate;
	public List<SchemaInternal> Schemas { get; }

	public List<IValidationMessage>? ConfigErrors { get; private set; }
	public List<IValidationMessage>? ConfigWarnings { get; }

	public bool Built { get; private set; }

	IEnumerable<ISchema> IModel.Schemas => Schemas?.Cast<ISchema>()!;
	IEnumerable<IValidationMessage>? IModel.ConfigErrors => ConfigErrors;
	IEnumerable<IValidationMessage>? IModel.ConfigWarnings => ConfigWarnings;

	public ModelInternal(Model config, bool cloneConfig = true)
	{
		Config = config ?? throw new ArgumentNullException(nameof(config));

		if (cloneConfig)
			Config = Config.Clone();

		var validateResult = Config.Validate();
		ConfigErrors = validateResult?.Where(x => x.Severity == ValidationSeverity.Error).ToList();
		if (ConfigErrors?.Count == 0)
			ConfigErrors = null;

		ConfigWarnings = validateResult?.Where(x => x.Severity == ValidationSeverity.Warning).ToList();
		if (ConfigWarnings?.Count == 0)
			ConfigWarnings = null;

		Schemas = new();
	}

	private readonly object _lock = new();
	public bool Build(out ModelInternal model)
	{
		model = this;

		if (Built || ConfigErrors?.Any() == true)
			return ConfigErrors?.Any() != true;

		lock (_lock)
		{
			if (Built || ConfigErrors?.Any() == true)
				return ConfigErrors?.Any() != true;

			foreach (var schema in Config.Schemas)
				Schemas.Add(new SchemaInternal(this, schema));

			foreach (var fromSchema in Schemas)
			{
				if (0 < fromSchema.Tables?.Count)
				{
					foreach (var fromTable in fromSchema.Tables.Where(x => 0 < x.ForeignKeys?.Count))
					{
						foreach (var foreignKey in fromTable.ForeignKeys!)
						{
							var fromColumn = fromTable.Columns.FirstOrDefault(x => x.Name == foreignKey.Column);
							if (fromColumn == null)
							{
								AddError(ValidationMessageFactory.Error($"Invalid FK: {fromSchema.Name}.{fromTable.Name}.{foreignKey.Name} | {nameof(fromColumn)} == null"));
								continue;
							}

							var toSchema = Schemas.FirstOrDefault(x => x.Alias == foreignKey.ForeignSchemaAlias);
							var toTable = toSchema?.Tables?.FirstOrDefault(x => x.Name == foreignKey.ForeignTableName);
							var toColumn = toTable?.Columns.FirstOrDefault(x => x.Name == foreignKey.ForeignColumnName);
							if (toColumn == null)
							{
								AddError(ValidationMessageFactory.Error($"Invalid FK: {fromSchema.Name}.{fromTable.Name}.{foreignKey.Name} | {nameof(toColumn)} == null"));
								continue;
							}

							foreignKey.SetFromColumn(fromColumn);
							foreignKey.SetToColumn(toColumn);
							fromColumn.SetTargetForeignKey(foreignKey);
							toColumn.AddSourceForeignKey(foreignKey);
						}
					}
				}
			}

			if (ConfigErrors?.Any() == true)
				return ConfigErrors?.Any() != true;

			Built = true;
		}

		return ConfigErrors?.Any() != true;
	}

	public void AddError(IValidationMessage error)
	{
		ConfigErrors ??= new List<IValidationMessage>();
		ConfigErrors.Add(error);
	}

	bool IModel.Build(out IModel model)
	{
		var result = Build(out var m);
		model = m;
		return result;
	}

	public override string ToString()
	{
		return Name;
	}
}
