using System;
using Ortoped;
using Ortoped.Definitions;
using GCS;

namespace Ortoped.HelpClasses
{
	/// <summary>
	/// Summary description for ohText.
	/// </summary>
	public class OrderRowText : GarpGEM.GarpCOM
	{
		private Garp.ITable mOGK;
		private Garp.ITabField mONR, mRDC, mSQC, mTX1, mFAF, mOGK_ONR, mOGK_RDC;
		private char[] mExcludeFlaggs = { 'B', 'O', 'N', 'E', 'T', 'A' };	// Används för att göra kontroll mot FAF flagga, alla textrader
																					// som har en av bokstäverna som FAF flagga hanteras på ett unikt sätt

		public OrderRowText()
		{
			mOGK = app.Tables.Item("OGK");
			mONR = mOGK.Fields.Item("ONR");
			mRDC = mOGK.Fields.Item("RDC");
			mSQC = mOGK.Fields.Item("SQC");
			mFAF = mOGK.Fields.Item("FAF");
			mTX1 = mOGK.Fields.Item("TX1");
			mOGK_ONR = mOGK.Fields.Item("ONR");
			mOGK_RDC = mOGK.Fields.Item("RDC");
		}

		/// <summary>
		/// Hämtar "vanliga" texter från orderrad som inte är flaggade på något sätt
		/// 
		/// </summary>
		/// <param name="onr"></param>
		/// <param name="row"></param>
		/// <returns></returns>
		public string getTexts(string onr, string row)
		{
			string s = "";

			mOGK.Find(onr.PadRight(6) + row.PadLeft(3));
			mOGK.Next();
			
			// Om någon order hittades (inte null)
			if(mONR.Value != null)
			{
				while((mONR.Value.Trim() == onr.Trim()) && (mRDC.Value.Trim() == row.Trim()) && (!mOGK.Eof))
				{
					// Hoppa över flaggade texter
					if(mFAF.Value.IndexOfAny(mExcludeFlaggs) == -1)
						s += mTX1.Value;
					mOGK.Next();
				}
			}

			return s;
		}

		public string getAidsText(string onr, string row)
		{
			string s = "";

			mOGK.Find(onr.PadRight(6) + row.PadLeft(3));
			mOGK.Next();

			// Om någon order hittades (inte null)
			if (mONR.Value != null)
			{
				while ((mONR.Value.Trim() == onr.Trim()) && (mRDC.Value.Trim() == row.Trim()) && (!mOGK.Eof))
				{
					// Hoppa över flaggade texter
					if (mFAF.Value.Equals("A"))
						s += mTX1.Value;
					mOGK.Next();
				}
			}

			return s;
		}

		private void deleteAidsTextRows(string onr, string row)
		{
      int irow = 1;

			try{
				if (mOGK.Find(onr.PadRight(6) + row.PadLeft(3) + irow.ToString().PadLeft(3)))
				{
					while (onr.Trim().Equals(mOGK_ONR.Value.Trim()) && row.Trim().Equals(mOGK_RDC.Value.Trim()) && !mOGK.Eof)
					{
						// Hoppa över flaggade texter
            if (mFAF.Value.Equals("A"))
              mOGK.Delete();
            else
              irow++;
            
            //irow++;
            if ((!mOGK.Find(onr.PadRight(6) + row.PadLeft(3) + irow.ToString().PadLeft(3))) || irow > 255)
              break;
					}
				}
			}catch { }
		}

		public void saveAidsText(string onr, string row, string text)
		{
			if (text == null)
				return;

			int k = 1;

			deleteAidsTextRows(onr, row);

			while (text.Length > 0)
			{
				mOGK.Insert();
				mONR.Value = onr;
				mRDC.Value = row;
				mSQC.Value = Convert.ToString(k).PadLeft(3);
				mFAF.Value = "A";
				if (text.Length > 60)
				{
					mTX1.Value = text.Substring(0, 60);
					text = text.Remove(0, 60);
				}
				else
				{
					mTX1.Value = text;
					text = "";
				}
				mOGK.Post();
				k++;
			}
		}

