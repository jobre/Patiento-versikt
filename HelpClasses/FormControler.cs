using System;
using System.Collections.Generic;
using System.Text;
using Ortoped.Definitions;

namespace Ortoped.HelpClasses
{
  public class FormControler
  {
    private static FormControler unique_instance;
    private OrderHeadDefinition currentOH;
    private PatientDefenition currentPatient, lastPatient;

    private FormControler()
    {
      currentOH = new OrderHeadDefinition();
      currentPatient = new PatientDefenition();
      lastPatient = new PatientDefenition();
    }

    public static FormControler getInstance()
    {
      if (unique_instance == null)
        unique_instance = new FormControler();

      return unique_instance;
    }

    public OrderHeadDefinition CurrentOH
    {
      get { return currentOH; }
      set { currentOH = value; }
    }

    public PatientDefenition CurrentPatient
    {
      get { return currentPatient; }
      set { currentPatient = value; }
    }

    public PatientDefenition LastPatient
    {
      get { return lastPatient; }
      set { lastPatient = value; }
    }

    /// <summary>
    /// Kontrollera om ordern är ändrad
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public bool isOrderHeadChanged(OrderHeadDefinition oCurrent, OrderHeadDefinition oNew)
    {
      bool isOrderChanged = false;

      try
      {
        if (!oCurrent.PatientNo.Trim().Equals(oNew.PatientNo.Trim()))
          isOrderChanged = true;

        if (!oCurrent.InvoiceCustomer.Trim().Equals(oNew.InvoiceCustomer.Trim()))
          isOrderChanged = true;

        if (!oCurrent.Clinik.Trim().Equals(oNew.Clinik.Trim()))
          isOrderChanged = true;

        if (!oCurrent.SelOrdinator.Trim().Equals(oNew.SelOrdinator.Trim()))
          isOrderChanged = true;

        if (!oCurrent.Ordination.Trim().Equals(oNew.Ordination.Trim()))
          isOrderChanged = true;

        if (!oCurrent.YourReference.Trim().Equals(oNew.YourReference.Trim()))
          isOrderChanged = true;

        if (!oCurrent.Diagnose.Trim().Equals(oNew.Diagnose.Trim()))
          isOrderChanged = true;

        if (!oCurrent.Notation.Trim().Equals(oNew.Notation.Trim()))
          isOrderChanged = true;

        if (!oCurrent.DiagnoseCode.Trim().Equals(oNew.DiagnoseCode.Trim()))
          isOrderChanged = true;

        if (oCurrent.ValidFrom.CompareTo(oNew.ValidFrom) != 0)
          isOrderChanged = true;

        if (oCurrent.ReferralDate.CompareTo(oNew.ReferralDate) != 0)
            isOrderChanged = true;

        if (oCurrent.ValidYearsCount.CompareTo(oNew.ValidYearsCount) != 0)
          isOrderChanged = true;

        if (!oCurrent.AidCount.Trim().Equals(oNew.AidCount.Trim()))
          isOrderChanged = true;

        if (!oCurrent.Signature.Trim().Equals(oNew.Signature.Trim()))// || oCurrent.Signature.Equals(""))
          isOrderChanged = true;

        if (!oCurrent.Pricelist.Trim().Equals(oNew.Pricelist.Trim()))
          isOrderChanged = true;
      }
      catch { }

      return isOrderChanged;
    }

  }
}
