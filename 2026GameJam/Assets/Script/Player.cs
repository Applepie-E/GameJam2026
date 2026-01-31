using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float spaceTime;
    // Start is called before the first frame update
    void Start()
    {
        spaceTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GetComponentInParent<Couple>()?.Quick();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponentInParent<Couple>()?.Slow();
        }
        
    }

    public void KillOrExchangeBySpace()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            spaceTime += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(spaceTime > 0.5f)
            {
                
            }
            else
            {
                
            }

                spaceTime = 0;
        }
    }

}
