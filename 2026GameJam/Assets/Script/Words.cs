using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Words : MonoBehaviour
{
    [SerializeField]public GUIStyle style;
    [SerializeField]private Rect rect;
    [SerializeField]private GUIContent cotent; 
    private void OnGUI()
    {
        GUI.Label(rect, cotent, style);
    }
}
