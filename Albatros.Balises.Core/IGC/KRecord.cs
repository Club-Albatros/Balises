using System;

namespace Albatros.Balises.Core.IGC
{
	/// <summary>
	/// Extension rec
	/// Ignored for now
	/// </summary>
	/// <remarks></remarks>
	public class KRecord : ExtendedRecord
	{

		public KRecord(string rec, DateTime flightdate, ExtensionRecord extensions) : base(rec, flightdate, extensions)
		{
		}

	}
}
