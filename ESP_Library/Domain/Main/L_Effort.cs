using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib.Common;
using CoreLib.Resources;
using Library.Common;
using Library.Resources;
using Library.Resources.Main;
using Csla;

namespace Library.Domain.Main
{
    /// <summary>
    /// Item Criteria
    /// </summary>
    [Serializable]
    public class L_Effort_ItemCriteria : ItemCriteria_Base<L_Effort_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<string> TitleTxt_Property = RegisterProperty<string>(c => c.TitleTxt);
        public string TitleTxt
        {
            get => ReadProperty(TitleTxt_Property);
            set => LoadProperty(TitleTxt_Property, value);
        }

        public K_L_EFFORT ToDto()
        {
            K_L_EFFORT dto = new K_L_EFFORT
            {
                titleTxt = TitleTxt
            };

            base.ToDto(dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// List Criteria
    /// </summary>
    [Serializable]
    public class L_Effort_ListCriteria : ListCriteria_Base<L_Effort_ListCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<string> TitleTxt_Property = RegisterProperty<string>(c => c.TitleTxt);
        public string TitleTxt
        {
            get => ReadProperty(TitleTxt_Property);
            set => LoadProperty(TitleTxt_Property, value);
        }

        public static readonly PropertyInfo<int?> ParentID_Property = RegisterProperty<int?>(c => c.ParentID);
        public int? ParentID
        {
            get => ReadProperty(ParentID_Property);
            set => LoadProperty(ParentID_Property, value);
        }

        public static readonly PropertyInfo<string> SearchTxt_Property = RegisterProperty<string>(c => c.SearchTxt);
        public string SearchTxt
        {
            get => ReadProperty(SearchTxt_Property);
            set => LoadProperty(SearchTxt_Property, value);
        }

        public F_L_EFFORT ToDto()
        {
            F_L_EFFORT dto = new F_L_EFFORT
            {
                titleTxt = TitleTxt,
                parentID = ParentID,
                searchTxt = SearchTxt
            };

            base.ToDto(dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// ReadOnly Item
    /// </summary>
    [Serializable]
    public class L_Effort_InfoItem : InfoItem_Base<L_Effort_InfoItem, L_Effort_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<string> TitleTxt_Property = RegisterProperty<string>(c => c.TitleTxt);
        public string TitleTxt
        {
            get => ReadProperty(TitleTxt_Property);
            set => LoadProperty(TitleTxt_Property, value);
        }

        public static readonly PropertyInfo<EEffortType> EffortTypeID_Property = RegisterProperty<EEffortType>(c => c.EffortTypeID);
        public EEffortType EffortTypeID
        {
            get => ReadProperty(EffortTypeID_Property);
            set => LoadProperty(EffortTypeID_Property, value);
        }

        public static readonly PropertyInfo<string> EffortTypeTxt_Property = RegisterProperty<string>(c => c.EffortTypeTxt);
        public string EffortTypeTxt
        {
            get => ReadProperty(EffortTypeTxt_Property);
            set => LoadProperty(EffortTypeTxt_Property, value);
        }

        public static readonly PropertyInfo<int?> ParentID_Property = RegisterProperty<int?>(c => c.ParentID);
        public int? ParentID
        {
            get => ReadProperty(ParentID_Property);
            set => LoadProperty(ParentID_Property, value);
        }

        public static readonly PropertyInfo<string> DescTxt_Property = RegisterProperty<string>(c => c.DescTxt);
        public string DescTxt
        {
            get => ReadProperty(DescTxt_Property);
            set => LoadProperty(DescTxt_Property, value);
        }

        public void FromDto(D_L_EFFORT dto)
        {
            TitleTxt      = dto.titleTxt;
            EffortTypeID  = dto.effortTypeID;
            EffortTypeTxt = dto.effortTypeTxt;
            ParentID      = dto.parentID;
            DescTxt       = dto.descTxt;

            base.FromDto(dto);
        }

        #endregion

        #region DataPortal

        [FetchChild]
        private void Child_Fetch(D_L_EFFORT dto) { FromDto(dto); }

        [Fetch]
        private void DataPortal_Fetch(L_Effort_ItemCriteria aKey)
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_L_EFFORT>();
                var data = dal.SelectItem(aKey.ToDto());

                FromDto(data);
            }
        }

        #endregion
    }

    /// <summary>
    /// ReadOnly List
    /// </summary>
    [Serializable]
    public class L_Effort_InfoList : InfoList_Base<L_Effort_InfoList, L_Effort_ListCriteria, L_Effort_InfoItem, L_Effort_ItemCriteria>
    {
        #region DataPortal

        [Fetch]
        private void DataPortal_Fetch(L_Effort_ListCriteria aCriteria)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // add select option if given
            if (aCriteria.SelectOption_Value.HasValue)
            {
                Insert(0, DataPortal.FetchChild<L_Effort_InfoItem>(new D_L_EFFORT
                {
                    selectTxt = aCriteria.SelectOption_Text,
                    objectID = aCriteria.SelectOption_Value.Value
                }));
            }

            // add elements of list from persistent store
            using (var mgr = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                var prov = mgr.GetProvider<I_L_EFFORT>();
                var list = prov.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<L_Effort_InfoItem>(item));
            }

            RaiseListChangedEvents = rlce;
            IsReadOnly = true;
        }

