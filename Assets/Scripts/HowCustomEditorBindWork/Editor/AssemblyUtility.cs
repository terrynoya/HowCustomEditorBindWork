using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using UnityEngine;

namespace HowCustomEditorBindWork.Editor
{
    public static class AssemblyUtility
    {
        private static string[] userAssemblyPrefixes = new string[]
        {
            "Assembly-CSharp",
            "Assembly-UnityScript",
            "Assembly-Boo",
            "Assembly-CSharp-Editor",
            "Assembly-UnityScript-Editor",
            "Assembly-Boo-Editor"
        };
        
        static AssemblyUtility()
        {
            AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoaded;
        }

        public static void Init()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                GetAssemblyFlag(assembly);    
            }
        }

        public static void GetAssemblyFlag(Assembly assembly)
        {
            string text = assembly.FullName.ToLower(CultureInfo.InvariantCulture);
            if (text.StartsWithAnyOf(userAssemblyPrefixes, StringComparison.InvariantCultureIgnoreCase))
            {
                Debug.Log($"{assembly.FullName} is user assembly");
                foreach (var type in assembly.SafeGetTypes())
                {
                    Debug.Log(type);
                }
            }
            // else
            // {
            //     Debug.Log($"{assembly.FullName}");
            // }
        }
        
        private static bool StartsWithAnyOf(this string str, IEnumerable<string> values, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            IList<string> list = values as IList<string>;
            if (list == null)
            {
                foreach (string value in values)
                {
                    if (str.StartsWith(value, comparisonType))
                    {
                        return true;
                    }
                }
                return false;
            }
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                if (str.StartsWith(list[i], comparisonType))
                {
                    return true;
                }
            }
            return false;
        }

        private static void OnAssemblyLoaded(object sender, AssemblyLoadEventArgs evt)
        {
            //evt.LoadedAssembly
        }
        
        public static Type[] SafeGetTypes(this Assembly assembly)
        {
            Type[] result;
            try
            {
                result = assembly.GetTypes();
            }
            catch
            {
                result = Type.EmptyTypes;
            }
            return result;
        }
    }
}