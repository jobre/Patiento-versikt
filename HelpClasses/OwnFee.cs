using System;
using System.Windows.Forms;
using Ortoped.GarpFunctions;
using Ortoped.Definitions;
using Excido;

namespace Ortoped.HelpClasses
{
    /// <summary>
    /// Summary description for OwnFee.
    /// </summary>
    public class OwnFee : GarpGEM.GarpCOM
    {
        private OrderRowDefinitions.OrderRow mOr = new OrderRowDefinitions.OrderRow();
        private OrderRowText orText = new OrderRowText();
        private GarpGEM.ProductCOM product = new Ortoped.GarpGEM.ProductCOM();
        private string mOnr = "", mAidId = "", mKnr = "", mBvk = "", mAmount = "0", mNamn = "",
                                        mAdress1 = "", mAdress2 = "", mOrt = "", mFsNr = "A", mProductGroup = "";

        public OwnFee()
        {

        }

        public struct OwnFeeResult
        {
            public string OrderNo;
            public string OrderRow;
            public string FsNo;
        }

        /// <summary>
        /// Skapar egenavgift på patienten samt uppdaterar orginlaordern med mostsvarande rad.
        /// 
        /// </summary>
        /// <param name="knr"></param>
        /// <param name="onr"></param>
        /// <param name="aidid"></param>
        /// <param name="bvk"></param>
        /// <param name="amount"></param>
        /// <param name="namn"></param>
        /// <param name="adress"></param>
        /// <param name="ort"></param>
        /// <returns>Följesedelsnummer som leveransen av patientens egenavgift utfördes på</returns>
        public string createOwnFee(string knr, string onr, string aidid, string bvk, string amount, string namn, string adress1, string adress2, string ort, string productgroup)
        {
            mOnr = onr;
            mAidId = aidid;
            mKnr = knr;
            mBvk = bvk;
            mAmount = amount;
            mNamn = namn;
            mAdress1 = adress1;
            mAdress2 = adress2;
            mOrt = ort;
            mProductGroup = productgroup;

            string sPatOnrRow = createRowOnPatientsOrder();

            if (GCS.GCF.noNULL(sPatOnrRow).Equals(""))
            {
                MessageBox.Show("Det gick inte att skapa egenavgift, det var inte möjligt att skapa en order på patienten av någon anledning", "Fel vid orderupplägg", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return "";
            }

            string sOrgOnrRow = createRowOnOriginalOrder();

            // Updatera orginalordern med ordernummer - rad från patientordern
            string[] s1 = sOrgOnrRow.Split('-');
            orText.addOwnFeeConnection(s1[0].Trim(), s1[1].Trim(), sPatOnrRow);

            // Uppdatera patientordern med ordernummer - rad från orginalordern
            string[] s2 = sPatOnrRow.Split('-');
            orText.addOwnFeeConnection(s2[0].Trim(), s2[1].Trim(), sOrgOnrRow);

            return mFsNr;
        }

        private string createRowOnOriginalOrder()
        {
            OrderRowFunc oOR = new OrderRowFunc();
            string rownr = oOR.addNewRow(mOnr, mAidId, "EA");

            // Orderrad på grundordern
            mOr.OrderNo = mOnr;
            mOr.AidNr = mAidId;
            mOr.Rad = rownr;
            mOr.Artikel = "EA";
            mOr.Antal = "0";
            mOr.Beloppsrad = true;

            try
            {
                if (double.Parse(mAmount) > 0)
                    mOr.APris = "-" + mAmount;
                else
                    mOr.APris = mAmount.Replace("-", "");
            }
            catch
            {
                MessageBox.Show(null, "Inget gilltigt belopp", "Fel inmatning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            mOr.AidDate = "";
            mOr.SelectedHandler = "";
            mOr.Prodstatus = "";
            mOr.LevTid = "";
            mOr.Text = "";
            mOr.EA_ProductGroup = mProductGroup;

            oOR.saveOrderRow(mOr, false, false);

            return mOnr.PadRight(6) + " - " + rownr;

            // *****	Borttagen för att få egenavgifter med på samma FS som hjälpmedel
            //				Överenskommelse med PN 2005-09-29
            //			oOR.deliverOwnFeeRow(mOr.OrderNo,mOr.Rad, mOr.Antal,"","A");
        }

        private string createRowOnPatientsOrder()
        {
            orderFunc oOH = new orderFunc();
            OrderRowFunc oOR = new OrderRowFunc();
            OrderHeadDefinition oh = oOH.addOH(mKnr);

            if (oh == null)
            {
                return "";
            }

            oh.OrderType = "E";
            oh.PaymentCondition = mBvk;
            oOH.saveOH(oh);
            oOH.changeAddress(oh.OrderNo, mNamn, mAdress1, mAdress2, mOrt);

            // Orderrad på patientens order
            string rownr = oOR.addNewRow(oh.OrderNo, "", "EA");
            mOr = new OrderRowDefinitions.OrderRow();
            mOr.OrderNo = oh.OrderNo;
            mOr.AidNr = "";
            mOr.Rad = rownr;
            mOr.Artikel = "EA";
            mOr.Antal = "0";
            //			mOr.AccountNo = oOR.getAccountOnAid(mOnr, mAidId);
            mOr.Beloppsrad = true;
            mOr.EA_ProductGroup = mProductGroup;


            try
            {
                mOr.APris = mAmount;
                oOR.saveOrderRow(mOr, false, false);
            }
            catch
            {
                MessageBox.Show(null, "Inget gilltigt belopp", "Fel inmatning (2)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            mFsNr = oOR.deliverOwnFeeRow(mOr.OrderNo, mOr.Rad, mOr.Antal, "", mFsNr);

            return mOr.OrderNo.PadRight(6) + " - " + mOr.Rad;
        }

        public OwnFeeResult creditRowOnPatientsOrder(string onr, string row)
        {
            OrderRowFunc oOR = new OrderRowFunc();
            OrderRowDefinitions.OrderRow or = oOR.getRow(onr, row);
            OwnFeeResult ofr = new OwnFeeResult();

            // Orderrad på patientens order
            string rownr = oOR.addNewRow(onr, "", "EA");
            or.OrderNo = onr;
            or.AidNr = "";
            or.Rad = rownr;
            or.Artikel = "EA";
            or.Antal = "0";
            or.Beloppsrad = true;

            try
            {
                or.APris = ECS.doubleToString(ECS.stringToDouble(or.APris) * -1, '.');
                oOR.saveOrderRow(or, false, false);
            }
            catch
            {
                MessageBox.Show(null, "Inget gilltigt belopp", "Fel inmatning (2)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (or.Levstatus.Equals("5"))
                ofr.FsNo = oOR.deliverOwnFeeRow(or.OrderNo, or.Rad, or.Antal, "", mFsNr);
            else
                ofr.FsNo = "";

            ofr.OrderNo = or.OrderNo;
            ofr.OrderRow = or.Rad;

            return ofr;
        }

        public string[] getAllProductGroups()
        {
            return product.getProductsGroup();
        }
    }
}
