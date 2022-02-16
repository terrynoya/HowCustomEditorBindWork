using System.Collections;
using System.Collections.Generic;
using HowOdinCustomEditorWork;
using UnityEngine;

public class NoCustomEditorAttributeClass : MonoBehaviour
{
    [Button("HowOdinAttributeWork")]
    public void MyBtnClick()
    {
        Debug.Log("my btn clicked!!");
    }
    
    [Button("btn2")]
    public void Btn2()
    {
        Debug.Log("btn2");
    }
    
}