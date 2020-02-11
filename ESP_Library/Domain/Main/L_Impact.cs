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
    public class L_Impact_ItemCriteria : ItemCriteria_Base<L_Impact_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int?> FocusID_Property = RegisterProperty<int?>(c => c.FocusID);
        public int? FocusID
        {
            get => ReadProperty(FocusID_Property);
            set => LoadProperty(FocusID_Property, value);
        }

        public static readonly PropertyInfo<int?> EffortID_Property = RegisterProperty<int?>(c => c.EffortID);
        public int? EffortID
        {
            get => ReadProperty(EffortID_Property);
            set => LoadProperty(EffortID_Property, value);
        }

        public K_L_IMPACT ToDto()
        {
            K_L_IMPACT dto = new K_L_IMPACT
            {
                focusID  = FocusID,
                effortID = EffortID
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
    public class L_Impact_ListCriteria : ListCriteria_Base<L_Impact_ListCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int?> FocusID_Property = RegisterProperty<int?>(c => c.FocusID);
        public int? FocusID
        {
            get => ReadProperty(FocusID_Property);
            set => LoadProperty(FocusID_Property, value);
        }

        public static readonly PropertyInfo<int?> EffortID_Property = RegisterProperty<int?>(c => c.EffortID);
        public int? EffortID
        {
            get => ReadProperty(EffortID_Property);
            set => LoadProperty(EffortID_Property, value);
        }

        public static readonly PropertyInfo<string> SearchTxt_Property = RegisterProperty<string>(c => c.SearchTxt);
        public string SearchTxt
        {
            get => ReadProperty(SearchTxt_Property);
            set => LoadProperty(SearchTxt_Property, value);
        }

        public F_L_IMPACT ToDto()
        {
            F_L_IMPACT dto = new F_L_IMPACT
            {
                focusID = FocusID,
                effortID = EffortID,
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
    public class L_Impact_InfoItem : InfoItem_Base<L_Impact_InfoItem, L_Impact_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int> FocusID_Property = RegisterProperty<int>(c => c.FocusID);
        public int FocusID
        {
            get => ReadProperty(FocusID_Property);
            set => LoadProperty(FocusID_Property, value);
        }

        public static readonly PropertyInfo<string> FocusTitleTxt_Property = RegisterProperty<string>(c => c.FocusTitleTxt);
        public string FocusTitleTxt
        {
            get => ReadProperty(FocusTitleTxt_Property);
            set => LoadProperty(FocusTitleTxt_Property, value);
        }

        public static readonly PropertyInfo<int> EffortID_Property = RegisterProperty<int>(c => c.EffortID);
        public int EffortID
        {
            get => ReadProperty(EffortID_Property);
            set => LoadProperty(EffortID_Property, value);
        }

        public static readonly PropertyInfo<string> EffortTitleTxt_Property = RegisterProperty<string>(c => c.EffortTitleTxt);
        public string EffortTitleTxt
        {
            get => ReadProperty(EffortTitleTxt_Property);
            set => LoadProperty(EffortTitleTxt_Property, value);
        }

        public static readonly PropertyInfo<string> TitleTxt_Property = RegisterProperty<string>(c => c.TitleTxt);
        public string TitleTxt
        {
            get => ReadProperty(TitleTxt_Property);
            set => LoadProperty(TitleTxt_Property, value);
        }

        public static readonly PropertyInfo<string> DescTxt_Property = RegisterProperty<string>(c => c.DescTxt);
        public string DescTxt
        {
            get => ReadProperty(DescTxt_Property);
            set => LoadProperty(DescTxt_Property, value);
        }

        public void FromDto(D_L_IMPACT dto)
        {
            FocusID        = dto.focusID;
            FocusTitleTxt  = dto.focusTitleTxt;
            EffortID       = dto.effortID;
            EffortTitleTxt = dto.effortTitleTxt;
            TitleTxt       = dto.titleTxt;
            DescTxt        = dto.descTxt;

            base.FromDto(dto);
        }

        #endregion

        #region DataPortal

        [FetchChild]
        private void Child_Fetch(D_L_IMPACT dto) { FromDto(dto); }

        [Fetch]
        private void DataPortal_Fetch(L_Impact_ItemCriteria aKey)
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_L_IMPACT>();
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
    public class L_Impact_InfoList : InfoList_Base<L_Impact_InfoList, L_Impact_ListCriteria, L_Impact_InfoItem, L_Impact_ItemCriteria>
    {
        #region DataPortal

        [Fetch]
        private void DataPortal_Fetch(L_Impact_ListCriteria aCriteria)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // add select option if given
            if (aCriteria.SelectOption_Value.HasValue)
            {
                Insert(0, DataPortal.FetchChild<L_Impact_InfoItem>(new D_L_IMPACT
                {
                    selectTxt = aCriteria.SelectOption_Text,
                    objectID = aCriteria.SelectOption_Value.Value
                }));
            }

            // add elements of list from persistent store
            using (var mgr = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                var prov = mgr.GetProvider<I_L_IMPACT>();
                var list = prov.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<L_Impact_InfoItem>(item));
            }

            RaiseListChangedEvents = rlce;
            IsReadOnly = true;
        }

        #endregion
    }

    [Serializable]
    public class L_Impact_EditItem : EditItem_Base<L_Impact_EditItem, L_Impact_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int> FocusID_Property = RegisterProperty<int>(c => c.FocusID);
        [Required]
        public int FocusID
        {
            get => GetProperty(FocusID_Property);
            set => SetProperty(FocusID_Property, value);
        }

        public static readonly PropertyInfo<string> FocusTitleTxt_Property = RegisterProperty<string>(c => c.FocusTitleTxt);
        public string FocusTitleTxt
        {
            get => GetProperty(FocusTitleTxt_Property);
            set => SetProperty(FocusTitleTxt_Property, value);
        }

        public static readonly PropertyInfo<int> EffortID_Property = RegisterProperty<int>(c => c.EffortID);
        [Required]
        public int EffortID
        {
            get => GetProperty(EffortID_Property);
            set => SetProperty(EffortID_Property, value);
        }

        public static readonly PropertyInfo<string> EffortTitleTxt_Property = RegisterProperty<string>(c => c.EffortTitleTxt);
        public string EffortTitleTxt
        {
            get => GetProperty(EffortTitleTxt_Property);
            set => SetProperty(EffortTitleTxt_Property, value);
        }

        public static readonly PropertyInfo<string> TitleTxt_Property = RegisterProperty<string>(c => c.TitleTxt);
        [Required]
        public string TitleTxt
        {
            get => GetProperty(TitleTxt_Property);
            set => SetProperty(TitleTxt_Property, value);
        }

        public static readonly PropertyInfo<string> DescTxt_Property = RegisterProperty<string>(c => c.DescTxt);
        public string DescTxt
        {
            get => GetProperty(DescTxt_Property);
            set => SetProperty(DescTxt_Property, value);
        }

        public void FromDto(D_L_IMPACT dto)
        {
            FocusID        = dto.focusID;
            FocusTitleTxt  = dto.focusTitleTxt;
            EffortID       = dto.effortID;
            EffortTitleTxt = dto.effortTitleTxt;
            TitleTxt       = dto.titleTxt;
            DescTxt        = dto.descTxt;

            base.FromDto(dto);
        }

        public D_L_IMPACT ToDto()
        {
            D_L_IMPACT dto = new D_L_IMPACT
            {
                focusID        = FocusID,
                focusTitleTxt  = FocusTitleTxt,
                effortID       = EffortID,
                effortTitleTxt = EffortTitleTxt,
                titleTxt       = TitleTxt,
                descTxt        = DescTxt
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
        protected void DataPortal_Create(L_Impact_ItemCriteria aKey)
        {
            base.DataPortal_Create();

            if (aKey != null)
            {
                FocusID  = aKey.FocusID.Value;
                EffortID = aKey.EffortID.Value;
            }
        }

        [CreateChild]
        protected override void Child_Create()
        {
            base.Child_Create();
        }

        [Fetch]
        private void DataPortal_Fetch(L_Impact_ItemCriteria aKey)
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_L_IMPACT>();
                var data = dal.SelectItem(aKey.ToDto());

                using (BypassPropertyChecks) { FromDto(data); }

                BusinessRules.CheckRules();
            }
        }

        [FetchChild]
        private void Child_Fetch(D_L_IMPACT dto) { FromDto(dto); }

        [Insert]
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_L_IMPACT>();
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
                var dal = dalManager.GetProvider<I_L_IMPACT>();
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

                var dal = dalManager.GetProvider<I_L_IMPACT>();
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

                var dal = dalManager.GetProvider<I_L_IMPACT>();
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
                var dal = dalManager.GetProvider<I_L_IMPACT>();

                dal.DeleteItem(new K_L_IMPACT { objectID = this.ObjectID });
            }
        }

        [DeleteSelfChild]
        private void Child_DeleteSelf()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                FieldManager.UpdateChildren(this);

                var dal = dalManager.GetProvider<I_L_IMPACT>();
                using (BypassPropertyChecks)
                {
                    dal.DeleteItem(new K_L_IMPACT { objectID = this.ObjectID });
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// Unit of Work Getter
    /// </summary>
    [Serializable]
    public class L_Impact_EditItem_Getter : EditItem_Getter_Base<L_Impact_EditItem, L_Impact_ItemCriteria>
    {
        #region DataPortal

        [Fetch]
        protected override void DataPortal_Fetch(L_Impact_ItemCriteria aCriteria)
        {
            if (aCriteria.HasKey)
                EditItem = L_Impact_EditItem.GetItem(aCriteria);
            else
                EditItem = L_Impact_EditItem.NewItem(aCriteria);
        }

        #endregion
    }

    /// <summary>
    /// Editable List
    /// </summary>
    [Serializable]
    public class L_Impact_EditList : EditList_Base<L_Impact_EditList, L_Impact_ListCriteria, L_Impact_EditItem, L_Impact_ItemCriteria>
    {
        #region DataPortal

        [Fetch]
        private void DataPortal_Fetch(L_Impact_ListCriteria aCriteria)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            using (var mgr = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                var prov = mgr.GetProvider<I_L_IMPACT>();
                var list = prov.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<L_Impact_EditItem>(item));
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
