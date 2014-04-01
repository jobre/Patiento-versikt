using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Excido;
using Ortoped.Thord;

namespace Thord.HelpClasses
{
    static class ConvertFunctions
    {
        public static System.Windows.Forms.ListViewItem[] convertReferralToListViewItems(Referral[] reflist)
        {
            ArrayList al = new ArrayList();
            ListViewItem lw;

            foreach (Referral rfl in reflist)
            {
                lw = new ListViewItem(rfl.datum.ToString("yyMMdd"));
                lw.SubItems.Add(rfl.remissid);
                lw.SubItems.Add(rfl.personnr);
                lw.SubItems.Add(rfl.kombikakod);
                if (!ECS.noNULL(rfl.prioritering).Equals("NORMAL"))
                    lw.SubItems.Add(rfl.prioritering);
                else
                    lw.SubItems.Add("");
                lw.Checked = true;
                al.Add(lw);
            }
            return (ListViewItem[])al.ToArray(typeof(ListViewItem));
        }

    }
}
