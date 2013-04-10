using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Reflection;
using Excido;

namespace Ortoped.Definitions
{
    public class PatientDefenition
    {
        private string mPatientNo;
        private bool mPaymentReminder;
        private bool mInterestInvoice;
        private string mViewCode;
        private bool mDoesExist;
        private bool mCopDok;
        private bool mJournal;
        private string mRemark;
        private double mOpenBalance;
        private string mPriceList;
        private bool mDeceased;
        private string mVATCode;
        private string mCategory;
        private string mDeliverTerms;
        private string mPaymentTerms;
        private string mTelMobile;
        private string mTelSMS;
        private string mTelWork;
        private string mTelHome;
        private string mPoCity;
        private string mSSN;
        private string mSureName;
        private string mLastName;
        private string mAddress;
        private bool mIsValid;

        public PatientDefenition()
        {
        }

        public PatientDefenition(PatientDefenition copyFrom)
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

        public string PatientNo
        {
            get { return ECS.noNULL(mPatientNo); }
            set { mPatientNo = value; }
        }

        public string SSN
        {
            get { return ECS.noNULL(mSSN); }
            set { mSSN = value; }
        }

        public string SureName
        {
            get { return ECS.noNULL(mSureName); }
            set { mSureName = value; }
        }

        public string LastName
        {
            get { return ECS.noNULL(mLastName); }
            set { mLastName = value; }
        }

        public string Address
        {
            get { return ECS.noNULL(mAddress); }
            set { mAddress = value; }
        }

        public string PoCity
        {
            get { return ECS.noNULL(mPoCity); }
            set { mPoCity = value; }
        }

        public string TelHome
        {
            get { return ECS.noNULL(mTelHome); }
            set { mTelHome = value; }
        }

        public string TelWork
        {
            get { return ECS.noNULL(mTelWork); }
            set { mTelWork = value; }
        }

        public string TelMobile
        {
            get { return ECS.noNULL(mTelMobile); }
            set { mTelMobile = value; }
        }

        public string TelSMS
        {
            get { return ECS.noNULL(mTelSMS); }
            set { mTelSMS = value; }
        }

        public string PaymentTerms
        {
            get { return ECS.noNULL(mPaymentTerms); }
            set { mPaymentTerms = value; }
        }

        public string DeliverTerms
        {
            get { return ECS.noNULL(mDeliverTerms); }
            set { mDeliverTerms = value; }
        }
        private string mWayOfDeliver;

        public string WayOfDeliver
        {
            get { return ECS.noNULL(mWayOfDeliver); }
            set { mWayOfDeliver = value; }
        }

        public string Category
        {
            get { return ECS.noNULL(mCategory); }
            set { mCategory = value; }
        }

        public string VATCode
        {
            get { return ECS.noNULL(mVATCode); }
            set { mVATCode = value; }
        }

        public bool Deceased
        {
            get { return mDeceased; }
            set { mDeceased = value; }
        }

        public string PriceList
        {
            get { return ECS.noNULL(mPriceList); }
            set { mPriceList = value; }
        }

        public double OpenBalance
        {
            get { return mOpenBalance; }
            set { mOpenBalance = value; }
        }

        public string Remark
        {
            get { return ECS.noNULL(mRemark); }
            set { mRemark = value; }
        }

        public bool Journal
        {
            get { return mJournal; }
            set { mJournal = value; }
        }

        public bool CopDok
        {
            get { return mCopDok; }
            set { mCopDok = value; }
        }

        public bool DoesExist
        {
            get { return mDoesExist; }
            set { mDoesExist = value; }
        }

        public string ViewCode
        {
            get { return ECS.noNULL(mViewCode); }
            set { mViewCode = value; }
        }

        public bool InterestInvoice
        {
            get { return mInterestInvoice; }
            set { mInterestInvoice = value; }
        }

        public bool PaymentReminder
        {
            get { return mPaymentReminder; }
            set { mPaymentReminder = value; }
        }

        public bool IsValid
        {
            get { return mIsValid; }
            set { mIsValid = value; }
        }

        public static ListViewItem[] convertToPatient(PatientDefenition[] p)
        {
            ListViewItem[] lw = new ListViewItem[p.Length];

            for (int i = 0; i < lw.Length; i++)
            {
                lw[i] = new ListViewItem(p[i].SSN);
                lw[i].SubItems.Add(p[i].LastName);
                lw[i].SubItems.Add(p[i].SureName);
                lw[i].SubItems.Add(p[i].PoCity);
            }

            return lw;
        }

        /// <summary>
        /// Konvertera en ArrayList till Patient object.
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static PatientDefenition[] convertFromArray(ArrayList arr)
        {
            PatientDefenition[] p = new PatientDefenition[arr.Count];

            for (int i = 0; i < arr.Count; i++)
            {
                p[i] = (PatientDefenition)arr[i];
            }
            return p;
        }
    }
}
