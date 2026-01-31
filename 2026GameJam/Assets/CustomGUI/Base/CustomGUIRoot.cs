using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//编辑模式下运行
[ExecuteAlways]
public class CustomGUIRoot : MonoBehaviour
{

    private CustomGUIControl[] allControls;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    private void OnGUI()
    {
        for(int i=0;i<allControls.Length;i++)
        {
        
            
                allControls=this.GetComponentsInChildren<CustomGUIControl>();
            
            //控制绘制并且有序绘制
            allControls[i].DrawGUI();
        }
        Debug.Log("asd");
    }
}
