using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject targetMask;
    public bool isliving;
    private SpriteRenderer sr;
    [SerializeField] private Sprite deadSprite;
    // Start is called before the first frame update
    void Start()
    {
        isliving = true;
        sr=GetComponent<SpriteRenderer>();
    }

    
    public void Dead()
    {
        sr.sprite = deadSprite;
        isliving = false;
        targetMask.SetActive(true);
    }
}
