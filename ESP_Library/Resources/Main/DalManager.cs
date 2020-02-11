using System;
using Microsoft.Extensions.Configuration;
using Csla.Data.EntityFrameworkCore;
using CoreLib.Common;

namespace Library.Resources.Main
{
    internal class DalManager : IDalManager
    {
        public T GetProvider<T>() where T : class
        {
            var lDal  = LibRef.Config.GetValue<string>("Library:DalManagerType:Main");
            var lName = typeof(T).FullName.Replace ("I_L_", lDal + ".L_");
            var lType = Type.GetType (lName);

            if (lType != null)
                return Activator.CreateInstance (lType) as T;
            else
                throw new NotImplementedException (lName);
        }

        //public DbContextManager<SqlServer.CoreEntities> ConnectionManager { get; private set; }

        //public DalManager()
        //{
        //    ConnectionManager = DbContextManager<SqlServer.CoreEntities>.GetManager("CoreEntities");
        //}

        public void Dispose() 
        {
            //ConnectionManager.Dispose();
            //ConnectionManager = null;
        }
    }
}

