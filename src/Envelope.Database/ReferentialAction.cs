namespace Envelope.Database;

public enum ReferentialAction
{
	NoAction = 1,
	Restrict = 2,
	Cascade = 3,
	SetNull = 4,
	SetDefault = 5
}
