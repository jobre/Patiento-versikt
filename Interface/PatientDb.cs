using System;
using System.Collections.Generic;
using System.Text;
using Ortoped.Definitions;

namespace Ortoped.Interface
{
  interface PatientDb
  {
    PatientDefenition[] getPatientByPnr(string pnr);
    PatientDefenition getPatientById(string id);
    bool addPatient(PatientDefenition patient);
    bool updatePatient(PatientDefenition patient);
    string test { get; set;} // Exempel Property

  }
}
