using System;
using System.Collections.Generic;
using System.Linq;
using CoreLib.Common;
using Library.Common;

namespace Library.Resources.Main.Memory
{
    /// <summary>
    /// data access class
    /// </summary>
    public class L_EFFORT : DATA_ACCESS_BASE<D_L_EFFORT, F_L_EFFORT, K_L_EFFORT>, I_L_EFFORT
    {
        #region Test Data

        public enum ETestEffortID { None = 0, GrowSelf=10, GrowFamily=20, GrowHome=30, GrowCraft=40 }

        // resource list
        public static List<D_L_EFFORT> ResourceList = new List<D_L_EFFORT>();

        static L_EFFORT()
        {
            int lID = (int) ETestEffortID.GrowSelf;
            ResourceList.Add (new D_L_EFFORT
            {
                objectID    = lID, titleTxt = "Grow Self", effortTypeID = EEffortType.Operation,
                descTxt     = string.Format ("Maintain Self"),
                createByUid = LibRef.AdminUid, createOnDts = DateTime.Now,
                updateByUid = LibRef.AdminUid, updateOnDts = DateTime.Now
            });
            ResourceList.Add (new D_L_EFFORT
            {
                objectID = lID+1, parentID=lID, titleTxt = "Meditate", effortTypeID = EEffortType.Operation,
                descTxt = string.Format("Meditate Daily"),
                createByUid = LibRef.AdminUid, createOnDts = DateTime.Now,
                updateByUid = LibRef.AdminUid, updateOnDts = DateTime.Now
            });

            lID = (int) ETestEffortID.GrowFamily;
            ResourceList.Add (new D_L_EFFORT
            {
                objectID    = lID, titleTxt = "Grow Family", effortTypeID = EEffortType.Operation,
                descTxt     = string.Format ("Maintain Fmaily"),
                createByUid = LibRef.AdminUid, createOnDts = DateTime.Now,
                updateByUid = LibRef.AdminUid, updateOnDts = DateTime.Now
            });
            ResourceList.Add (new D_L_EFFORT
            {
                objectID    = lID+1, parentID=lID, titleTxt = "Be nice to wife", effortTypeID = EEffortType.Operation,
                descTxt     = string.Format ("Be nice to wife"),
                createByUid = LibRef.AdminUid, createOnDts = DateTime.Now,
                updateByUid = LibRef.AdminUid, updateOnDts = DateTime.Now
            });
            ResourceList.Add(new D_L_EFFORT
            {
                objectID = lID+2, parentID=lID, titleTxt = "Raise Kid", effortTypeID = EEffortType.Project,
                descTxt = string.Format ("Raise kids"),
                createByUid = LibRef.AdminUid, createOnDts = DateTime.Now,
                updateByUid = LibRef.AdminUid, updateOnDts = DateTime.Now
            });

            lID = (int) ETestEffortID.GrowHome;
            ResourceList.Add (new D_L_EFFORT
            {
                objectID    = lID, titleTxt = "Grow Home", effortTypeID = EEffortType.Operation,
                descTxt     = string.Format ("Maintain Home"),
                createByUid = LibRef.AdminUid, createOnDts = DateTime.Now,
                updateByUid = LibRef.AdminUid, updateOnDts = DateTime.Now
            });
            ResourceList.Add(new D_L_EFFORT
            {
                objectID = lID+1, parentID=lID, titleTxt = "Mow Lawn",  effortTypeID = EEffortType.Operation,
                descTxt = string.Format("Mown Lawn Weekly"),
                createByUid = LibRef.AdminUid, createOnDts = DateTime.Now,
                updateByUid = LibRef.AdminUid, updateOnDts = DateTime.Now
            });
            ResourceList.Add(new D_L_EFFORT
            {
                objectID = lID+2, parentID=lID, titleTxt = "Clean out garage", effortTypeID = EEffortType.Project,
                descTxt = string.Format("Clean out Garage"),
                createByUid = LibRef.AdminUid, createOnDts = DateTime.Now,
                updateByUid = LibRef.AdminUid, updateOnDts = DateTime.Now
            });

            lID = (int) ETestEffortID.GrowCraft;
            ResourceList.Add (new D_L_EFFORT
            {
                objectID    = lID, titleTxt = "Grow Craft", effortTypeID = EEffortType.Operation,
                descTxt     = string.Format ("Maintain Employment"),
                createByUid = LibRef.AdminUid, createOnDts = DateTime.Now,
                updateByUid = LibRef.AdminUid, updateOnDts = DateTime.Now
            });
            ResourceList.Add(new D_L_EFFORT
            {
                objectID = lID+1, parentID=lID, titleTxt = "Project 1", effortTypeID = EEffortType.Project,
                descTxt = string.Format("Work on Project 1"),
                createByUid = LibRef.AdminUid, createOnDts = DateTime.Now,
                updateByUid = LibRef.AdminUid, updateOnDts = DateTime.Now
            });
            ResourceList.Add(new D_L_EFFORT
            {
                objectID = lID+2, parentID=lID, titleTxt = "Project 2", effortTypeID = EEffortType.Project,
                descTxt = string.Format("Work on Project 2"),
                createByUid = LibRef.AdminUid, createOnDts = DateTime.Now,
                updateByUid = LibRef.AdminUid, updateOnDts = DateTime.Now
            });
            ResourceList.Add(new D_L_EFFORT
            {
                objectID = lID+3, parentID=lID, titleTxt = "Operation 1", effortTypeID = EEffortType.Operation,
                descTxt = string.Format("Work on Operation 1"),
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
        public List<D_L_EFFORT> SelectList (F_L_EFFORT aFilter)
        {
            IEnumerable<D_L_EFFORT> lResult = from item in ResourceList
                                              join typeItem in L_EFFORT_TYPE.ResourceList on (int) item.effortTypeID equals typeItem.objectID
                                              select new D_L_EFFORT
                                              {
                                                     titleTxt      = item.titleTxt,
                                                     effortTypeID  = item.effortTypeID,
                                                     effortTypeTxt = typeItem.typeTxt,
                                                     parentID      = item.parentID,
                                                     descTxt       = item.descTxt,

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
            return lResult.ToList<D_L_EFFORT>();
        }

        /// <summary>
        /// remove all matching items from persistent store
        /// </summary>
        /// <param name="aFilter"></param>
        public void DeleteList (F_L_EFFORT aFilter)
        {
            throw new NotImplementedException ("L_EFFORT.DeleteList not implemented");
        }

        /// <summary>
        /// select an item from persistent store for given key
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public D_L_EFFORT SelectItem (K_L_EFFORT aKey)
        {
            D_L_EFFORT lResult = null;

            // apply key attributes
            if (aKey.objectID.HasValue)
            {
                F_L_EFFORT lFilter = new F_L_EFFORT() { objectID = aKey.objectID.Value };

                lResult = SelectList(lFilter).FirstOrDefault();
            }

            // throw exception if not found
            if (lResult == null)
                throw new DllNotFoundException (string.Format ("L_EFFORT Item not found for key {0}", aKey.objectID));

            // return result
            return lResult;
        }

        /// <summary>
        /// insert an item into persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_L_EFFORT InsertItem (D_L_EFFORT aDto)
        {
            int lID = 0;

            if (ResourceList.Count > 0)
                lID = ResourceList.Select(x => x.objectID).Max() + 1;

            // create new item
            D_L_EFFORT lItem = new D_L_EFFORT
            {
                // attribute fields
                titleTxt      = aDto.titleTxt,
                effortTypeID = aDto.effortTypeID,
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
        public D_L_EFFORT UpdateItem (D_L_EFFORT aDto)
        {
            // fetch indicated item
            D_L_EFFORT lItem = ResourceList.Where (x => x.objectID == aDto.objectID).FirstOrDefault();

            // update item
            lock (lItem)
            {
                // attribute fields
                lItem.titleTxt      = aDto.titleTxt;
                lItem.effortTypeID  = aDto.effortTypeID;
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
        public void DeleteItem (K_L_EFFORT aKey)
        {
            // fetch indicated item
            D_L_EFFORT lItem = ResourceList.Where (x => x.objectID == aKey.objectID).FirstOrDefault();

            // delete item from list
            lock (ResourceList)
            {
                ResourceList.Remove(lItem);
            }
        }
    }
}