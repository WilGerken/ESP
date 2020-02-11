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
    public class L_EffortType_ItemCriteria : ItemCriteria_Base<L_EffortType_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<string> TypeCd_Property = RegisterProperty<string>(c => c.TypeCd);
        public string TypeCd
        {
            get => ReadProperty(TypeCd_Property);
            set => LoadProperty(TypeCd_Property, value);
        }

        public K_L_EFFORT_TYPE ToDto()
        {
            K_L_EFFORT_TYPE dto = new K_L_EFFORT_TYPE
            {
                typeCd = TypeCd
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
    public class L_EffortType_ListCriteria : ListCriteria_Base<L_EffortType_ListCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<string> TypeCd_Property = RegisterProperty<string>(c => c.TypeCd);
        public string TypeCd
        {
            get => ReadProperty(TypeCd_Property);
            set => LoadProperty(TypeCd_Property, value);
        }

        public F_L_EFFORT_TYPE ToDto()
        {
            F_L_EFFORT_TYPE dto = new F_L_EFFORT_TYPE
            {
                typeCd = TypeCd
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
    public class L_EffortType_InfoItem : InfoItem_Base<L_EffortType_InfoItem, L_EffortType_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<string> TypeCd_Property = RegisterProperty<string>(c => c.TypeCd);
        public string TypeCd
        {
            get => ReadProperty(TypeCd_Property);
            set => LoadProperty(TypeCd_Property, value);
        }

        public static readonly PropertyInfo<string> TypeTxt_Property = RegisterProperty<string>(c => c.TypeTxt);
        public string TypeTxt
        {
            get => ReadProperty(TypeTxt_Property);
            set => LoadProperty(TypeTxt_Property, value);
        }

        public static readonly PropertyInfo<string> DescTxt_Property = RegisterProperty<string>(c => c.DescTxt);
        public string DescTxt
        {
            get => ReadProperty(DescTxt_Property);
            set => LoadProperty(DescTxt_Property, value);
        }

        public void FromDto(D_L_EFFORT_TYPE dto)
        {
            TypeCd  = dto.typeCd;
            TypeTxt = dto.typeTxt;
            DescTxt = dto.descTxt;

            base.FromDto(dto);
        }

        #endregion

        #region DataPortal

        [FetchChild]
        private void Child_Fetch(D_L_EFFORT_TYPE dto) { FromDto(dto); }

        [Fetch]
        private void DataPortal_Fetch(L_EffortType_ItemCriteria aKey)
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_L_EFFORT_TYPE>();
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
    public class L_EffortType_InfoList : InfoList_Base<L_EffortType_InfoList, L_EffortType_ListCriteria, L_EffortType_InfoItem, L_EffortType_ItemCriteria>
    {
        #region DataPortal

        [Fetch]
        private void DataPortal_Fetch(L_EffortType_ListCriteria aCriteria)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // add select option if given
            if (aCriteria.SelectOption_Value.HasValue)
            {
                Insert(0, DataPortal.FetchChild<L_EffortType_InfoItem>(new D_L_EFFORT_TYPE
                {
                    selectTxt = aCriteria.SelectOption_Text,
                    objectID = aCriteria.SelectOption_Value.Value
                }));
            }

            // add elements of list from persistent store
            using (var mgr = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                var prov = mgr.GetProvider<I_L_EFFORT_TYPE>();
                var list = prov.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<L_EffortType_InfoItem>(item));
            }

            RaiseListChangedEvents = rlce;
            IsReadOnly = true;
        }

        #endregion
    }

    [Serializable]
    public class L_EffortType_EditItem : EditItem_Base<L_EffortType_EditItem, L_EffortType_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<string> TypeCd_Property = RegisterProperty<string>(c => c.TypeCd);
        [Required]
        public string TypeCd
        {
            get => GetProperty(TypeCd_Property);
            set => SetProperty(TypeCd_Property, value);
        }

        public static readonly PropertyInfo<string> TypeTxt_Property = RegisterProperty<string>(c => c.TypeTxt);
        [Required]
        public string TypeTxt
        {
            get => GetProperty(TypeTxt_Property);
            set => SetProperty(TypeTxt_Property, value);
        }

        public static readonly PropertyInfo<string> DescTxt_Property = RegisterProperty<string>(c => c.DescTxt);
        public string DescTxt
        {
            get => GetProperty(DescTxt_Property);
            set => SetProperty(DescTxt_Property, value);
        }

        public void FromDto(D_L_EFFORT_TYPE dto)
        {
            TypeCd  = dto.typeCd;
            TypeTxt = dto.typeTxt;
            DescTxt = dto.descTxt;

            base.FromDto(dto);
        }

        public D_L_EFFORT_TYPE ToDto()
        {
            D_L_EFFORT_TYPE dto = new D_L_EFFORT_TYPE
            {
                typeCd  = TypeCd,
                typeTxt = TypeTxt,
                descTxt = DescTxt
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
        protected void DataPortal_Create(L_EffortType_ItemCriteria aKey)
        {
            base.DataPortal_Create();

            if (aKey != null)
            {
                TypeTxt = aKey.TypeCd;
            }
        }

        [CreateChild]
        protected override void Child_Create()
        {
            base.Child_Create();
        }

        [Fetch]
        private void DataPortal_Fetch(L_EffortType_ItemCriteria aKey)
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_L_EFFORT_TYPE>();
                var data = dal.SelectItem(aKey.ToDto());

                using (BypassPropertyChecks) { FromDto(data); }

                BusinessRules.CheckRules();
            }
        }

        [FetchChild]
        private void Child_Fetch(D_L_EFFORT_TYPE dto) { FromDto(dto); }

        [Insert]
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_L_EFFORT_TYPE>();
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
                var dal = dalManager.GetProvider<I_L_EFFORT_TYPE>();
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

                var dal = dalManager.GetProvider<I_L_EFFORT_TYPE>();
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

                var dal = dalManager.GetProvider<I_L_EFFORT_TYPE>();
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
                var dal = dalManager.GetProvider<I_L_EFFORT_TYPE>();

                dal.DeleteItem(new K_L_EFFORT_TYPE { objectID = this.ObjectID });
            }
        }

        [DeleteSelfChild]
        private void Child_DeleteSelf()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                FieldManager.UpdateChildren(this);

                var dal = dalManager.GetProvider<I_L_EFFORT_TYPE>();
                using (BypassPropertyChecks)
                {
                    dal.DeleteItem(new K_L_EFFORT_TYPE { objectID = this.ObjectID });
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// Unit of Work Getter
    /// </summary>
    [Serializable]
    public class L_EffortType_EditItem_Getter : EditItem_Getter_Base<L_EffortType_EditItem, L_EffortType_ItemCriteria>
    {
        #region DataPortal

        [Fetch]
        protected override void DataPortal_Fetch(L_EffortType_ItemCriteria aCriteria)
        {
            if (aCriteria.HasKey)
                EditItem = L_EffortType_EditItem.GetItem(aCriteria);
            else
                EditItem = L_EffortType_EditItem.NewItem(aCriteria);
        }

        #endregion
    }

    /// <summary>
    /// Editable List
    /// </summary>
    [Serializable]
    public class L_EffortType_EditList : EditList_Base<L_EffortType_EditList, L_EffortType_ListCriteria, L_EffortType_EditItem, L_EffortType_ItemCriteria>
    {
        #region DataPortal

        [Fetch]
        private void DataPortal_Fetch(L_EffortType_ListCriteria aCriteria)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            using (var mgr = DalFactory.GetManager(DalFactory.L_MAIN_SCHEMA_NM))
            {
                var prov = mgr.GetProvider<I_L_EFFORT_TYPE>();
                var list = prov.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<L_EffortType_EditItem>(item));
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
