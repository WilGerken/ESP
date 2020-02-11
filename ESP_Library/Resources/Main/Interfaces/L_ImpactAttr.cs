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
    public interface I_L_IMPACT_ATTR
    {
        List<D_L_IMPACT_ATTR> SelectList (F_L_IMPACT_ATTR aFilter);
        void                  DeleteList (F_L_IMPACT_ATTR aFilter);

        D_L_IMPACT_ATTR SelectItem (K_L_IMPACT_ATTR aKey);
        D_L_IMPACT_ATTR InsertItem (D_L_IMPACT_ATTR aDto);
        D_L_IMPACT_ATTR UpdateItem (D_L_IMPACT_ATTR aDto);
        void            DeleteItem (K_L_IMPACT_ATTR aKey);
    }

    /// <summary>
    /// filter object for instance lists
    /// </summary>
    public class F_L_IMPACT_ATTR : Data_F_Base
    {
        public int?   impactID    { get; set; }
        public int?   effortID    { get; set; }
        public int?   focusID     { get; set; }
        public int?   focusAttrID { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_L_IMPACT_ATTR() { }
    }

    /// <summary>
    /// key object for instance items
    /// </summary>
    public class K_L_IMPACT_ATTR : Data_K_Base
    {
        public int? impactID    { get; set; }
        public int? focusAttrID { get; set; }
    }

    /// <summary>
    /// data object for instance items
    /// </summary>
    public class D_L_IMPACT_ATTR : Data_O_Base
    {
        // read-write
        public int    impactID    { get; set; }
        public int    focusID     { get; set; }
        public int    effortID    { get; set; }
        public int    focusAttrID { get; set; }
        public int    attrID      { get; set; }
        public string impactAmt   { get; set; }
        public string descTxt     { get; set; }

        // read-only
        public string focusTitleTxt  { get; set; }
        public string effortTitleTxt { get; set; }
        public string attrTitleTxt   { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public D_L_IMPACT_ATTR() : base() { }
    }
}