        #endregion
    }

    [Serializable]
    public class L_Effort_EditItem : EditItem_Base<L_Effort_EditItem, L_Effort_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<string> TitleTxt_Property = RegisterProperty<string>(c => c.TitleTxt);
        [Required]
        public string TitleTxt
        {
            get => GetProperty(TitleTxt_Property);
            set => SetProperty(TitleTxt_Property, value);
        }

        public static readonly PropertyInfo<EEffortType> EffortTypeID_Property = RegisterProperty<EEffortType>(c => c.EffortTypeID);
        [Required]
        public EEffortType EffortTypeID
        {
            get => GetProperty(EffortTypeID_Property);
            set => SetProperty(EffortTypeID_Property, value);
        }

        public static readonly PropertyInfo<string> EffortTypeTxt_Property = RegisterProperty<string>(c => c.EffortTypeTxt);
        [Required]
        public string EffortTypeTxt
        {
            get => GetProperty(EffortTypeTxt_Property);
            set => SetProperty(EffortTypeTxt_Property, value);
        }

        public static readonly PropertyInfo<int?> ParentID_Property = RegisterProperty<int?>(c => c.ParentID);
        public int? ParentID
        {
            get => GetProperty(ParentID_Property);
            set => SetProperty(ParentID_Property, value);
        }

        public static readonly PropertyInfo<string> DescTxt_Property = RegisterProperty<string>(c => c.DescTxt);
        public string DescTxt
        {
            get => GetProperty(DescTxt_Property);
            set => SetProperty(DescTxt_Property, value);
        }

        public void FromDto(D_L_EFFORT dto)
        {
            TitleTxt      = dto.titleTxt;
            EffortTypeID  = dto.effortTypeID;
            EffortTypeTxt = dto.effortTypeTxt;
            ParentID      = dto.parentID;
            DescTxt       = dto.descTxt;

            base.FromDto(dto);
        }

        public D_L_EFFORT ToDto()
        {
            D_L_EFFORT dto = new D_L_EFFORT
            {
                titleTxt      = TitleTxt,
                effortTypeID  = EffortTypeID,
                effortTypeTxt = EffortTypeTxt,
                parentID      = ParentID,
                descTxt       = DescTxt
            };

            base.ToDto(dto);

            return dto;
        }

        #endregion

        #region Methods

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
        }

        #endregion

        #region DataPortal

        [RunLocal]
        [Create]
        protected override void DataPortal_Create()
        {
            base.DataPortal_Create();
        }

        [Create]
        protected void DataPortal_Create(L_Effort_ItemCriteria aKey)
        {
            base.DataPortal_Create();

            if (aKey != null)
            {
                TitleTxt = aKey.TitleTxt;
            }
        }

        [CreateChild]
        protected override void Child_Create()
        {
            base.Child_Create();
        }

        [Fetch]
        private void DataPortal_Fetch(L_Effort_ItemCriteria aKey)
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_L_EFFORT>();
                var data = dal.SelectItem(aKey.ToDto());

                using (BypassPropertyChecks) { FromDto(data); }

                BusinessRules.CheckRules();
            }
        }

        [FetchChild]
        private void Child_Fetch(D_L_EFFORT dto) { FromDto(dto); }

        [Insert]
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_L_EFFORT>();
                var data = dal.InsertItem(ToDto());

                using (BypassPropertyChecks) { FromDto(data); }

                FieldManager.UpdateChildren(this);
            }
        }

        [InsertChild]
        private void Child_Insert()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_L_EFFORT>();
                var data = dal.InsertItem(ToDto());

                using (BypassPropertyChecks) { FromDto(data); }

                FieldManager.UpdateChildren(this);
            }
        }

        [Update]
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                UpdateOnDts = DateTime.Now;
                UpdateByUid = AppInfo.UserUid;

                var dal = dalManager.GetProvider<I_L_EFFORT>();
                var data = dal.UpdateItem(ToDto());

                using (BypassPropertyChecks) { FromDto(data); }

                FieldManager.UpdateChildren(this);
            }
        }

        [UpdateChild]
        private void Child_Update()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                UpdateOnDts = DateTime.Now;
                UpdateByUid = AppInfo.UserUid;

                var dal = dalManager.GetProvider<I_L_EFFORT>();
                var data = dal.UpdateItem(ToDto());

                using (BypassPropertyChecks) { FromDto(data); }

                FieldManager.UpdateChildren(this);
            }
        }

        [DeleteSelf]
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_L_EFFORT>();

                dal.DeleteItem(new K_L_EFFORT { objectID = this.ObjectID });
            }
        }

        [DeleteSelfChild]
        private void Child_DeleteSelf()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                FieldManager.UpdateChildren(this);

                var dal = dalManager.GetProvider<I_L_EFFORT>();
                using (BypassPropertyChecks)
                {
                    dal.DeleteItem(new K_L_EFFORT { objectID = this.ObjectID });
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// Unit of Work Getter
    /// </summary>
    [Serializable]
    public class L_Effort_EditItem_Getter : EditItem_Getter_Base<L_Effort_EditItem, L_Effort_ItemCriteria>
    {
        #region DataPortal

        [Fetch]
        protected override void DataPortal_Fetch(L_Effort_ItemCriteria aCriteria)
        {
            if (aCriteria.HasKey)
                EditItem = L_Effort_EditItem.GetItem(aCriteria);
            else
                EditItem = L_Effort_EditItem.NewItem(aCriteria);
        }

        #endregion
    }

    /// <summary>
    /// Editable List
    /// </summary>
    [Serializable]
    public class L_Effort_EditList : EditList_Base<L_Effort_EditList, L_Effort_ListCriteria, L_Effort_EditItem, L_Effort_ItemCriteria>
    {
        #region DataPortal

        [Fetch]
        private void DataPortal_Fetch(L_Effort_ListCriteria aCriteria)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            using (var mgr = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                var prov = mgr.GetProvider<I_L_EFFORT>();
                var list = prov.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<L_Effort_EditItem>(item));
            }

            RaiseListChangedEvents = rlce;
        }

        [Update]
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var mgr = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                Child_Update();
            }
        }

        #endregion
    }
}
