using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

public class Couple : MonoBehaviour
{
    [SerializeField] public float orginSpeed;
    [SerializeField]public float rotateSpeed;
    [SerializeField]private float aS;
    // Start is called before the first frame update
    void Start()
    {
        orginSpeed = 125;
        aS = 50;
        rotateSpeed = orginSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * rotateSpeed);
    }
    public void Quick()
    {
        rotateSpeed += aS;
        if (Mathf.Abs(rotateSpeed) > 375) { rotateSpeed = 375; }
        Velocity();
    }
    public void Slow()
    {
        rotateSpeed -=  aS;
        if (Mathf.Abs(rotateSpeed) > 375) { rotateSpeed = -375; }
        Velocity();
    }
    public void leave()
    {
        orginSpeed += 25;
        rotateSpeed = orginSpeed;
    }
    public void Velocity()
    {
        
       
        if ((rotateSpeed > 0) && (rotateSpeed) < 65) { rotateSpeed = -75; }
        if ((rotateSpeed < 0) &&(rotateSpeed>-65)) { rotateSpeed = rotateSpeed = 75; }
    }
}
