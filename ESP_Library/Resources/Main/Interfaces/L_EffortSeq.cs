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
    public interface I_L_EFFORT_SEQ
    {
        List<D_L_EFFORT_SEQ> SelectList (F_L_EFFORT_SEQ aFilter);
        void                 DeleteList (F_L_EFFORT_SEQ aFilter);

        D_L_EFFORT_SEQ SelectItem (K_L_EFFORT_SEQ aKey);
        D_L_EFFORT_SEQ InsertItem (D_L_EFFORT_SEQ aDto);
        D_L_EFFORT_SEQ UpdateItem (D_L_EFFORT_SEQ aDto);
        void           DeleteItem (K_L_EFFORT_SEQ aKey);
    }

    /// <summary>
    /// filter object for instance lists
    /// </summary>
    public class F_L_EFFORT_SEQ : Data_F_Base
    {
        public int?   sourceEffortID { get; set; }
        public int?   targetEffortID { get; set; }
        public string searchTxt      { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_L_EFFORT_SEQ () { }
    }

    /// <summary>
    /// key object for instance items
    /// </summary>
    public class K_L_EFFORT_SEQ : Data_K_Base
    {
        public int? sourceEffortID { get; set; }
        public int? targetEffortID { get; set; }
    }

    /// <summary>
    /// data object for instance items
    /// </summary>
    public class D_L_EFFORT_SEQ : Data_O_Base
    {
        // read-write
        public int?   sourceEffortID { get; set; }
        public int?   targetEffortID { get; set; }
        public int    overlapPct     { get; set; }
        public string noteTxt        { get; set; }
        public string descTxt        { get; set; }

        // read-only
        public string sourceTitleTxt { get; set; }
        public string targetTitleTxt { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public D_L_EFFORT_SEQ () : base() { }
    }
}
