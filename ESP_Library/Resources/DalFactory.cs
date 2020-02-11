using System;
using System.Collections.Generic;

namespace Library.Resources
{
    internal interface IDalManager : IDisposable
    {
        T GetProvider<T>() where T : class;
    }

    internal class DalFactory
    {
        public const string L_MAIN_SCHEMA_NM  = "Main";

        private const string MANAGER_TYPE_NAME = "Library.Resources.{0}.DalManager, Library";

        private static Dictionary<string, Type> _dalTypes = new Dictionary<string, Type>();

        public static IDalManager GetManager (string aSchemaNm)
        {
            if (! _dalTypes.ContainsKey (aSchemaNm))
            {
                string lName = string.Format (MANAGER_TYPE_NAME, aSchemaNm);
                Type   lType = Type.GetType (lName);

                if (lType == null)
                    throw new ArgumentException (string.Format ("Resource Type {0} not found", lName));

                _dalTypes.Add (aSchemaNm, lType);
            }
            return (IDalManager) Activator.CreateInstance (_dalTypes[aSchemaNm]);
        }
    }
}
