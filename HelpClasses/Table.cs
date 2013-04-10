using System;
using System.Collections;
using Ortoped;

namespace Ortoped.HelpClasses
{
	/// <summary>
	/// Summary description for Diagnos.
	/// </summary>
	public class Table : GarpGEM.GarpCOM
	{
		private string mPrefix;
		private Garp.ITable mTA;
		private Garp.ITabField mKEY;
		private Garp.ITabField mTX1;
		private Garp.ITabField mTX2;
		private Garp.ITabField mKD1;


		public struct TableFields
		{
			public string ID;
			public string TX1;
			public string TX2;
			public string KD1;
			public bool Active;
		}


		public Table()
		{
			mTA = app.Tables.Item("TA");
			mKEY = mTA.Fields.Item("KEY");
			mTX1 = mTA.Fields.Item("TX1");
			mTX2 = mTA.Fields.Item("TX2");
			mKD1 = mTA.Fields.Item("KD1");
		}

		public Table(string prefix)
		{
			mPrefix = prefix;
			connectToGarp("","");

			mTA = app.Tables.Item("TA");
			mKEY = mTA.Fields.Item("KEY");
			mTX1 = mTA.Fields.Item("TX1");
			mTX2 = mTA.Fields.Item("TX2");
			mKD1 = mTA.Fields.Item("KD1");
		}

		public string getTx1ByKey(string id)
		{
			if(mTA.Find(mPrefix + id))
				return mTX1.Value;
			else
				return "";
		}

		public TableFields getTable(string id)
		{
			TableFields d = new TableFields();
			
			if(mTA.Find(mPrefix + id))
			{
				d.ID = id;
				d.TX1 = mTX1.Value;
				d.TX2 = mTX2.Value;
				d.KD1 = mKD1.Value;
				d.Active = true;
			}
			else
			{
				d.ID = "";
				d.TX1 = "";
				d.TX2 = "";
				d.KD1 = "";
				d.Active = true;
			}
			return d;
		}

		public string[] getListOfReservations()
		{
			ArrayList s = new ArrayList();
			
			mTA.Find(mPrefix);
			mTA.Next();
			while((mKEY.Value.StartsWith(mPrefix)) || mTA.Eof)
			{
				s.Add(mTX1.Value);
				mTA.Next();
			}
			return (string[]) s.ToArray(typeof(string));
		}

		public string getIdByTX1(string tx1)
		{
			if(tx1 == null)
				return " ";

			mTA.Find(mPrefix);
			mTA.Next();
			while((mKEY.Value.StartsWith(mPrefix)) || mTA.Eof)
			{
				if(mTX1.Value.Trim() == tx1.Trim())
				{
					return mKEY.Value.Substring(2,1); 				
				}
				mTA.Next();
			}
			return "";
		}
	}
}
