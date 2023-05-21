using Envelope.Database.Config;

namespace Envelope.Database;

public interface IMetadataBuilder
{
	Model LoadMetadata(string connectionString, string databaseName);
}
