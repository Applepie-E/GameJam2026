using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float spaceTime;
    private Collider2D colliders;
    private float spaceCDTimer;
    [SerializeField]private float spaceCD;
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
        //CD
        spaceCDTimer-= Time.deltaTime;
        if (spaceCDTimer > 0) return;
        //
        if (Input.GetKey(KeyCode.Space))
        {
            spaceTime += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(spaceTime > 0.5f)
            {
                if (colliders != null)
                {
                    Debug.Log("kill");

                    if (colliders.transform.gameObject.CompareTag("Enemy"))
                    {

                        Debug.Log("killAC");
                    }
                    else
                    {
                        Debug.Log("killW");
                    }
                }
            }
            else
            {
                if(colliders!= null)
                {
                    Debug.Log("exchange");
                    Transform transformMid= colliders.transform.parent;
                    Vector3 v3 = colliders.transform.position;
                    Vector3 v3r=colliders.transform.localScale;

                    colliders.transform.parent=transform.parent;
                    colliders.transform.position=transform.position;
                    colliders.transform.localScale=transform.localScale;

                    transform.parent=transformMid;
                    transform.position=v3;
                   transform.localScale=v3r;
                }
            }

                spaceTime = 0;
            spaceCDTimer = spaceCD;
        }
    }

}
