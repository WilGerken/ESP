using System;
using System.Collections.Generic;
using CoreLib.Common;
using Library.Common;

namespace Library.Resources.Main
{
    /// <summary>
    /// public interface for instance items
    /// </summary>
    public interface I_L_EFFORT
    {
        List<D_L_EFFORT> SelectList (F_L_EFFORT aFilter);
        void             DeleteList (F_L_EFFORT aFilter);

        D_L_EFFORT SelectItem (K_L_EFFORT aKey);
        D_L_EFFORT InsertItem (D_L_EFFORT aDto);
        D_L_EFFORT UpdateItem (D_L_EFFORT aDto);
        void       DeleteItem (K_L_EFFORT aKey);
    }

    /// <summary>
    /// filter object for instance lists
    /// </summary>
    public class F_L_EFFORT : Data_F_Base
    {
        public string titleTxt  { get; set; }
        public string searchTxt { get; set; }
        public int?   parentID  { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_L_EFFORT() { }
    }

    /// <summary>
    /// key object for instance items
    /// </summary>
    public class K_L_EFFORT : Data_K_Base
    {
        public string titleTxt { get; set; }
    }

    /// <summary>
    /// data object for instance items
    /// </summary>
    public class D_L_EFFORT : Data_O_Base
    {
        // read-write
        public string      titleTxt     { get; set; }
        public EEffortType effortTypeID { get; set; }
        public int?        parentID     { get; set; }
        public string      descTxt      { get; set; }

        // read-only
        public string effortTypeTxt { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public D_L_EFFORT() : base() { }
    }
}
