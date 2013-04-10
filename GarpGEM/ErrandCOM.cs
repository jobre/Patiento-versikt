using System;
using GCS;

namespace Ortoped.GarpGEM
{
	/// <summary>
	/// Summary description for ErrandCOM.
	/// </summary>
	public class ErrandCOM : GarpCOM 
	{
		
		private string lastContactKey = "";
		protected Garp.IAppointment mErrand;

		private Garp.ITable mI2H, mI2R, mTA04;
		private Garp.ITabField	mI2H_HN1, mI2H_HN2, mI2H_HN3, mI2R_HN1, mI2R_HN2,
														mI2R_HN3, mI2R_TXT;

		public ErrandCOM()
		{
			mI2H  = app.Tables.Item("I2H");
			mI2R  = app.Tables.Item("I2R");
            mTA04 = app.Tables.Item("TA04");

			mI2H_HN1 = mI2H.Fields.Item("HN1");
			mI2H_HN2 = mI2H.Fields.Item("HN2");
			mI2H_HN3 = mI2H.Fields.Item("HN3");
			
			mI2R_HN1 = mI2R.Fields.Item("HN1");
			mI2R_HN2 = mI2R.Fields.Item("HN2");
			mI2R_HN3 = mI2R.Fields.Item("HN3");
			mI2R_TXT = mI2R.Fields.Item("TXT");
			mErrand = app.Appointment;
		}

		protected bool findFirstErrand(string knr)
		{
			if(mI2H.Find("M23" + knr))
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
		
		protected bool nextErrand()
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

        protected string getHandlerNameById(string id)
        {
            if (mTA04.Find(id))
                return mTA04.Fields.Item("TX1").Value;
            else
                return "";

        }


		# region Propertys

		protected string StartDatum
		{
			get
			{
				return GCF.noNULL(mI2R_TXT.Value.Substring(0,8)).Trim(); 
			}
		}

		protected string Dagar
		{
			get
			{
				return GCF.noNULL(mI2R_TXT.Value.Substring(52,2)).Trim(); 
			}
		}

		protected string Starttid
		{
			get
			{
                return GCF.noNULL(mI2R_TXT.Value.Substring(8, 5)).Trim(); 
			}
		}

		protected string Tidsperiod
		{
			get
			{
                return GCF.noNULL(mI2R_TXT.Value.Substring(16, 5)).Trim(); 
			}
		}

		protected string Aktivitet
		{
			get
			{
                return GCF.noNULL(mI2R_TXT.Value.Substring(24, 3)).Trim(); 
			}
		}

		protected string Kontaktperson
		{
			get
			{
                return GCF.noNULL(mI2R_TXT.Value.Substring(27, 20)).Trim(); 
			}
		}

		protected string Ansvarig
		{
			get
			{
                return GCF.noNULL(mI2R_TXT.Value.Substring(47, 2)).Trim(); 
			}
		}

		protected string Prioritet
		{
			get
			{
                return GCF.noNULL(mI2R_TXT.Value.Substring(69, 2)).Trim(); 
			}
		}

		protected string Refnr
		{
			get
			{
                return GCF.noNULL(mI2R_TXT.Value.Substring(55, 15)).Trim(); 
			}
		}

		protected string Status
		{
			get
			{
				return GCF.noNULL(mI2R_TXT.Value.Substring(71,1)); 
			}
		}

		protected string Sekretess
		{
			get
			{
				return GCF.noNULL(mI2R_TXT.Value.Substring(72,1)); 
			}
		}

		/// <summary>
		/// ** Fält från Arkiv Skåp Kund, Låda 23 **
		/// Dokumentreferens
		/// Start	=	102
		/// Längd =	7
		/// Kol		=	78
		/// </summary>
		protected string ArkivID
		{
			get
			{
				return mI2R_TXT.Value.Substring(73,7); 
			}
		}

		protected string Kod1
		{
			get
			{
				return mI2R_TXT.Value.Substring(53,1); 
			}
		}

		protected string Kod2
		{
			get
			{
				return mI2R_TXT.Value.Substring(54,2); 
			}
		}

	# endregion

	}
}
