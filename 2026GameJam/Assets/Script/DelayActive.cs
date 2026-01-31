using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayActive : MonoBehaviour
{
    private Words word;
    [SerializeField]private float delayTime;
    private float changeSpeed=1;

    // Start is called before the first frame update
    void Start()
    {
        word = GetComponentInChildren<Words>();
        word.style.focused.textColor = new Color(1f, 1f, 1f, 1f);
        delayTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        delayTime-= Time.deltaTime;

        if (delayTime*changeSpeed<0&&delayTime*changeSpeed>-1)
            word.style.focused.textColor = new Color(1f, 1f, 1f, -delayTime * changeSpeed);
    }
}
