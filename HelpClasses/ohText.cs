using System;
using Ortoped;
using Excido;

namespace Ortoped.HelpClasses
{
	/// <summary>
	/// Summary description for ohText.
	/// </summary>
	public class ohText : GarpGEM.GarpCOM
	{
		private string mTillagg = "", mOrdination = "", mNotering = "", mOHText = "";

		private Garp.ITable mOGK;
		private Garp.ITabField mONR, mRDC, mSQC, mTX1, mFAF;

		public ohText()
		{
			mOGK = app.Tables.Item("OGK");
			mONR = mOGK.Fields.Item("ONR");
			mRDC = mOGK.Fields.Item("RDC");
			mSQC = mOGK.Fields.Item("SQC");
			mTX1 = mOGK.Fields.Item("TX1");
			mFAF = mOGK.Fields.Item("FAF");
		}

		/// <summary>
		/// Hittar texter på orderrad noll. Typ av text styrs med FAF flaggan.
		///		"O" = Ordination
		///		"T" = Tillägg
		///		"N" = Notering
		///		"" eller "G" = Generell OH text
		/// </summary>
		/// <param name="onr"></param>
		/// <returns></returns>
		public bool findTexts(string onr)
		{
			mOrdination = "";
			mTillagg = "";
			mNotering = "";
			mOHText = "";

			mOGK.Find(onr.PadRight(6));
			mOGK.Next();
			
			// Om någon order hittades (inte null)
			if(mONR.Value != null)
			{
				while((ECS.noNULL(mONR.Value).Trim().Equals(ECS.noNULL(onr.Trim()))) && (ECS.noNULL(mRDC.Value).Trim().Equals("0")) && (!mOGK.Eof))
				{
					// Fyll på rätt textfält beroende av FAF flagga
					switch(mFAF.Value)
					{
						case "O" :
							mOrdination += ECS.noNULL(mTX1.Value).PadRight(60);
							break;
						case "T" :
							mTillagg += ECS.noNULL(mTX1.Value).PadRight(60);
							break;
						case "N" :
							mNotering += ECS.noNULL(mTX1.Value).PadRight(60);
							break;
                        case "G":
                            mOHText += ECS.noNULL(mTX1.Value);
                            break;
						case "1":
							mOHText += ECS.noNULL(mTX1.Value);
							break;
					}
					mOGK.Next();
				}
				return true;
			}
			else return false;
		}
		
		public string Tillagg
		{
			get
			{
				return ECS.noNULL(mTillagg);
			}
			
			set
			{
				mTillagg = value;
			}
		}
		
		public string Ordination
		{
			get
			{
				return ECS.noNULL(mOrdination);
			}
			
			set
			{
				mOrdination = value;
			}
		}

		public string Notering
		{
			get
			{
				return ECS.noNULL(mNotering);
			}
			
			set
			{
				mNotering = value;
			}
		}

		public string OHText
		{
			get
			{
				return ECS.noNULL(mOHText);
			}

			set
			{
				mOHText = value;
			}
		}

		public void deleteAllTextRows(string onr)
		{
			while(mOGK.Find(onr.PadRight(6) + "  0" + "  1"))
			{
        mOGK.Delete();
			}
		}

		public void saveText(string onr)
		{
			int k = 1;

			deleteAllTextRows(onr);

			while(ECS.noNULL(mOrdination).Length > 0)
			{
				mOGK.Insert(); 
				mONR.Value = onr;
				mRDC.Value = "  0";
				mSQC.Value = Convert.ToString(k).PadLeft(3); 
				if(mOrdination.Length > 60)
				{
					mTX1.Value = mOrdination.Substring(0,60);
					mOrdination = mOrdination.Remove(0,60);
				}
				else
				{
					mTX1.Value = mOrdination;
					mOrdination = "";
				}
				mFAF.Value = "O";
				mOGK.Post();
				k++;
			}
			
			while(ECS.noNULL(mTillagg).Length > 0)
			{
				mOGK.Insert(); 
				mONR.Value = onr;
				mRDC.Value = "  0";
				mSQC.Value = Convert.ToString(k).PadLeft(3); 
				if(mTillagg.Length > 60)
				{
					mTX1.Value = mTillagg.Substring(0,60);
					mTillagg = mTillagg.Remove(0,60);
				}
				else
				{
					mTX1.Value = mTillagg;
					mTillagg = "";
				}
				mFAF.Value = "T";
				mOGK.Post();
				k++;
			}

			while(ECS.noNULL(mNotering).Length > 0)
			{
				mOGK.Insert(); 
				mONR.Value = onr;
				mRDC.Value = "  0";
				mSQC.Value = Convert.ToString(k).PadLeft(3); 
				if(mNotering.Length > 60)
				{
					mTX1.Value = mNotering.Substring(0,60);
					mNotering = mNotering.Remove(0,60);
				}
				else
				{
					mTX1.Value = mNotering;
					mNotering = "";
				}
				mFAF.Value = "N";
				mOGK.Post();
				k++;
			}

      while (ECS.noNULL(mOHText).Length > 0)
      {
        mOGK.Insert();
        mONR.Value = onr;
        mRDC.Value = "  0";
        mSQC.Value = Convert.ToString(k).PadLeft(3);
        if (mOHText.Length > 60)
        {
          mTX1.Value = mOHText.Substring(0, 60);
          mOHText = mOHText.Remove(0, 60);
        }
        else
        {
          mTX1.Value = mOHText;
          mOHText = "";
        }
        mFAF.Value = "G";
        mOGK.Post();
        k++;
      }
    }


		~ohText()
		{
			mOGK = null;
			GC.Collect();
		}
	}
}
