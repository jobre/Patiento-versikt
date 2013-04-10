using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ortoped.HelpClasses
{
    class ListViewItemComparer : IComparer
    {
        //    private int col;
        //    private SortOrder OrderOfSort;
        //    private CaseInsensitiveComparer ObjectCompare;
        /// <summary>
        /// Specifies the column to be sorted
        /// </summary>
        private int ColumnToSort;
        /// <summary>
        /// Specifies the order in which to sort (i.e. 'Ascending').
        /// </summary>
        private SortOrder OrderOfSort;
        /// <summary>
        /// Case insensitive comparer object
        /// </summary>
        private CaseInsensitiveComparer ObjectCompare;

        public ListViewItemComparer()
        {
            // Initialize the column to '0'
            ColumnToSort = 0;

            // Initialize the sort order to 'none'
            OrderOfSort = SortOrder.None;

            // Initialize the CaseInsensitiveComparer object
            ObjectCompare = new CaseInsensitiveComparer();
        }

        public ListViewItemComparer(int column)
        {
            ColumnToSort = column;

            // Initialize the sort order to 'none'
            OrderOfSort = SortOrder.Descending;

            // Initialize the CaseInsensitiveComparer object
            ObjectCompare = new CaseInsensitiveComparer();

        }
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;

            // Cast the objects to be compared to ListViewItem objects
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            // Compare the two items
            compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);

            // Calculate correct return value based on object comparison
            if (OrderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }


            //int compareResult;
            //ListViewItem listviewX, listviewY;

            //// Cast the objects to be compared to ListViewItem objects
            //listviewX = (ListViewItem)x;
            //listviewY = (ListViewItem)y;

            //// Compare the two items
            //compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);

            //// Calculate correct return value based on object comparison
            //if (OrderOfSort == SortOrder.Ascending)
            //{
            //    // Ascending sort is selected, return normal result of compare operation
            //    return compareResult;
            //}
            //else if (OrderOfSort == SortOrder.Descending)
            //{
            //    // Descending sort is selected, return negative result of compare operation
            //    return (-compareResult);
            //}
            //else
            //{
            //    // Return '0' to indicate they are equal
            //    return 0;
            //}



            //int returnVal = -1;
            //try
            //{
            //  returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            //  returnVal *= -1; // Descending sort
            //}
            //catch{
            //}
            //return returnVal;
        }

        /// <summary>
        /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
        /// </summary>
        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
            }
            get
            {
                return ColumnToSort;
            }
        }

        /// <summary>
        /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
        /// </summary>
        public SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
            }
        }
    }
}
