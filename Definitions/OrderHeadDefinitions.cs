using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Reflection;
using Excido;

namespace Ortoped.Definitions
{
    public class OrderHeadDefinition
    {
        private string mOrderNo;
        private string mOrderType;
        private string mPaymentCondition;
        private string mOrderDate;
        private string mPatientsSSN;  // Social sequrity number
        private string mPatientNo;
        private string mDeliverCustomerName;
        private string mInvoiceCustomer;
        private string mInvoiceCustomerName;
        private string mClinik;
        private string mClinikName;
        private string[] mOrdinator;
        private string mSelOrdinator;
        private string mOrdination;
        private string mDiagnoseCode;
        private string mAidType;
        private string mDiagnose;
        private string mNotation;
        private string mOHtext;
        private DateTime mValidFrom;
        private int mValidYearsCount;	// Antal år
        private string mAidCount;		// Antal hjälpmedel
        private string mYourReference;
        private string mSignature;
        private string mPricelist;
        private DateTime mReferralDate;   // Datum som remiss anländer till OTA
        private string mRekvNo;			// Rekvisitionsnummer
        private string mKombikaCode;    // THORD: Kombikakod (klinik)
        private string mReferralNo;     // THORD: Remissnummer
        private string mPriority;       // THORD: Prioritet (0=NORMAL, 1=SUBAKUT, 2=AKUT)
        private bool misClosed;
        private string mDeliverStatus;
        private bool mCanChangeInvoiceCustomer;

        public OrderHeadDefinition()
        {
        }

