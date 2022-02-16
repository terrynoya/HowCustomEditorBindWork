using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MyClass))]
public class MyClassInspector :UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("btn"))
        {
            OnBtnClick();
        }
    }

    public void OnBtnClick()
    {
        Debug.Log("my class btn clicked");
    }
}
