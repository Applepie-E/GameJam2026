using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

public class Couple : MonoBehaviour
{
    [SerializeField]private float rotateSpeed;
    [SerializeField]private float aS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * rotateSpeed);
    }
    public void Quick()
    {
        rotateSpeed += aS;
    }
    public void Slow()
    {
        rotateSpeed -=  aS;
    }
}