        public OrderHeadDefinition(OrderHeadDefinition copyFrom)
        {
            // get all the fields in the class
            FieldInfo[] fields_of_class = this.GetType().GetFields(
              BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            // copy each value over to 'this'
            foreach (FieldInfo fi in fields_of_class)
            {
                fi.SetValue(this, fi.GetValue(copyFrom));
            }
        }

        public string OrderNo
        {
            get { return ECS.noNULL(mOrderNo); }
            set { mOrderNo = value; }
        }

        public string OrderType
        {
            get { return ECS.noNULL(mOrderType); }
            set { mOrderType = value; }
        }

        public string PaymentCondition
        {
            get { return ECS.noNULL(mPaymentCondition); }
            set { mPaymentCondition = value; }
        }

        public string OrderDate
        {
            get { return ECS.noNULL(mOrderDate); }
            set { mOrderDate = value; }
        }

        public string PatientsSSN
        {
            get { return ECS.noNULL(mPatientsSSN); }
            set { mPatientsSSN = value; }
        }

        public string PatientNo
        {
            get { return ECS.noNULL(mPatientNo); }
            set { mPatientNo = value; }
        }

        public string DeliverCustomerName
        {
            get { return ECS.noNULL(mDeliverCustomerName); }
            set { mDeliverCustomerName = value; }
        }

        public string InvoiceCustomer
        {
            get { return ECS.noNULL(mInvoiceCustomer); }
            set { mInvoiceCustomer = value; }
        }

        public string InvoiceCustomerName
        {
            get { return ECS.noNULL(mInvoiceCustomerName); }
            set { mInvoiceCustomerName = value; }
        }

        public string Clinik
        {
            get { return ECS.noNULL(mClinik); }
            set { mClinik = value; }
        }

        public string ClinikName
        {
            get { return ECS.noNULL(mClinikName); }
            set { mClinikName = value; }
        }

        public string[] Ordinator
        {
            get { return mOrdinator; }
            set { mOrdinator = value; }
        }

        public string SelOrdinator
        {
            get { return ECS.noNULL(mSelOrdinator); }
            set { mSelOrdinator = value; }
        }

        public string Ordination
        {
            get { return ECS.noNULL(mOrdination); }
            set { mOrdination = value; }
        }

        public string DiagnoseCode
        {
            get { return ECS.noNULL(mDiagnoseCode); }
            set { mDiagnoseCode = value; }
        }

        public string AidType
        {
            get { return ECS.noNULL(mAidType); }
            set { mAidType = value; }
        }

        public string Diagnose
        {
            get { return ECS.noNULL(mDiagnose); }
            set { mDiagnose = value; }
        }

        public string Notation
        {
            get { return ECS.noNULL(mNotation); }
            set { mNotation = value; }
        }

        public string OHtext
        {
            get { return ECS.noNULL(mOHtext); }
            set { mOHtext = value; }
        }

        public DateTime ValidFrom
        {
            get { return mValidFrom; }
            set { mValidFrom = value; }
        }

        public int ValidYearsCount
        {
            get { return mValidYearsCount; }
            set { mValidYearsCount = value; }
        }

        public string AidCount
        {
            get { return ECS.noNULL(mAidCount); }
            set { mAidCount = value; }
        }

        public string YourReference
        {
            get { return ECS.noNULL(mYourReference); }
            set { mYourReference = value; }
        }

        public string Signature
        {
            get { return ECS.noNULL(mSignature); }
            set { mSignature = value; }
        }

        public string Pricelist
        {
            get { return ECS.noNULL(mPricelist); }
            set { mPricelist = value; }
        }

        public string RekvNo
        {
            get { return ECS.noNULL(mRekvNo); }
            set { mRekvNo = value; }
        }

        public string KombikaCode
        {
            get { return ECS.noNULL(mKombikaCode); }
            set { mKombikaCode = value; }
        }

        public DateTime ReferralDate
        {
            get { return mReferralDate; }
            set { mReferralDate = value; }
        }

        public string ReferralNo
        {
            get { return ECS.noNULL(mReferralNo); }
            set { mReferralNo = value; }
        }

        public string Priority
        {
            get { return ECS.noNULL(mPriority); }
            set { mPriority = value; }
        }

        public bool isClosed
        {
            get { return misClosed; }
            set { misClosed = value; }
        }


        public string DeliverStatus
        {
            get { return ECS.noNULL(mDeliverStatus); }
            set { mDeliverStatus = value; }
        }

        public static ListViewItem[] convertToListView(OrderHeadDefinition[] o)
        {
            ArrayList al = new ArrayList();
            ListViewItem[] lw = new ListViewItem[o.Length];

            for (int i = 0; i < lw.Length; i++)
            {
                lw[i] = new ListViewItem(o[i].OrderNo);
                lw[i].SubItems.Add(o[i].ValidFrom.ToString("yyyy-MM-dd"));
                lw[i].SubItems.Add(o[i].ValidFrom.AddYears(o[i].ValidYearsCount).ToString("yyyy-MM-dd"));

                // Check if order is valid, wihtin valid date range
                if (DateTime.Today.CompareTo(o[i].ValidFrom) >= 0 && DateTime.Today.CompareTo(o[i].ValidFrom.AddYears(o[i].ValidYearsCount)) <= 0)
                {
                    lw[i].BackColor = System.Drawing.Color.LightGreen;
                }
                else
                {
                    lw[i].BackColor = System.Drawing.Color.White;
                }

                if (o[i].isClosed)
                {
                    lw[i].SubItems.Add("Stängd");
                    lw[i].BackColor = System.Drawing.Color.LightCoral;
                }
                else
                {
                    lw[i].SubItems.Add("");
                    //lw[i].BackColor = System.Drawing.Color.White;
                }
                lw[i].SubItems.Add(o[i].Priority);
                lw[i].SubItems.Add(o[i].RekvNo);
                lw[i].SubItems.Add(o[i].Ordination);
            }

            return lw;
        }

        public static ListViewItem[] convertToThordListView(OrderHeadDefinition[] o)
        {
            ArrayList al = new ArrayList();
            ListViewItem[] lw = new ListViewItem[o.Length];

            for (int i = 0; i < lw.Length; i++)
            {
                lw[i] = new ListViewItem(o[i].OrderNo);
                lw[i].SubItems.Add(o[i].PatientsSSN);
                lw[i].SubItems.Add(o[i].ValidFrom.ToString());
                lw[i].SubItems.Add(o[i].ValidYearsCount.ToString());
                if (o[i].isClosed)
                    lw[i].SubItems.Add("Stängd");
                else
                    lw[i].SubItems.Add("");
                lw[i].SubItems.Add(o[i].Priority);
                lw[i].SubItems.Add(o[i].RekvNo);
                lw[i].SubItems.Add(o[i].Ordination);
                lw[i].SubItems.Add(o[i].Priority + o[i].ValidFrom);
            }

            return lw;
        }

        public bool CanChangeInvoiceCustomer
        {
            get { return mCanChangeInvoiceCustomer; }
            set { mCanChangeInvoiceCustomer = value; }
        }
    }
}
