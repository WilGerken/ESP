using System;
using System.Collections.Generic;
using CoreLib.Common;
using CoreLib.Resources;
using Library.Common;

namespace Library.Resources.Main
{
    /// <summary>
    /// public interface for instance items
    /// </summary>
    public interface I_L_FOCUS
    {
        List<D_L_FOCUS> SelectList (F_L_FOCUS aFilter);
        void            DeleteList (F_L_FOCUS aFilter);

        D_L_FOCUS SelectItem (K_L_FOCUS aKey);
        D_L_FOCUS InsertItem (D_L_FOCUS aDto);
        D_L_FOCUS UpdateItem (D_L_FOCUS aDto);
        void      DeleteItem (K_L_FOCUS aKey);
    }

    /// <summary>
    /// filter object for instance lists
    /// </summary>
    public class F_L_FOCUS : Data_F_Base
    {
        public string titleTxt  { get; set; }
        public int?   parentID  { get; set; }
        public string searchTxt { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_L_FOCUS() { }
    }

    /// <summary>
    /// key object for instance items
    /// </summary>
    public class K_L_FOCUS : Data_K_Base
    {
        public string titleTxt { get; set; }
    }

    /// <summary>
    /// data object for instance items
    /// </summary>
    public class D_L_FOCUS : Data_O_Base
    {
        // read-write
        public string        titleTxt { get; set; }
        public int?          parentID { get; set; }
        public string        descTxt  { get; set; }

        // read-only

        /// <summary>
        /// default constructor
        /// </summary>
        public D_L_FOCUS() : base() { }
    }
}
