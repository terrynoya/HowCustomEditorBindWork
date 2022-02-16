using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class CustomInspectorUtility
{
    private static Type CustomEditorAttributesType;
    private static Type MonoEditorType;
    private static FieldInfo CustomEditorAttributesType_CustomEditors;
    private static FieldInfo CustomEditorAttributesType_CustomMultiEditors;
    private static FieldInfo MonoEditorType_InspectedType;
    private static FieldInfo MonoEditorType_InspectorType;
    private static FieldInfo CustomEditorAttributesType_Initialized;
    private static MethodInfo CustomEditorAttributesType_Rebuild;
    
    static CustomInspectorUtility()
    {
        CustomEditorAttributesType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.CustomEditorAttributes");
        MonoEditorType = CustomEditorAttributesType.GetNestedType("MonoEditorType", BindingFlags.Public | BindingFlags.NonPublic);
        CustomEditorAttributesType_Initialized = CustomEditorAttributesType.GetField("s_Initialized", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        CustomEditorAttributesType_CustomEditors = CustomEditorAttributesType.GetField("kSCustomEditors", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        CustomEditorAttributesType_CustomMultiEditors = CustomEditorAttributesType.GetField("kSCustomMultiEditors", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        
        MonoEditorType_InspectedType = MonoEditorType.GetField("m_InspectedType", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        MonoEditorType_InspectorType = MonoEditorType.GetField("m_InspectorType", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        CustomEditorAttributesType_Rebuild = CustomEditorAttributesType.GetMethod("Rebuild",BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    }
    
    public static void ClearCustomEditors()
    {
        ((IDictionary)CustomEditorAttributesType_CustomEditors.GetValue(null)).Clear();
        ((IDictionary)CustomEditorAttributesType_CustomMultiEditors.GetValue(null)).Clear();
        var dic = ((IDictionary) CustomEditorAttributesType_CustomEditors.GetValue(null));
        Debug.Log(dic.Count);
    }

    public static void ResetCustomEditors()
    {
        if (UnityVersion.IsVersionOrGreater(2019, 1))
        {
            CustomEditorAttributesType_Rebuild.Invoke(null, null);
            CustomEditorAttributesType_Initialized.SetValue(null, true);
            return;
        }
        CustomEditorAttributesType_Initialized.SetValue(null, false);
    }
    
    public static void SetCustomEditor(Type inspectedType, Type editorType, bool isFallbackEditor,
        bool isEditorForChildClasses, bool isMultiEditor)
    {
        object obj = Activator.CreateInstance(MonoEditorType);
        MonoEditorType_InspectedType.SetValue(obj, inspectedType);
        MonoEditorType_InspectorType.SetValue(obj, editorType);
        AddEntryToDictList((IDictionary) CustomEditorAttributesType_CustomEditors.GetValue(null), obj, inspectedType);
    }
    
    private static void AddEntryToDictList(IDictionary dict, object entry, Type inspectedType)
    {
        IList list;
        if (dict.Contains(inspectedType))
        {
            list = (IList)dict[inspectedType];
        }
        else
        {
            list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[]
            {
                MonoEditorType
            }));
            dict[inspectedType] = list;
        }
        list.Insert(0, entry);
    }
}
