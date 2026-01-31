using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float spaceTime;
    private Collider2D colliders;
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
        KillOrExchangeBySpace();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        colliders = other;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        colliders = null;
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
                if(colliders!= null)
                {
                    Transform transformMid= colliders.transform.parent;
                    Vector3 v3 = colliders.transform.position;
                    Quaternion v3r = colliders.transform.rotation;

                    colliders.transform.parent=transform.parent;
                    colliders.transform.position=transform.position;
                    colliders.transform.rotation=transform.rotation;

                    transform.parent=transformMid;
                    transform.position=v3;
                    transform.rotation=v3r;
                }
            }

                spaceTime = 0;
        }
    }

}
