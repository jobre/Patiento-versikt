using System;
using System.Collections;
using Excido;

namespace Ortoped.GarpGEM
{
	/// <summary>
	/// Summary description for SaleCOM.
	/// </summary>
	public class SaleCOM : GarpCOM
	{
		protected Garp.ITable mTA04;
		protected Garp.ITabField mKEY, mTX1, mKST;

		public SaleCOM()
		{
			mTA04 = app.Tables.Item("TA04");
			mKEY = mTA04.Fields.Item("KEY");
			mTX1 = mTA04.Fields.Item("TX1");
      mKST = mTA04.Fields.Item("KST");
    }

		public void first()
		{
			mTA04.First();
		}

    /// <summary>
    /// Get list of all signatures with matching costId. If costId is blank then all signatures are returned
    /// </summary>
    /// <param name="costId">Costid on signatures that must match</param>
    /// <returns>string[] with all signatures on selected costId</returns>
    public string[] getListOfAllSignatures(string costId)
    {
      ArrayList s = new ArrayList();
      first();

      do
      {
        // If costid on user is "" or if costid on salesman is "" or if costid on salesman match users costid
        if (ECS.noNULL(mKST.Value).Equals(costId) || ECS.noNULL(mKST.Value).Equals("") || costId.Equals(""))
          s.Add(mTX1.Value);
      }
      while (!next());

      return (string[])s.ToArray(typeof(string));
    }

    /// <summary>
    /// Get list of all handler with matching costId. If costId is blank then all handler are returned
    /// </summary>
    /// <param name="costId">Costid on handlers that must match</param>
    /// <returns>string[] with all handler on selected costId</returns>
    public string[] getListOfAllHandler(string costId)
    {
      return getListOfAllSignatures(costId);
    }

		public bool findByKey(string key)
		{
			return mTA04.Find(key);
		}

		public bool next()
		{
			mTA04.Next();
			return mTA04.Eof;
		}

		#region Propertys

		public string KEY
		{
			get
			{
				return mKEY.Value == null ? "" : mKEY.Value;			
			}
		}

		public string TX1
		{
			get
			{
				return mTX1.Value == null ? "" : mTX1.Value;			
			}
		}

		#endregion

	}
}
