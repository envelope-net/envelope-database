using Envelope.Validation;

namespace Envelope.Database;

public interface IModel
{
	ProviderType ProviderType { get; }
	string Name { get; }
	IEnumerable<ISchema> Schemas { get; }

	IEnumerable<IValidationMessage>? ConfigErrors { get; }
	IEnumerable<IValidationMessage>? ConfigWarnings { get; }

	bool Built { get; }

	bool Build(out IModel model);
}
