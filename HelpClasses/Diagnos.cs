using System;
using Ortoped;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using GCS;

namespace Ortoped.HelpClasses
{
    /// <summary>
    /// Summary description for Diagnos.
    /// </summary>
    public class Diagnos
    {
        private Hashtable hsDiagList = new Hashtable();

        public struct Diag
        {
            public string ID;
            public string TXT;
        }

        public ListViewItem[] getDiagnosList()
        {
            if (hsDiagList.Count <= 0)
                loadDiagnosFromFile();

            ArrayList al = new ArrayList();
            ListViewItem[] lw = new ListViewItem[hsDiagList.Count];
            int i = 0;

            IDictionaryEnumerator myEnum = hsDiagList.GetEnumerator();

            while (myEnum.MoveNext())
            {
                lw[i] = new ListViewItem(myEnum.Key.ToString());
                lw[i].SubItems.Add(myEnum.Value.ToString());
                i++;
            }

            return lw;
        }

        public Diagnos()
        {
            loadDiagnosFromFile();
        }

        private void loadDiagnosFromFile()
        {
            try
            {
                if (!GCF.noNULL(Config.DiagnosPath).Equals(""))
                {
                    StreamReader sr = new StreamReader(Config.DiagnosPath);
                    string[] s;

                    while (sr.Peek() != -1)
                    {
                        s = sr.ReadLine().Split(';');
                        if (!hsDiagList.ContainsKey(s[0]))
                            hsDiagList.Add(s[0], s[1]);
                    }
                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                Log4Net.Logger.loggError(ex, "Error while loading diagnose code from file: " + Config.DiagnosPath, "GCS", "Diagnos.loadDiagnosFromFile");
            }
        }

        public string getDiagnosById(string id)
        {
            if (hsDiagList.ContainsKey(id))
                return hsDiagList[id].ToString();
            else
                return "";
        }

        /// <summary>
        /// Finns diagnoskoden?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ContainsId(string id)
        {
            return hsDiagList.Contains(id);
        }

    }
}
