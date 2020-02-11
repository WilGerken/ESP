using System;
using System.Collections.Generic;
using CoreLib.Common;
using Library.Common;

namespace Library.Resources.Main
{
    /// <summary>
    /// public interface for instance items
    /// </summary>
    public interface I_L_EFFORT_TYPE
    {
        List<D_L_EFFORT_TYPE> SelectList (F_L_EFFORT_TYPE aFilter);
        void                  DeleteList (F_L_EFFORT_TYPE aFilter);

        D_L_EFFORT_TYPE SelectItem (K_L_EFFORT_TYPE aKey);
        D_L_EFFORT_TYPE InsertItem (D_L_EFFORT_TYPE aDto);
        D_L_EFFORT_TYPE UpdateItem (D_L_EFFORT_TYPE aDto);
        void            DeleteItem (K_L_EFFORT_TYPE aKey);
    }

    /// <summary>
    /// filter object for instance lists
    /// </summary>
    public class F_L_EFFORT_TYPE : Data_F_Base
    {
        public string typeCd { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_L_EFFORT_TYPE () { }
    }

    /// <summary>
    /// key object for instance items
    /// </summary>
    public class K_L_EFFORT_TYPE : Data_K_Base
    {
        public string typeCd { get; set; }
    }

    /// <summary>
    /// data object for instance items
    /// </summary>
    public class D_L_EFFORT_TYPE : Data_O_Base
    {
        // read-write
        public string typeCd  { get; set; }
        public string typeTxt { get; set; }
        public string descTxt { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public D_L_EFFORT_TYPE () : base() { }
    }
}