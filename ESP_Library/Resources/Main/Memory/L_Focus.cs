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
    public class L_FOCUS : DATA_ACCESS_BASE<D_L_FOCUS, F_L_FOCUS, K_L_FOCUS>, I_L_FOCUS
    {
        #region Test Data

        public enum ETestFocusID { None = 0, Self, Family, Home, Craft }

        // resource list
        public static List<D_L_FOCUS> ResourceList = new List<D_L_FOCUS>();

        static L_FOCUS()
        {
            ResourceList.Add (new D_L_FOCUS
            {
                objectID    = (int)ETestFocusID.Self, 
                titleTxt    = "Self",
                descTxt     = string.Format ("Indv Entity {0}", ETestFocusID.Self),
                createByUid = LibRef.AdminUid, createOnDts = DateTime.Now,
                updateByUid = LibRef.AdminUid, updateOnDts = DateTime.Now
            });
            ResourceList.Add (new D_L_FOCUS
            {
                objectID    = (int)ETestFocusID.Family,
                titleTxt = "Family",
                descTxt     = string.Format ("Indv Entity {0}", ETestFocusID.Family),
                createByUid = LibRef.AdminUid, createOnDts = DateTime.Now,
                updateByUid = LibRef.AdminUid, updateOnDts = DateTime.Now
            });
            ResourceList.Add (new D_L_FOCUS
            {
                objectID    = (int) ETestFocusID.Home,
                titleTxt = "Home",
                descTxt     = string.Format ("Indv Entity {0}", ETestFocusID.Home),
                createByUid = LibRef.AdminUid, createOnDts = DateTime.Now,
                updateByUid = LibRef.AdminUid, updateOnDts = DateTime.Now
            });
            ResourceList.Add (new D_L_FOCUS
            {
                objectID    = (int) ETestFocusID.Craft,
                titleTxt = "Craft",
                descTxt     = string.Format ("Indv Entity {0}", ETestFocusID.Craft),
                createByUid = LibRef.AdminUid, createOnDts = DateTime.Now,
                updateByUid = LibRef.AdminUid, updateOnDts = DateTime.Now
            });
        }

        #endregion

        /// <summary>
        /// select a list of items from persistent store for given filter
        /// </summary>
        /// <param name="aFilter"></param>
        /// <returns></returns>
        public List<D_L_FOCUS> SelectList (F_L_FOCUS aFilter)
        {
            IEnumerable<D_L_FOCUS> lResult = from item in ResourceList
                                              select new D_L_FOCUS
                                              {
                                                     titleTxt   = item.titleTxt,
                                                     parentID   = item.parentID,
                                                     descTxt    = item.descTxt,

                                                     objectID    = item.objectID,
                                                     activeYn    = item.activeYn,
                                                     deletedYn   = item.deletedYn,
                                                     createByUid = item.createByUid,
                                                     createOnDts = item.createOnDts,
                                                     updateByUid = item.updateByUid,
                                                     updateOnDts = item.updateOnDts
                                                 };

            // apply filter attributes
            if (aFilter.parentID.HasValue)
            {
                lResult = lResult.Where (x => x.parentID == aFilter.parentID.Value);
            }

            if (! string.IsNullOrEmpty (aFilter.searchTxt))
            {
                lResult = lResult.Where (x => x.titleTxt.Contains (aFilter.searchTxt));
            }

            // check base criteria
            lResult = CheckBaseCriteria (lResult, aFilter);

            // return result
            return lResult.ToList<D_L_FOCUS>();
        }

        /// <summary>
        /// remove all matching items from persistent store
        /// </summary>
        /// <param name="aFilter"></param>
        public void DeleteList (F_L_FOCUS aFilter)
        {
            throw new NotImplementedException ("L_FOCUS.DeleteList not implemented");
        }

        /// <summary>
        /// select an item from persistent store for given key
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public D_L_FOCUS SelectItem (K_L_FOCUS aKey)
        {
            D_L_FOCUS lResult = null;

            // apply key attributes
            if (aKey.objectID.HasValue)
            {
                F_L_FOCUS lFilter = new F_L_FOCUS() { objectID = aKey.objectID.Value };

                lResult = SelectList(lFilter).FirstOrDefault();
            }

            // throw exception if not found
            if (lResult == null)
                throw new DllNotFoundException (string.Format ("L_FOCUS Item not found for key {0}", aKey.objectID));

            // return result
            return lResult;
        }

        /// <summary>
        /// insert an item into persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_L_FOCUS InsertItem (D_L_FOCUS aDto)
        {
            int lID = 0;

            if (ResourceList.Count > 0)
                lID = ResourceList.Select(x => x.objectID).Max() + 1;

            // create new item
            D_L_FOCUS lItem = new D_L_FOCUS
            {
                // attribute fields
                titleTxt      = aDto.titleTxt,
                parentID      = aDto.parentID,
                descTxt       = aDto.descTxt,

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
        public D_L_FOCUS UpdateItem (D_L_FOCUS aDto)
        {
            // fetch indicated item
            D_L_FOCUS lItem = ResourceList.Where (x => x.objectID == aDto.objectID).FirstOrDefault();

            // update item
            lock (lItem)
            {
                // attribute fields
                lItem.titleTxt      = aDto.titleTxt;
                lItem.parentID      = aDto.parentID;
                lItem.descTxt       = aDto.descTxt;

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
        public void DeleteItem (K_L_FOCUS aKey)
        {
            // fetch indicated item
            D_L_FOCUS lItem = ResourceList.Where (x => x.objectID == aKey.objectID).FirstOrDefault();

            // delete item from list
            lock (ResourceList)
            {
                ResourceList.Remove(lItem);
            }
        }
    }
}