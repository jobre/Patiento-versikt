using System;
using System.Collections;

namespace Ortoped.HelpClasses
{
	/// <summary>
	/// Summary description for Prislista.
	/// </summary>
	public class Prislista : GarpGEM.GarpCOM
	{
		private Garp.ITable mAPD;
		private Garp.ITabField mPRL;
		private Garp.ITabField mBEN;

		public Prislista()
		{
			mAPD = app.Tables.Item("APD");
			mPRL = mAPD.Fields.Item("PRL");
			mBEN = mAPD.Fields.Item("BEN");
		}

		public string[] getPricelists()
		{
			ArrayList ar = new ArrayList(0);

			mAPD.First();
			while(!mAPD.Eof)
			{
				if(mPRL.Value != null)
					ar.Add(mPRL.Value + " - " + mBEN.Value);
				mAPD.Next();
			}

			return (string[]) ar.ToArray(typeof(string));
		}

		public string getPriceListById(string id)
		{
			if(mAPD.Find("K" + id))
				return mPRL.Value + " - " + mBEN.Value;
			else
				return "";
		}
	}
}
