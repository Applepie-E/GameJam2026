using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum E_Style_OnOff
{
    On,
    Off
}

public abstract class CustomGUIControl : MonoBehaviour
{
    //控件共同表现
    //位置
    public CustomGUIPos guiPos;
    //内容
    public GUIContent content;
    //自定义样式
    public GUIStyle style;
    //自定义开关
    public E_Style_OnOff styleOnOff=E_Style_OnOff.Off;

    public void DrawGUI()
    {
        Debug.Log("as");
        switch (styleOnOff)
        {
            case E_Style_OnOff.On:
                StyleOnDraw();
                break;
            case E_Style_OnOff.Off:
                StyleOffDraw();
                break;
        }
    }

    protected abstract void StyleOnDraw();

    protected abstract void StyleOffDraw();
    
}
