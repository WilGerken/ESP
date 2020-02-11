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
    public class L_IMPACT_ATTR : DATA_ACCESS_BASE<D_L_IMPACT_ATTR, F_L_IMPACT_ATTR, K_L_IMPACT_ATTR>, I_L_IMPACT_ATTR
    {
        #region Test Data

        public enum ETestFocusID { None = 0, Self, Family, Home, Craft }

        // resource list
        public static List<D_L_IMPACT_ATTR> ResourceList = new List<D_L_IMPACT_ATTR>();

        static L_IMPACT_ATTR()
        {
        }

        #endregion

        /// <summary>
        /// select a list of items from persistent store for given filter
        /// </summary>
        /// <param name="aFilter"></param>
        /// <returns></returns>
        public List<D_L_IMPACT_ATTR> SelectList (F_L_IMPACT_ATTR aFilter)
        {
            IEnumerable<D_L_IMPACT_ATTR> lResult = from item       in ResourceList
                                                   join impactItem in L_IMPACT.ResourceList     on item.impactID       equals impactItem.objectID
                                                   join focusItem  in L_FOCUS.ResourceList      on impactItem.focusID  equals focusItem.objectID
                                                   join effortItem in L_EFFORT.ResourceList     on impactItem.effortID equals effortItem.objectID
                                                   join attrItem   in L_FOCUS_ATTR.ResourceList on item.focusAttrID    equals attrItem.objectID
                                                   select new D_L_IMPACT_ATTR
                                                   {
                                                       impactID       = item.impactID,
                                                       focusID        = impactItem.focusID,
                                                       focusTitleTxt  = focusItem.titleTxt,
                                                       effortID       = impactItem.effortID,
                                                       effortTitleTxt = effortItem.titleTxt,
                                                       focusAttrID    = item.focusAttrID,
                                                       impactAmt      = item.impactAmt,
                                                       descTxt        = item.descTxt,

                                                       objectID = item.objectID,
                                                       activeYn = item.activeYn,
                                                       deletedYn = item.deletedYn,
                                                       createByUid = item.createByUid,
                                                       createOnDts = item.createOnDts,
                                                       updateByUid = item.updateByUid,
                                                       updateOnDts = item.updateOnDts
                                                   };

            // apply filter attributes
            if (aFilter.impactID.HasValue)
            {
                lResult = lResult.Where(x => x.focusID == aFilter.focusID.Value);
            }

            if (aFilter.focusID.HasValue)
            {
                lResult = lResult.Where (x => x.focusID == aFilter.focusID.Value);
            }

            if (aFilter.effortID.HasValue)
            {
                lResult = lResult.Where (x => x.effortID == aFilter.effortID.Value);
            }

            // check base criteria
            lResult = CheckBaseCriteria (lResult, aFilter);

            // return result
            return lResult.ToList<D_L_IMPACT_ATTR>();
        }

        /// <summary>
        /// remove all matching items from persistent store
        /// </summary>
        /// <param name="aFilter"></param>
        public void DeleteList (F_L_IMPACT_ATTR aFilter)
        {
            throw new NotImplementedException ("L_IMPACT_ATTR.DeleteList not implemented");
        }

        /// <summary>
        /// select an item from persistent store for given key
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public D_L_IMPACT_ATTR SelectItem (K_L_IMPACT_ATTR aKey)
        {
            D_L_IMPACT_ATTR lResult = null;

            // apply key attributes
            if (aKey.objectID.HasValue)
            {
                F_L_IMPACT_ATTR lFilter = new F_L_IMPACT_ATTR() { objectID = aKey.objectID.Value };

                lResult = SelectList(lFilter).FirstOrDefault();
            }

            // throw exception if not found
            if (lResult == null)
                throw new DllNotFoundException (string.Format ("L_IMPACT_ATTR Item not found for key {0}", aKey.objectID));

            // return result
            return lResult;
        }

        /// <summary>
        /// insert an item into persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_L_IMPACT_ATTR InsertItem (D_L_IMPACT_ATTR aDto)
        {
            int lID = 0;

            if (ResourceList.Count > 0)
                lID = ResourceList.Select(x => x.objectID).Max() + 1;

            // create new item
            D_L_IMPACT_ATTR lItem = new D_L_IMPACT_ATTR
            {
                // attribute fields
                impactID     = aDto.impactID,
                focusID      = aDto.focusID,
                effortID     = aDto.effortID,
                focusAttrID  = aDto.focusAttrID,
                impactAmt    = aDto.impactAmt,
                descTxt      = aDto.descTxt,

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
        public D_L_IMPACT_ATTR UpdateItem (D_L_IMPACT_ATTR aDto)
        {
            // fetch indicated item
            D_L_IMPACT_ATTR lItem = ResourceList.Where (x => x.objectID == aDto.objectID).FirstOrDefault();

            // update item
            lock (lItem)
            {
                // attribute fields
                lItem.impactID     = aDto.impactID;
                lItem.focusID      = aDto.focusID;
                lItem.effortID     = aDto.effortID;
                lItem.focusAttrID  = aDto.focusAttrID;
                lItem.impactAmt    = aDto.impactAmt;
                lItem.descTxt      = aDto.descTxt;

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
        public void DeleteItem (K_L_IMPACT_ATTR aKey)
        {
            // fetch indicated item
            D_L_IMPACT_ATTR lItem = ResourceList.Where (x => x.objectID == aKey.objectID).FirstOrDefault();

            // delete item from list
            lock (ResourceList)
            {
                ResourceList.Remove(lItem);
            }
        }
    }
}