using System;
using System.Collections;
using Excido;

namespace Ortoped.GarpGEM
{
	/// <summary>
	/// Summary description for ArticleCOM.
	/// </summary>
	public class ProductCOM : GarpCOM 
	{
		private Garp.ITable mAGA, mRKPX;
    private Garp.ITabField mAGA_ANR, mAGA_BEN, mAGA_PRI, mRKPX_KNR, mRKPX_TX1;

		public ProductCOM()
		{
			mAGA = app.Tables.Item("AGA");
      mRKPX = app.Tables.Item("RKPX");

      mAGA_ANR = mAGA.Fields.Item("ANR");
			mAGA_BEN = mAGA.Fields.Item("BEN");
			mAGA_PRI = mAGA.Fields.Item("PRI");

      mRKPX_KNR = mRKPX.Fields.Item("KNR");
      mRKPX_TX1 = mRKPX.Fields.Item("TX1");
    }


		/// <summary>
		/// Söker artikel genom index för artikelnummer
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool findByID(string id)
		{
			mAGA.IndexNo = 1;
			return mAGA.Find(id);
		}

		public bool findByName(string name)
		{
			mAGA.IndexNo = 2;
			return mAGA.Find(name);
		}

		/// <summary>
		/// Sök artikel via artikelnummer och returnera Benämning som string
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public string getNameById(string id)
		{
			mAGA.IndexNo = 1;
			if(mAGA.Find(id))
				return mAGA_BEN.Value;
			else
				return "";
		}
		public bool next()
		{
			mAGA.Next();
			return mAGA.Eof;
		}

    public string[] getProductsGroup()
    {
      ArrayList al = new ArrayList();

      mRKPX.First();

      while (!mRKPX.Eof)
      {
        al.Add(mRKPX_KNR.Value + " - " + mRKPX_TX1.Value);
        mRKPX.Next();
      }

      return (string[])al.ToArray(typeof(string));
    }

		#region Propertys

		public string ANR
		{
			get
			{
        return ECS.noNULL(mAGA_ANR.Value);
			}
		}

		public string BEN
		{
			get
			{
        return ECS.noNULL(mAGA_BEN.Value);
			}
		}

		public string PRI
		{
			get
			{
        return ECS.noNULL(mAGA_PRI.Value);
			}
		}

		public bool EOF
		{
			get
			{
				return mAGA.Eof;
			}
		}


		#endregion

	}
}
