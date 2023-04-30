using Envelope.Database.Config;
using Envelope.Validation;

namespace Envelope.Database.Internal;

internal class ModelInternal : IModel
{
	private readonly Model _config;

	public ProviderType ProviderType => _config.ProviderType;
	public string Name => _config.Name;
	public List<SchemaInternal> Schemas { get; }

	public List<IValidationMessage>? ConfigErrors { get; }
	public List<IValidationMessage>? ConfigWarnings { get; }

	public bool Built { get; private set; }

	IEnumerable<ISchema> IModel.Schemas => Schemas?.Cast<ISchema>()!;
	IEnumerable<IValidationMessage>? IModel.ConfigErrors => ConfigErrors;
	IEnumerable<IValidationMessage>? IModel.ConfigWarnings => ConfigWarnings;

	public ModelInternal(Model config, bool cloneConfig = true)
	{
		_config = config ?? throw new ArgumentNullException(nameof(config));

		if (cloneConfig)
			_config = _config.Clone();

		var validateResult = _config.Validate();
		ConfigErrors = validateResult?.Where(x => x.Severity == ValidationSeverity.Error).ToList();
		if (ConfigErrors?.Count == 0)
			ConfigErrors = null;

		ConfigWarnings = validateResult?.Where(x => x.Severity == ValidationSeverity.Warning).ToList();
		if (ConfigWarnings?.Count == 0)
			ConfigWarnings = null;

		Schemas = new();
	}

	private readonly object _lock = new();
	public ModelInternal Build()
	{
		if (Built)
			return this;
			//throw new InvalidOperationException("Already built");

		lock (_lock)
		{
			if (Built)
				return this;
				//throw new InvalidOperationException("Already built");

			if (ConfigErrors?.Any() == true)
				throw new InvalidOperationException("Config error");

			foreach (var schema in _config.Schemas)
				Schemas.Add(new SchemaInternal(this, schema));

			Built = true;
		}

		return this;
	}

	IModel IModel.Build()
		=> Build();

	public override string ToString()
	{
		return Name;
	}
}
