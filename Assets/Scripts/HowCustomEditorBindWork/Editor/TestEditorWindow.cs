using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Editor;
using HowCustomEditorBindWork.Editor;
using UnityEditor;
using UnityEngine;

public class TestEditorWindow : EditorWindow
{
    [MenuItem("yaojz/test")]
    public static void Show()
    {
        var win = EditorWindow.CreateInstance<TestEditorWindow>();
        win.Init();
        ((EditorWindow) win).Show();
    }

    private void Init()
    {
        CustomInspectorUtility.ResetCustomEditors();
    }

    public void ClearCustomEditors()
    {
        CustomInspectorUtility.ClearCustomEditors();
    }

    public static void ResetCustomEditors()
    {
        CustomInspectorUtility.ResetCustomEditors();
    }
    
    private void OnGUI()
    {
        if (GUILayout.Button("clear customEditors"))
        {
            ClearCustomEditors();
        }

        if (GUILayout.Button("rebuild"))
        {
            ResetCustomEditors();
        }

        if (GUILayout.Button("add custom class"))
        {
            CustomInspectorUtility.SetCustomEditor(typeof(NoCustomEditorAttributeClass),typeof(NoClassInspector),false,false,false);
        }

        if (GUILayout.Button("load assembly"))
        {
            AssemblyUtility.Init();
        }
    }
}