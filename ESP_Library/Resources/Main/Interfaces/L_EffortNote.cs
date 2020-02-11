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
    public interface I_L_EFFORT_NOTE
    {
        List<D_L_EFFORT_NOTE> SelectList (F_L_EFFORT_NOTE aFilter);
        void                  DeleteList (F_L_EFFORT_NOTE aFilter);

        D_L_EFFORT_NOTE SelectItem (K_L_EFFORT_NOTE aKey);
        D_L_EFFORT_NOTE InsertItem (D_L_EFFORT_NOTE aDto);
        D_L_EFFORT_NOTE UpdateItem (D_L_EFFORT_NOTE aDto);
        void            DeleteItem (K_L_EFFORT_NOTE aKey);
    }

    /// <summary>
    /// filter object for instance lists
    /// </summary>
    public class F_L_EFFORT_NOTE : Data_F_Base
    {
        public int?   effortID { get; set; }
        public string searchTxt { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_L_EFFORT_NOTE() { }
    }

    /// <summary>
    /// key object for instance items
    /// </summary>
    public class K_L_EFFORT_NOTE : Data_K_Base
    {
        public int?      effortID { get; set; }
        public DateTime? noteDts { get; set; }
    }

    /// <summary>
    /// data object for instance items
    /// </summary>
    public class D_L_EFFORT_NOTE : Data_O_Base
    {
        // read-write
        public int      effortID { get; set; }
        public DateTime noteDts { get; set; }
        public string   noteTxt { get; set; }
        public string   descTxt  { get; set; }

        // read-only
        public string effortTitleTxt { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public D_L_EFFORT_NOTE() : base() { }
    }
}
