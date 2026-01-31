using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class Words : MonoBehaviour
{
    [SerializeField]private GUIStyle style;
    [SerializeField]private Rect rect;
    [SerializeField]private GUIContent cotent;
    private void OnGUI()
    {
        GUI.Label(rect, cotent, style);
    }
}
