using System;
using System.Collections;
using Excido;
using Ortoped.GarpGEM;

namespace Ortoped.HelpClasses
{
	/// <summary>
	/// Summary description for Contacts.
	/// </summary>
	public class Contacts : GarpGEM.GarpCOM
	{
    //private Garp.Application app = new Garp.Application();
		private Garp.ITable	mI2H, mI2R;
    private Garp.ITabField mI2H_HN1, mI2H_HN2, mI2H_HN3, mI2H_DEL, mI2H_LAY, mI2H_NYC, mI2R_HN1, mI2R_HN2,
														mI2R_HN3, mI2R_TXT;

		private string lastContactKey = "";
    private ContactsDef[] mAllContacts = null;

    public struct ContactsDef
    {
      public string Name;
      public string CustomerNo;
    }

		public Contacts()
		{
			mI2H  = app.Tables.Item("I2H");
			mI2R  = app.Tables.Item("I2R");

			mI2H_HN1 = mI2H.Fields.Item("HN1");
			mI2H_HN2 = mI2H.Fields.Item("HN2");
			mI2H_HN3 = mI2H.Fields.Item("HN3");
      mI2H_DEL = mI2H.Fields.Item("DEL");
      mI2H_LAY = mI2H.Fields.Item("LAY");
      mI2H_NYC = mI2H.Fields.Item("NYC");
			
			mI2R_HN1 = mI2R.Fields.Item("HN1");
			mI2R_HN2 = mI2R.Fields.Item("HN2");
			mI2R_HN3 = mI2R.Fields.Item("HN3");
			mI2R_TXT = mI2R.Fields.Item("TXT");
		}

		public bool findContactPerson(string knr)
		{
			if(mI2H.Find("M22" + knr))
			{
				lastContactKey = mI2H_HN1.Value + mI2H_HN2.Value + mI2H_HN3.Value;
				mI2R.Find(lastContactKey + "  0" + "  1");
				mI2R.Next();
				if(lastContactKey == (mI2R_HN1.Value + mI2R_HN2.Value + mI2R_HN3.Value))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				lastContactKey = "";
				return false;
			}
		}

		public bool nextContact()
		{
			mI2R.Next();

			if((lastContactKey == (mI2R_HN1.Value + mI2R_HN2.Value + mI2R_HN3.Value)) && (!mI2R.Eof))
			{
				return false;
			}
			else
			{
				return true;
			}
		}

    public ContactsDef[] getContacts(string customerNo, string postcode)
    {
      ArrayList al = new ArrayList();
      ContactsDef cdf = new ContactsDef();

      if (mI2H.Find("M22" + customerNo))
      {
        lastContactKey = mI2H_HN1.Value + mI2H_HN2.Value + mI2H_HN3.Value;
        mI2R.Find(lastContactKey + "  0" + "  1");
        mI2R.Next();

        while ((lastContactKey == (mI2R_HN1.Value + mI2R_HN2.Value + mI2R_HN3.Value)) && (!mI2R.Eof))
        {
          if (CON_BEF.Trim().Equals("X"))
          {
            cdf.Name = CON_NAM;
            cdf.CustomerNo = ECS.noNULL(mI2H_NYC.Value);
            al.Add(cdf);
          }
          mI2R.Next();
        }
        mI2H.Next();
      }

      return (ContactsDef[])al.ToArray(typeof(ContactsDef));
    }

    /// <summary>
    /// Returns all contact regardless of customer.
    /// </summary>
    /// <param name="postcode">Code for selektion on contacts with specific position</param>
    /// <returns>CustomerDef[]</returns>
    public ContactsDef[] getContacts(string postcode)
    {
      ArrayList al = new ArrayList();
      ContactsDef cdf = new ContactsDef();
      int i = 0;

      if (mAllContacts == null)
      {
        mI2H.Find("M22");
        mI2H.Next();

        while (ECS.noNULL(mI2H_DEL.Value).Equals("M") && ECS.noNULL(mI2H_LAY.Value).Equals("22") && !mI2H.Eof)
        {
          lastContactKey = mI2H_HN1.Value + mI2H_HN2.Value + mI2H_HN3.Value;
          mI2R.Find(lastContactKey + "  0" + "  1");
          mI2R.Next();
          i++;

          while ((lastContactKey == (mI2R_HN1.Value + mI2R_HN2.Value + mI2R_HN3.Value)) && (!mI2R.Eof))
          {
            if (CON_BEF.Trim().Equals("X"))
            {
              cdf.Name = CON_NAM;
              cdf.CustomerNo = ECS.noNULL(mI2H_NYC.Value);
              al.Add(cdf);
            }
            mI2R.Next();
          }
          mI2H.Next();
        }
        mAllContacts = (ContactsDef[])al.ToArray(typeof(ContactsDef));
        return mAllContacts;
      }
      else
        return mAllContacts;
    }

		#region Propertys

		public string CON_NAM
		{
			get
			{
				try
				{
					return ECS.noNULL(mI2R_TXT.Value.Substring(0,20)); 
				}
				catch
				{
					return "";
				}
			}
		}

		public string CON_BEF
		{
			get
			{
				try
				{
					return ECS.noNULL(mI2R_TXT.Value.Substring(20,3)); 
				}
				catch 
				{
					return "";
				}
			}
		}


		#endregion

		~Contacts()
		{
			mI2H = null;
			mI2R = null;
		}

	}
}
