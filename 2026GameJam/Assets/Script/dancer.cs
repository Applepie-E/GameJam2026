using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum faceDir
{
    right,
    left
}
public class dancer : MonoBehaviour
{
    [SerializeField] private float distance;
    public faceDir LorR;
    public dancer partner;
    public int sign;
    
    // Start is called before the first frame update
    void Start()
    {
        dancer[] ds = transform.parent.GetComponentsInChildren<dancer>();
        partner = new dancer();
        for (int i = 0; i < ds.Length; i++)
        {
            if (ds[i] != this)
                partner = ds[i];
        }
        distance=transform.localPosition.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x < 0) sign = -1;
        else sign = 1;

    }
    public void Face(float s)
    {
        transform.localPosition=new Vector3(s*Mathf.Abs(distance),0,0);
    }

   public bool Direction(dancer partners)
    {
        
        if(partners.distance < 0) return true;
        else return false;
    }
}
