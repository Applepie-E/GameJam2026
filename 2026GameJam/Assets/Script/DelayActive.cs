using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayActive : MonoBehaviour
{
    [SerializeField]private GameObject gObj;
    [SerializeField]private float delayTime;

    // Start is called before the first frame update
    void Start()
    {
        delayTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        delayTime-= Time.deltaTime;

        if(delayTime<0)
            gObj.SetActive(true);
    }
}
