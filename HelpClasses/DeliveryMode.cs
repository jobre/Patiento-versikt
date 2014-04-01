using System;
using System.Collections;
using Ortoped;

namespace Ortoped.HelpClasses
{
	/// <summary>
	/// Summary description for DeliveryMode.
	/// </summary>
	public class DeliveryMode : GarpGEM.GarpCOM
	{
		private Garp.ITable TA03;
		private Garp.ITabField  TX1, KEY;

		public DeliveryMode()
		{
			TA03 = app.Tables.Item("TA03");
			KEY = TA03.Fields.Item("KEY");
            TX1 = TA03.Fields.Item("TX1");
        }

		public string getNameByKey(string key)
		{
			if(TA03.Find(key))
				return TX1.Value;
			else
				return "";
		}

		public string getKeyByName(string key)
		{
			string s = "";

			TA03.First();
			while(!TA03.Eof)
			{
				try
				{
					if(key.Trim().Equals(TX1.Value.Trim()))
					{
						s = TA03.Fields.Item("KEY").Value;
						break;
					}
				}
				catch{}
				TA03.Next();
			}
			return s;	
		}

		public string[] getListOfNames()
		{
			ArrayList ar = new ArrayList();

			TA03.First();
			while(!TA03.Eof)
			{
				if(TX1.Value != null && KEY.Value.StartsWith("P"))
					ar.Add(TX1.Value);
				TA03.Next();
			}
			return (string[]) ar.ToArray(typeof(string));			
		}

	}
}
