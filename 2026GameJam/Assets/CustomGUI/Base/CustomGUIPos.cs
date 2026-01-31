using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_Alignment_Type
{
    Up,
    Down, 
    Left,
    Right,
    Center,
    Left_Up,
    Right_Up,
    Left_Down,
    Right_Down
}
[System.Serializable]
public class CustomGUIPos
{
    //主要实现位置计算，不需要Monobehaviour
    private Rect rPos = new Rect(0, 0, 100, 100);
    //屏幕九宫格对齐
    public E_Alignment_Type screen_Alignment_Type=E_Alignment_Type.Center;
    //空间中心对齐
    public E_Alignment_Type control_Alignment_Type=E_Alignment_Type.Center;
    //偏移位置
    public Vector2 pos;
    //
    public float width = 100;
    public float height = 50;

    //用于计算的中心点变量
    private Vector2 centerPos;
    //计算中性点偏移
    private void CalcCenterPos()
    {
        switch (control_Alignment_Type)
        {
            case E_Alignment_Type.Up:
                centerPos.x = -width / 2;
                centerPos.y = 0;
                break;
            case E_Alignment_Type.Down:
                centerPos.x = -width / 2;
                centerPos.y = -height; 
                break;
            case E_Alignment_Type.Left:
                centerPos.x = 0;
                centerPos.y = -height/2;
                break;
            case E_Alignment_Type.Right:
                centerPos.x = -width;
                centerPos.y = -height/2;
                break;
            case E_Alignment_Type.Center:
                centerPos.x = width/2;
                centerPos.y = -height/2;
                break;
            case E_Alignment_Type.Left_Up:
                centerPos.x = 0;
                centerPos.y = 0;
                break;
            case E_Alignment_Type.Right_Up:
                centerPos.x = -width;
                centerPos.y = 0;
                break;
            case E_Alignment_Type.Left_Down:
                centerPos.x = 0;
                centerPos.y = -height;
                break;
            case E_Alignment_Type.Right_Down:
                centerPos.x = -width;
                centerPos.y = -height;
                break;
        }
    }

    //计算最终相对坐标
    private void CalPos()
    {
        switch (screen_Alignment_Type)
        {
            case E_Alignment_Type.Up:
                rPos.x = Screen.width/2+centerPos.x+pos.x;
                rPos.y = 0+centerPos.y+pos.y;
                break;
            case E_Alignment_Type.Down:
                rPos.x = Screen.width / 2 + centerPos.x + pos.x;
                rPos.y = Screen.height + centerPos.y - pos.y;
                break;
            case E_Alignment_Type.Left:
                rPos.x = centerPos.x + pos.x;
                rPos.y = Screen.height/2  + centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Right:
                rPos.x = Screen.width+centerPos.x - pos.x;
                rPos.y = Screen.height / 2 + centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Center:
                rPos.x = Screen.width /2+ centerPos.x - pos.x;
                rPos.y = Screen.height/2 + centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Left_Up:
                rPos.x=centerPos.x + pos.x;
                rPos.y=centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Right_Up:
                rPos.x = Screen.width+ centerPos.x - pos.x;
                rPos.y = centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Left_Down:
                rPos.x = centerPos.x + pos.x;
                rPos.y = Screen.height+ centerPos.y - pos.y;
                break;
            case E_Alignment_Type.Right_Down:
                rPos.x = Screen.width+ centerPos.x - pos.x;
                rPos.y = Screen.height + centerPos.y - pos.y;
                break;
        }
    }

    public Rect Pos
    {
        get {
            //中心点偏移
            CalcCenterPos();
            //相对屏幕位置
            CalPos();

            rPos.width = width;
            rPos.height = height;
            
            return rPos;
        
        }
    }
    //控件位置=相对屏幕位置+中心点偏移+偏移位置  实现自适应屏幕分辨率

}
