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
    public class L_EFFORT_SEQ : DATA_ACCESS_BASE<D_L_EFFORT_SEQ, F_L_EFFORT_SEQ, K_L_EFFORT_SEQ>, I_L_EFFORT_SEQ
    {
        #region Test Data

        public enum ETestFocusID { None = 0, Self, Family, Home, Craft }

        // resource list
        public static List<D_L_EFFORT_SEQ> ResourceList = new List<D_L_EFFORT_SEQ>();

        static L_EFFORT_SEQ()
        {
        }

        #endregion

        /// <summary>
        /// select a list of items from persistent store for given filter
        /// </summary>
        /// <param name="aFilter"></param>
        /// <returns></returns>
        public List<D_L_EFFORT_SEQ> SelectList (F_L_EFFORT_SEQ aFilter)
        {
            IEnumerable<D_L_EFFORT_SEQ> lResult = from item       in ResourceList
                                                  join sourceItem in L_EFFORT.ResourceList on item.sourceEffortID equals sourceItem.objectID
                                                  join targetItem in L_EFFORT.ResourceList on item.targetEffortID equals targetItem.objectID
                                                  select new D_L_EFFORT_SEQ
                                                  {
                                                      sourceEffortID = item.sourceEffortID,
                                                      sourceTitleTxt = sourceItem.titleTxt,
                                                      targetEffortID = item.targetEffortID,
                                                      targetTitleTxt = targetItem.titleTxt,
                                                      overlapPct     = item.overlapPct,
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
            if (aFilter.sourceEffortID.HasValue)
            {
                lResult = lResult.Where (x => x.sourceEffortID == aFilter.sourceEffortID.Value);
            }

            if (aFilter.targetEffortID.HasValue)
            {
                lResult = lResult.Where(x => x.targetEffortID == aFilter.targetEffortID.Value);
            }

            // check base criteria
            lResult = CheckBaseCriteria (lResult, aFilter);

            // return result
            return lResult.ToList<D_L_EFFORT_SEQ>();
        }

        /// <summary>
        /// remove all matching items from persistent store
        /// </summary>
        /// <param name="aFilter"></param>
        public void DeleteList (F_L_EFFORT_SEQ aFilter)
        {
            throw new NotImplementedException ("L_EFFORT_SEQ.DeleteList not implemented");
        }

        /// <summary>
        /// select an item from persistent store for given key
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public D_L_EFFORT_SEQ SelectItem (K_L_EFFORT_SEQ aKey)
        {
            D_L_EFFORT_SEQ lResult = null;

            // apply key attributes
            if (aKey.objectID.HasValue)
            {
                F_L_EFFORT_SEQ lFilter = new F_L_EFFORT_SEQ() { objectID = aKey.objectID.Value };

                lResult = SelectList(lFilter).FirstOrDefault();
            }

            // throw exception if not found
            if (lResult == null)
                throw new DllNotFoundException (string.Format ("L_EFFORT_SEQ Item not found for key {0}", aKey.objectID));

            // return result
            return lResult;
        }

        /// <summary>
        /// insert an item into persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_L_EFFORT_SEQ InsertItem (D_L_EFFORT_SEQ aDto)
        {
            int lID = 0;

            if (ResourceList.Count > 0)
                lID = ResourceList.Select(x => x.objectID).Max() + 1;

            // create new item
            D_L_EFFORT_SEQ lItem = new D_L_EFFORT_SEQ
            {
                // attribute fields
                sourceEffortID = aDto.sourceEffortID,
                targetEffortID = aDto.targetEffortID,
                overlapPct     = aDto.overlapPct,
                noteTxt        = aDto.noteTxt,
                descTxt        = aDto.descTxt,

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
        public D_L_EFFORT_SEQ UpdateItem (D_L_EFFORT_SEQ aDto)
        {
            // fetch indicated item
            D_L_EFFORT_SEQ lItem = ResourceList.Where (x => x.objectID == aDto.objectID).FirstOrDefault();

            // update item
            lock (lItem)
            {
                // attribute fields
                lItem.sourceEffortID = aDto.sourceEffortID;
                lItem.targetEffortID = aDto.targetEffortID;
                lItem.overlapPct     = aDto.overlapPct;
                lItem.noteTxt        = aDto.noteTxt;
                lItem.descTxt        = aDto.descTxt;

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
        public void DeleteItem (K_L_EFFORT_SEQ aKey)
        {
            // fetch indicated item
            D_L_EFFORT_SEQ lItem = ResourceList.Where (x => x.objectID == aKey.objectID).FirstOrDefault();

            // delete item from list
            lock (ResourceList)
            {
                ResourceList.Remove(lItem);
            }
        }
    }
}