		private void deleteAllTextRows(string onr, string row)
		{
      int irow = 1;

			try
			{
				if (mOGK.Find(onr.PadRight(6) + row.PadLeft(3) + irow.ToString().PadLeft(3)))
				{
					while (onr.Trim().Equals(mOGK_ONR.Value.Trim()) && row.Trim().Equals(mOGK_RDC.Value.Trim()) && !mOGK.Eof)
					{
						// Hoppa över flaggade texter
						if (mFAF.Value.IndexOfAny(mExcludeFlaggs) == -1)
							mOGK.Delete();
            else
              irow++;

            //irow++;
            if ((!mOGK.Find(onr.PadRight(6) + row.PadLeft(3) + irow.ToString().PadLeft(3))) || irow > 255)
              break;
          }
				}
			}
			catch { }
		}

		/// <summary>
		/// Spara "vanliga" textrader som inte är flaggade på något sätt.
		/// 
		/// </summary>
		/// <param name="onr"></param>
		/// <param name="row"></param>
		/// <param name="text"></param>
		public void saveText(string onr, string row, string text)
		{
			if (text == null)
				return;

			int k = 1;

			deleteAllTextRows(onr, row);

			while (text.Length > 0)
			{
				mOGK.Insert();
				mONR.Value = onr;
				mRDC.Value = row;
				mSQC.Value = Convert.ToString(k).PadLeft(3);
				if (text.Length > 60)
				{
					mTX1.Value = text.Substring(0, 60);
					text = text.Remove(0, 60);
				}
				else
				{
					mTX1.Value = text;
					text = "";
				}
				mOGK.Post();
				k++;
			}
		}

		/// <summary>
		/// Skapa en ny rad textrad med ärendeinformation
		/// 
		/// </summary>
		/// <param name="onr"></param>
		/// <param name="row"></param>
		/// <param name="text"></param>
		public void addErrandText(string onr, string row, string text)
		{
			mOGK.Insert(); 
			mONR.Value = onr;
			mRDC.Value = row;
			mSQC.Value = "255";
			mFAF.Value = "B";
			mTX1.Value = text;
			mOGK.Post();
		}

		public void addOwnFeeConnection(string onr, string row, string text)
		{
			mOGK.Insert(); 
			mONR.Value = onr;
			mRDC.Value = row;
			mSQC.Value = "255";
			mFAF.Value = "E";
			mTX1.Value = "Kopplad till order - rad: " + text;
			mOGK.Post();
		}

    public void findOwnFeeConnection(string onr, string row, ref OrderRowDefinitions.OwnFee ownfee )
    {
      string s = "";
      string[] s2;

      mOGK.Find(onr.PadRight(6) + row.PadLeft(3));
      mOGK.Next();

      while ((mONR.Value.Trim().Equals(onr.Trim())) && (mRDC.Value.Trim().Equals(row.Trim())) && !mOGK.Eof)
      {
        if (GCF.noNULL(mFAF.Value).Equals("E"))
        {
          try
          {
            s = mTX1.Value.Substring(26);
            s2 = s.Split('-');

            ownfee.PatientsOrderNo = s2[0].Trim();
            ownfee.PatientsRowNo = s2[1].Trim();
            return;
          }
          catch (Exception e)
          {
            Log4Net.Logger.loggError(e, "", app.User, "OrderRowText.findOwnFeeConnection");
          }
        }

        mOGK.Next();
      }
    }

		/// <summary>
		/// Uppdaterar en befintlig textrad som inehåller ärendeinformation
		/// </summary>
		/// <param name="onr"></param>
		/// <param name="row"></param>
		/// <param name="sqc"></param>
		/// <param name="text"></param>
		public void updateErrandText(string onr, string row, string sqc, string text)
		{
			if(mOGK.Find(onr.PadRight(6) + row.PadLeft(3) + sqc.PadLeft(3)))
			{
				mTX1.Value = text;
				mOGK.Post();
			}
		}

		/// <summary>
		/// Finns ärendet upplagt som textrad på raden?
		/// 
		/// </summary>
		/// <param name="onr"></param>
		/// <param name="row"></param>
		/// <param name="errand"></param>
		/// <returns></returns>
		public string findErrand(string onr, string row, string errand)
		{
			mOGK.Find(onr.PadRight(6) + row.PadLeft(3));
      mOGK.Next();
			while((GCF.noNULL(mONR.Value).Trim().Equals(onr.Trim())) && (GCF.noNULL(mRDC.Value).Trim().Equals(row.Trim())) && !mOGK.Eof)
			{
        if (GCF.noNULL(mTX1.Value).PadRight(60).Substring(0, 7).Equals(errand))
					return mSQC.Value;

				mOGK.Next();
			}
			return "255";
		}

		~OrderRowText()
		{
			mOGK = null;
			GC.Collect();
		}
	}
}
