using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Scaffold.Extensions
{
    public static class MyMapster
    {
        public static bool CloneData<T>(this T target, object source, bool MakeTarget = false)
        {
            var targetHere = (T)Activator.CreateInstance(typeof(T));

            if (!MakeTarget)
            {
                targetHere = target;
            }
            else
            {
                target = targetHere;
            }

            Type objTypeBase = source.GetType();
            Type objTypeTarget = targetHere.GetType();

            PropertyInfo _propinfo = null;
            var propInfos = objTypeBase.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var propInfo in propInfos)
            {
                try
                {
                    _propinfo = objTypeTarget.GetProperty(propInfo.Name, BindingFlags.Instance | BindingFlags.Public);
                    if (_propinfo != null)
                    {
                        _propinfo.SetValue(targetHere, propInfo.GetValue(source));
                    }
                }
                catch (ArgumentException aex) 
                { 
                    if (!string.IsNullOrEmpty(aex.Message)) 
                        continue; 
                }
                catch (Exception ex) 
                {
                    if (!string.IsNullOrEmpty(ex.Message))
                        //return default(T); 
                        return (false);
                }
            }

            return (true);
        }
               
        public static T Clone<T>(T value, Newtonsoft.Json.JsonSerializerSettings settings = null)
        {
            var objectType = value.GetType();
            var cereal = Newtonsoft.Json.JsonConvert.SerializeObject(value, settings);
            return (T)Newtonsoft.Json.JsonConvert.DeserializeObject(cereal, objectType, settings);
        }
    }
}
