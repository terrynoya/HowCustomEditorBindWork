using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Editor;
using UnityEditor;
using UnityEngine;

public class TestEditorWindow : EditorWindow
{
    // private static Type CustomEditorAttributesType;
    // private static Type MonoEditorType;
    // private static FieldInfo CustomEditorAttributesType_CustomEditors;
    // private static FieldInfo CustomEditorAttributesType_CustomMultiEditors;
    // private static FieldInfo MonoEditorType_InspectedType;
    // private static FieldInfo MonoEditorType_InspectorType;
    // private static FieldInfo CustomEditorAttributesType_Initialized;
    // private static MethodInfo CustomEditorAttributesType_Rebuild;
    
    [MenuItem("yaojz/test")]
    public static void Show()
    {
        var win = EditorWindow.CreateInstance<TestEditorWindow>();
        win.Init();
        ((EditorWindow) win).Show();
    }

    private void Init()
    {
        // CustomEditorAttributesType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.CustomEditorAttributes");
        // MonoEditorType = CustomEditorAttributesType.GetNestedType("MonoEditorType", BindingFlags.Public | BindingFlags.NonPublic);
        // CustomEditorAttributesType_Initialized = CustomEditorAttributesType.GetField("s_Initialized", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        // CustomEditorAttributesType_CustomEditors = CustomEditorAttributesType.GetField("kSCustomEditors", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        // CustomEditorAttributesType_CustomMultiEditors = CustomEditorAttributesType.GetField("kSCustomMultiEditors", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        //
        // MonoEditorType_InspectedType = MonoEditorType.GetField("m_InspectedType", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        // MonoEditorType_InspectorType = MonoEditorType.GetField("m_InspectorType", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        // CustomEditorAttributesType_Rebuild = CustomEditorAttributesType.GetMethod("Rebuild",BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        
        //ResetCustomEditors();
        // var customEditors = (IDictionary) CustomEditorAttributesType_CustomEditors.GetValue(null);
        // var customMultiEditors = (IDictionary) CustomEditorAttributesType_CustomMultiEditors.GetValue(null);
        // Debug.Log($"custom editor count:{customEditors.Count}");
        // foreach (var key in customEditors.Keys)
        // {
        //     Debug.Log(key);
        // }
        
        CustomInspectorUtility.ResetCustomEditors();
    }

    public void ClearCustomEditors()
    {
        // ((IDictionary)CustomEditorAttributesType_CustomEditors.GetValue(null)).Clear();
        // ((IDictionary)CustomEditorAttributesType_CustomMultiEditors.GetValue(null)).Clear();
        // var dic = ((IDictionary) CustomEditorAttributesType_CustomEditors.GetValue(null));
        // Debug.Log(dic.Count);
        
        CustomInspectorUtility.ClearCustomEditors();
    }

    public static void ResetCustomEditors()
    {
        // if (UnityVersion.IsVersionOrGreater(2019, 1))
        // {
        //     CustomEditorAttributesType_Rebuild.Invoke(null, null);
        //     CustomEditorAttributesType_Initialized.SetValue(null, true);
        //     return;
        // }
        // CustomEditorAttributesType_Initialized.SetValue(null, false);
        CustomInspectorUtility.ResetCustomEditors();
    }

    // public static void SetCustomEditor(Type inspectedType, Type editorType, bool isFallbackEditor,
    //     bool isEditorForChildClasses, bool isMultiEditor)
    // {
    //     object obj = Activator.CreateInstance(MonoEditorType);
    //     MonoEditorType_InspectedType.SetValue(obj, inspectedType);
    //     MonoEditorType_InspectorType.SetValue(obj, editorType);
    //     AddEntryToDictList((IDictionary) CustomEditorAttributesType_CustomEditors.GetValue(null), obj, inspectedType);
    // }

    private void OnGUI()
    {
        if (GUILayout.Button("clear customEditors"))
        {
            ClearCustomEditors();
        }

        if (GUILayout.Button("rebuild"))
        {
            ResetCustomEditors();
            // var dic = (IDictionary) CustomEditorAttributesType_CustomEditors.GetValue(null);
            // var dic2 = (IDictionary) CustomEditorAttributesType_CustomMultiEditors.GetValue(null);
            // Debug.Log($"custom editor count:{dic.Count}");
            // foreach (var key in dic.Keys)
            // {
            //     Debug.Log(key);
            // }
        }

        if (GUILayout.Button("add custom class"))
        {
            // SetCustomEditor(typeof(NoCustomEditorAttributeClass),typeof(NoClassInspector),false,false,false);
            CustomInspectorUtility.SetCustomEditor(typeof(NoCustomEditorAttributeClass),typeof(NoClassInspector),false,false,false);
        }
    }
    
    // private static void AddEntryToDictList(IDictionary dict, object entry, Type inspectedType)
    // {
    //     IList list;
    //     if (dict.Contains(inspectedType))
    //     {
    //         list = (IList)dict[inspectedType];
    //     }
    //     else
    //     {
    //         list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[]
    //         {
    //             MonoEditorType
    //         }));
    //         dict[inspectedType] = list;
    //     }
    //     list.Insert(0, entry);
    // }
}