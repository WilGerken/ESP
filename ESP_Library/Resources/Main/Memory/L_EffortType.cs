using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CoreLib.Common;
using Library.Common;

namespace Library.Resources.Main.Memory
{
    /// <summary>
    /// data access class
    /// </summary>
    public class L_EFFORT_TYPE : DATA_ACCESS_BASE<D_L_EFFORT_TYPE, F_L_EFFORT_TYPE, K_L_EFFORT_TYPE>, I_L_EFFORT_TYPE
    {
        #region Test Data

        // resource list
        public static List<D_L_EFFORT_TYPE> ResourceList = new List<D_L_EFFORT_TYPE>();

        static L_EFFORT_TYPE()
        {
            ResourceList.Add (new D_L_EFFORT_TYPE { objectID = (int) EEffortType.Project,   typeCd = "PROJECT",   typeTxt = "Project",
                createByUid = LibRef.AdminUid, updateByUid = LibRef.AdminUid });
            ResourceList.Add (new D_L_EFFORT_TYPE { objectID = (int) EEffortType.Operation, typeCd = "OPERATION", typeTxt = "Operation",
                createByUid = LibRef.AdminUid, updateByUid = LibRef.AdminUid });
            ResourceList.Add (new D_L_EFFORT_TYPE { objectID = (int) EEffortType.Task,      typeCd = "TASK",      typeTxt = "Task",
                createByUid = LibRef.AdminUid, updateByUid = LibRef.AdminUid });
        }

        #endregion

        /// <summary>
        /// select a list of items from persistent store for given filter
        /// </summary>
        /// <param name="aFilter"></param>
        /// <returns></returns>
        public List<D_L_EFFORT_TYPE> SelectList (F_L_EFFORT_TYPE aFilter)
        {
            IEnumerable<D_L_EFFORT_TYPE> lResult = ResourceList;

            // apply filter attributes
            if (aFilter.objectID.HasValue)
            {
                lResult = lResult.Where (x => x.objectID == aFilter.objectID.Value);
            }

            if (! string.IsNullOrEmpty (aFilter.typeCd))
            {
                lResult = lResult.Where (x => x.typeCd.Contains (aFilter.typeCd));
            }

            // check base criteria
            lResult = CheckBaseCriteria (lResult, aFilter);

            // return result
            return lResult.ToList<D_L_EFFORT_TYPE>();
        }

        /// <summary>
        /// remove all matching items from persistent store
        /// </summary>
        /// <param name="aFilter"></param>
        public void DeleteList (F_L_EFFORT_TYPE aFilter)
        {
            throw new NotImplementedException ("L_EFFORT_TYPE.DeleteList not implemented");
        }

        /// <summary>
        /// select an item from persistent store for given key
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public D_L_EFFORT_TYPE SelectItem (K_L_EFFORT_TYPE aKey)
        {
            D_L_EFFORT_TYPE lResult = null;

            // apply key attributes
            if (aKey.objectID.HasValue)
            {
                F_L_EFFORT_TYPE lFilter = new F_L_EFFORT_TYPE() { objectID = aKey.objectID.Value };

                lResult = SelectList(lFilter).FirstOrDefault();
            }

            // throw exception if not found
            if (lResult == null)
                throw new DllNotFoundException (string.Format ("L_EFFORT_TYPE Item not found for key {0}", aKey.objectID));

            // return result
            return lResult;
        }

        /// <summary>
        /// insert an item into persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_L_EFFORT_TYPE InsertItem (D_L_EFFORT_TYPE aDto)
        {
            int lID = 0;

            if (ResourceList.Count > 0)
                lID = ResourceList.Select (x => x.objectID).Max() + 1;

            // create new item
            D_L_EFFORT_TYPE lItem = new D_L_EFFORT_TYPE
            {
                typeCd   = aDto.typeCd,
                typeTxt  = aDto.typeTxt,
                descTxt  = aDto.descTxt,

                // meta fields
                objectID    = lID,
                activeYn    = aDto.activeYn,
                deletedYn   = aDto.deletedYn,
                createByUid = aDto.createByUid,
                createOnDts = aDto.createOnDts,
                updateByUid = aDto.updateByUid,
                updateOnDts = aDto.updateOnDts,
            };

            // insert new item into list
            lock (ResourceList)
            {
                ResourceList.Add (lItem);
            }

            return aDto;
        }

        /// <summary>
        /// update an item in persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_L_EFFORT_TYPE UpdateItem (D_L_EFFORT_TYPE aDto)
        {
            // fetch indicated item
            D_L_EFFORT_TYPE lItem = ResourceList.Where (x => x.objectID == aDto.objectID).FirstOrDefault();

            // update item
            lock (lItem)
            {
                lItem.typeCd  = aDto.typeCd;
                lItem.typeTxt = aDto.typeTxt;
                lItem.descTxt = aDto.descTxt;

                // meta fields
                lItem.activeYn    = aDto.activeYn;
                lItem.deletedYn   = aDto.deletedYn;
                lItem.createByUid = aDto.createByUid;
                lItem.createOnDts = aDto.createOnDts;
                lItem.updateByUid = aDto.updateByUid;
                lItem.updateOnDts = aDto.updateOnDts;
            }

            return aDto;
        }

        /// <summary>
        /// remove an item from persistent store
        /// </summary>
        /// <param name="aKey"></param>
        public void DeleteItem(K_L_EFFORT_TYPE aKey)
        {
            // fetch indicated item
            D_L_EFFORT_TYPE lItem = ResourceList.Where (x => x.objectID == aKey.objectID).FirstOrDefault();

            // delete item from list
            lock (ResourceList)
            {
                ResourceList.Remove (lItem);
            }
        }
    }
}