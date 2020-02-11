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
    public interface I_L_IMPACT
    {
        List<D_L_IMPACT> SelectList (F_L_IMPACT aFilter);
        void             DeleteList (F_L_IMPACT aFilter);

        D_L_IMPACT SelectItem (K_L_IMPACT aKey);
        D_L_IMPACT InsertItem (D_L_IMPACT aDto);
        D_L_IMPACT UpdateItem (D_L_IMPACT aDto);
        void       DeleteItem (K_L_IMPACT aKey);
    }

    /// <summary>
    /// filter object for instance lists
    /// </summary>
    public class F_L_IMPACT : Data_F_Base
    {
        public int?   focusID  { get; set; }
        public int?   effortID { get; set; }
        public string searchTxt { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_L_IMPACT() { }
    }

    /// <summary>
    /// key object for instance items
    /// </summary>
    public class K_L_IMPACT : Data_K_Base
    {
        public int? focusID  { get; set; }
        public int? effortID { get; set; }
    }

    /// <summary>
    /// data object for instance items
    /// </summary>
    public class D_L_IMPACT : Data_O_Base
    {
        // read-write
        public int    focusID { get; set; }
        public int    effortID { get; set; }
        public string titleTxt { get; set; }
        public string descTxt  { get; set; }

        // read-only
        public string focusTitleTxt  { get; set; }
        public string effortTitleTxt { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public D_L_IMPACT() : base() { }
    }
}
