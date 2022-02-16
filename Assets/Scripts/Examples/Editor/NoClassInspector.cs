using System.Reflection;
using HowOdinCustomEditorWork;
using UnityEngine;

namespace Editor
{
    public class NoClassInspector:UnityEditor.Editor
    {
        
        public override void OnInspectorGUI()
        {
            // Debug.Log($"target:{this.target}");
            base.OnInspectorGUI();

            var type = target.GetType();
            var methods =  type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            foreach (var method in methods)
            {
                var attr = method.GetCustomAttribute<ButtonAttribute>();
                if (attr != null)
                {
                    if (GUILayout.Button(attr.Text))
                    {
                        method.Invoke(target,null);
                    }                    
                }
            }
            
            // if (GUILayout.Button("aa"))
            // {
            //     
            // }
        }
    }
}