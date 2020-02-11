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
    public class L_EFFORT_NOTE : DATA_ACCESS_BASE<D_L_EFFORT_NOTE, F_L_EFFORT_NOTE, K_L_EFFORT_NOTE>, I_L_EFFORT_NOTE
    {
        #region Test Data

        public enum ETestFocusID { None = 0, Self, Family, Home, Craft }

        // resource list
        public static List<D_L_EFFORT_NOTE> ResourceList = new List<D_L_EFFORT_NOTE>();

        static L_EFFORT_NOTE()
        {
        }

        #endregion

        /// <summary>
        /// select a list of items from persistent store for given filter
        /// </summary>
        /// <param name="aFilter"></param>
        /// <returns></returns>
        public List<D_L_EFFORT_NOTE> SelectList (F_L_EFFORT_NOTE aFilter)
        {
            IEnumerable<D_L_EFFORT_NOTE> lResult = from item       in ResourceList
                                              join effortItem in L_EFFORT.ResourceList on item.effortID equals effortItem.objectID
                                              select new D_L_EFFORT_NOTE
                                              {
                                                     effortID       = item.effortID,
                                                     effortTitleTxt = effortItem.titleTxt,
                                                     noteDts        = item.noteDts,
                                                     noteTxt        = item.noteTxt,
                                                     descTxt        = item.descTxt,

                                                     objectID    = item.objectID,
                                                     activeYn    = item.activeYn,
                                                     deletedYn   = item.deletedYn,
                                                     createByUid = item.createByUid,
                                                     createOnDts = item.createOnDts,
                                                     updateByUid = item.updateByUid,
                                                     updateOnDts = item.updateOnDts
                                                 };

            // apply filter attributes
            if (aFilter.effortID.HasValue)
            {
                lResult = lResult.Where (x => x.effortID == aFilter.effortID.Value);
            }

            if (! string.IsNullOrEmpty (aFilter.searchTxt))
            {
                lResult = lResult.Where (x => x.noteTxt.Contains (aFilter.searchTxt) || 
                                              x.descTxt.Contains (aFilter.searchTxt));
            }

            // check base criteria
            lResult = CheckBaseCriteria (lResult, aFilter);

            // return result
            return lResult.ToList<D_L_EFFORT_NOTE>();
        }

        /// <summary>
        /// remove all matching items from persistent store
        /// </summary>
        /// <param name="aFilter"></param>
        public void DeleteList (F_L_EFFORT_NOTE aFilter)
        {
            throw new NotImplementedException ("L_EFFORT_NOTE.DeleteList not implemented");
        }

        /// <summary>
        /// select an item from persistent store for given key
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public D_L_EFFORT_NOTE SelectItem (K_L_EFFORT_NOTE aKey)
        {
            D_L_EFFORT_NOTE lResult = null;

            // apply key attributes
            if (aKey.objectID.HasValue)
            {
                F_L_EFFORT_NOTE lFilter = new F_L_EFFORT_NOTE() { objectID = aKey.objectID.Value };

                lResult = SelectList(lFilter).FirstOrDefault();
            }

            // throw exception if not found
            if (lResult == null)
                throw new DllNotFoundException (string.Format ("L_EFFORT_NOTE Item not found for key {0}", aKey.objectID));

            // return result
            return lResult;
        }

        /// <summary>
        /// insert an item into persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_L_EFFORT_NOTE InsertItem (D_L_EFFORT_NOTE aDto)
        {
            int lID = 0;

            if (ResourceList.Count > 0)
                lID = ResourceList.Select(x => x.objectID).Max() + 1;

            // create new item
            D_L_EFFORT_NOTE lItem = new D_L_EFFORT_NOTE
            {
                // attribute fields
                effortID = aDto.effortID,
                noteDts  = aDto.noteDts,
                noteTxt  = aDto.noteTxt,
                descTxt  = aDto.descTxt,

                // TODO: related fields

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
        public D_L_EFFORT_NOTE UpdateItem (D_L_EFFORT_NOTE aDto)
        {
            // fetch indicated item
            D_L_EFFORT_NOTE lItem = ResourceList.Where (x => x.objectID == aDto.objectID).FirstOrDefault();

            // update item
            lock (lItem)
            {
                // attribute fields
                lItem.effortID = aDto.effortID;
                lItem.noteDts  = aDto.noteDts;
                lItem.noteTxt  = aDto.noteTxt;
                lItem.descTxt  = aDto.descTxt;

                // TODO: related fields

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
        public void DeleteItem (K_L_EFFORT_NOTE aKey)
        {
            // fetch indicated item
            D_L_EFFORT_NOTE lItem = ResourceList.Where (x => x.objectID == aKey.objectID).FirstOrDefault();

            // delete item from list
            lock (ResourceList)
            {
                ResourceList.Remove(lItem);
            }
        }
    }
}