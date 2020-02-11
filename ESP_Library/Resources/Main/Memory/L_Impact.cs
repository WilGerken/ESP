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
    public class L_IMPACT : DATA_ACCESS_BASE<D_L_IMPACT, F_L_IMPACT, K_L_IMPACT>, I_L_IMPACT
    {
        #region Test Data

        // resource list
        public static List<D_L_IMPACT> ResourceList = new List<D_L_IMPACT>();

        static L_IMPACT()
        {
            int lID = 1;

            ResourceList.Add (new D_L_IMPACT
            {
                objectID    = lID++, titleTxt    = "Meditation",
                focusID = (int) L_FOCUS.ETestFocusID.Self, effortID = (int) L_EFFORT.ETestEffortID.GrowSelf + 1,
                descTxt     = string.Format ("Impact for focus {0}, effort {1}", L_FOCUS.ETestFocusID.Self, L_EFFORT.ETestEffortID.GrowSelf + 1),
                createByUid = LibRef.AdminUid, createOnDts = DateTime.Now,
                updateByUid = LibRef.AdminUid, updateOnDts = DateTime.Now
            });
            ResourceList.Add (new D_L_IMPACT
            {
                objectID    = lID++, titleTxt = "Romance",
                focusID = (int) L_FOCUS.ETestFocusID.Self, effortID = (int) L_EFFORT.ETestEffortID.GrowFamily + 1,
                descTxt = string.Format("Impact for focus {0}, effort {1}", L_FOCUS.ETestFocusID.Family, L_EFFORT.ETestEffortID.GrowFamily + 1),
                createByUid = LibRef.AdminUid, createOnDts = DateTime.Now,
                updateByUid = LibRef.AdminUid, updateOnDts = DateTime.Now
            });
            ResourceList.Add (new D_L_IMPACT
            {
                objectID    = lID++, titleTxt = "Pay Attention",
                focusID = (int) L_FOCUS.ETestFocusID.Self, effortID = (int) L_EFFORT.ETestEffortID.GrowFamily + 2,
                descTxt = string.Format("Impact for focus {0}, effort {1}", L_FOCUS.ETestFocusID.Family, L_EFFORT.ETestEffortID.GrowFamily + 2),
                createByUid = LibRef.AdminUid, createOnDts = DateTime.Now,
                updateByUid = LibRef.AdminUid, updateOnDts = DateTime.Now
            });
            ResourceList.Add (new D_L_IMPACT
            {
                objectID    = lID++, titleTxt = "Mowing",
                focusID = (int) L_FOCUS.ETestFocusID.Self, effortID = (int) L_EFFORT.ETestEffortID.GrowHome + 1,
                descTxt = string.Format("Impact for focus {0}, effort {1}", L_FOCUS.ETestFocusID.Home, L_EFFORT.ETestEffortID.GrowHome + 1),
                createByUid = LibRef.AdminUid, createOnDts = DateTime.Now,
                updateByUid = LibRef.AdminUid, updateOnDts = DateTime.Now
            });
            ResourceList.Add (new D_L_IMPACT
            {
                objectID    = lID++, titleTxt = "Cleaning",
                focusID = (int) L_FOCUS.ETestFocusID.Self, effortID = (int) L_EFFORT.ETestEffortID.GrowHome + 2,
                descTxt = string.Format("Impact for focus {0}, effort {1}", L_FOCUS.ETestFocusID.Home, L_EFFORT.ETestEffortID.GrowHome + 2),
                createByUid = LibRef.AdminUid, createOnDts = DateTime.Now,
                updateByUid = LibRef.AdminUid, updateOnDts = DateTime.Now
            });
            ResourceList.Add (new D_L_IMPACT
            {
                objectID    = lID++, titleTxt = "Project 1",
                focusID = (int) L_FOCUS.ETestFocusID.Self, effortID = (int) L_EFFORT.ETestEffortID.GrowCraft + 1,
                descTxt = string.Format("Impact for focus {0}, effort {1}", L_FOCUS.ETestFocusID.Craft, L_EFFORT.ETestEffortID.GrowCraft + 1),
                createByUid = LibRef.AdminUid, createOnDts = DateTime.Now,
                updateByUid = LibRef.AdminUid, updateOnDts = DateTime.Now
            });
            ResourceList.Add(new D_L_IMPACT
            {
                objectID = lID++, titleTxt = "Project 2",
                focusID = (int) L_FOCUS.ETestFocusID.Self, effortID = (int) L_EFFORT.ETestEffortID.GrowCraft + 2,
                descTxt = string.Format("Impact for focus {0}, effort {1}", L_FOCUS.ETestFocusID.Craft, L_EFFORT.ETestEffortID.GrowCraft + 2),
                createByUid = LibRef.AdminUid, createOnDts = DateTime.Now,
                updateByUid = LibRef.AdminUid, updateOnDts = DateTime.Now
            });
            ResourceList.Add(new D_L_IMPACT
            {
                objectID = lID++, titleTxt = "Operation 1",
                focusID = (int) L_FOCUS.ETestFocusID.Self, effortID = (int) L_EFFORT.ETestEffortID.GrowCraft + 3,
                descTxt = string.Format("Impact for focus {0}, effort {1}", L_FOCUS.ETestFocusID.Craft, L_EFFORT.ETestEffortID.GrowCraft + 3),
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
        public List<D_L_IMPACT> SelectList (F_L_IMPACT aFilter)
        {
            IEnumerable<D_L_IMPACT> lResult = from item       in ResourceList
                                              join focusItem  in L_FOCUS.ResourceList  on item.focusID  equals focusItem.objectID
                                              join effortItem in L_EFFORT.ResourceList on item.effortID equals effortItem.objectID
                                              select new D_L_IMPACT
                                              {
                                                     focusID        = item.focusID,
                                                     focusTitleTxt  = focusItem.titleTxt,
                                                     effortID       = item.effortID,
                                                     effortTitleTxt = effortItem.titleTxt,
                                                     titleTxt       = item.titleTxt,
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
            if (aFilter.focusID.HasValue)
            {
                lResult = lResult.Where (x => x.focusID == aFilter.focusID.Value);
            }

            if (aFilter.effortID.HasValue)
            {
                lResult = lResult.Where (x => x.effortID == aFilter.effortID.Value);
            }

            if (! string.IsNullOrEmpty (aFilter.searchTxt))
            {
                lResult = lResult.Where (x => x.titleTxt.Contains (aFilter.searchTxt));
            }

            // check base criteria
            lResult = CheckBaseCriteria (lResult, aFilter);

            // return result
            return lResult.ToList<D_L_IMPACT>();
        }

        /// <summary>
        /// remove all matching items from persistent store
        /// </summary>
        /// <param name="aFilter"></param>
        public void DeleteList (F_L_IMPACT aFilter)
        {
            throw new NotImplementedException ("L_IMPACT.DeleteList not implemented");
        }

        /// <summary>
        /// select an item from persistent store for given key
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public D_L_IMPACT SelectItem (K_L_IMPACT aKey)
        {
            D_L_IMPACT lResult = null;

            // apply key attributes
            if (aKey.objectID.HasValue)
            {
                F_L_IMPACT lFilter = new F_L_IMPACT() { objectID = aKey.objectID.Value };

                lResult = SelectList(lFilter).FirstOrDefault();
            }

            // throw exception if not found
            if (lResult == null)
                throw new DllNotFoundException (string.Format ("L_IMPACT Item not found for key {0}", aKey.objectID));

            // return result
            return lResult;
        }

        /// <summary>
        /// insert an item into persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_L_IMPACT InsertItem (D_L_IMPACT aDto)
        {
            int lID = 0;

            if (ResourceList.Count > 0)
                lID = ResourceList.Select(x => x.objectID).Max() + 1;

            // create new item
            D_L_IMPACT lItem = new D_L_IMPACT
            {
                // attribute fields
                focusID  = aDto.focusID,
                effortID = aDto.effortID,
                titleTxt = aDto.titleTxt,
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
        public D_L_IMPACT UpdateItem (D_L_IMPACT aDto)
        {
            // fetch indicated item
            D_L_IMPACT lItem = ResourceList.Where (x => x.objectID == aDto.objectID).FirstOrDefault();

            // update item
            lock (lItem)
            {
                // attribute fields
                lItem.focusID  = aDto.focusID;
                lItem.effortID = aDto.effortID;
                lItem.titleTxt = aDto.titleTxt;
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
        public void DeleteItem (K_L_IMPACT aKey)
        {
            // fetch indicated item
            D_L_IMPACT lItem = ResourceList.Where (x => x.objectID == aKey.objectID).FirstOrDefault();

            // delete item from list
            lock (ResourceList)
            {
                ResourceList.Remove(lItem);
            }
        }
    }
}