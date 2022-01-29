using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Service.Utility
{
    public class String<T>
    {
        private static String<T> stringClass;
        protected String()
        {

        }
        private static void CreateInstance()
        {
            if (stringClass == null)
                stringClass = new String<T>();
        }
        public static String<T> GetInstance()
        {
            if (stringClass == null)
                CreateInstance();
            return stringClass;
        }
        public string GetPropertiesAsString(T Entity)
        {
            var props = Entity.GetType().GetProperties();
            string[] parameters = new string[props.Count()];
            int i = 0;
            foreach(var item in props)
            {
                parameters[i] = item.GetValue(Entity, null) != null ? item.GetValue(Entity, null).ToString() : "";
                i++;
            }
            string result = String.Concat(" "+parameters+" ");
            return result;
        }
    }
}
