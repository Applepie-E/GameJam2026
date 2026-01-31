//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Player : MonoBehaviour
//{
//    private float spaceTime;
//    private Collider2D colliders;
//    private float spaceCDTimer;
//    [SerializeField]private float spaceCD;
//    // Start is called before the first frame update
//    void Start()
//    {
//        spaceTime = 0;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.A))
//        {
//            GetComponentInParent<Couple>()?.Quick();
//        }
//        if (Input.GetKeyDown(KeyCode.D))
//        {
//            GetComponentInParent<Couple>()?.Slow();
//        }
//        KillOrExchangeBySpace();
//    }


//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        colliders = other;
//    }
//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        colliders = null;
//    }
//    public void KillOrExchangeBySpace()
//    {
//        //CD
//        spaceCDTimer-= Time.deltaTime;
//        if (spaceCDTimer > 0) return;
//        //
//        if (Input.GetKey(KeyCode.Space))
//        {
//            spaceTime += Time.deltaTime;
//        }
//        if (Input.GetKeyUp(KeyCode.Space))
//        {
//            if(spaceTime > 0.5f)
//            {
//                if (colliders != null)
//                {
//                    Debug.Log("kill");

//                    if (colliders.transform.gameObject.CompareTag("Enemy"))
//                    {

//                        Debug.Log("killAC");
//                    }
//                    else
//                    {
//                        Debug.Log("killW");
//                        // ========== 新增：调用UI显示三格红块 ==========
//                        KillWUI_Manager.Instance?.LightNextKillWGrid();
//                    }
//                }
//            }
//            else
//            {
//                if(colliders!= null)
//                {
//                    Debug.Log("exchange");
//                    Transform transformMid= colliders.transform.parent;
//                    Vector3 v3 = colliders.transform.position;
//                    Vector3 v3r=colliders.transform.localScale;

//                    colliders.transform.parent=transform.parent;
//                    colliders.transform.position=transform.position;
//                    colliders.transform.localScale=transform.localScale;

//                    transform.parent=transformMid;
//                    transform.position=v3;
//                   transform.localScale=v3r;
//                }
//            }

//                spaceTime = 0;
//            spaceCDTimer = spaceCD;
//        }
//    }

//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 【新增】引入场景管理命名空间（必须加，否则无法跳转场景）
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // 原有变量、Start、Update、触发检测方法均无需修改
    private float spaceTime;
    private Collider2D colliders;
    private float spaceCDTimer;
    [SerializeField] private float spaceCD;

    void Start()
    {
        spaceTime = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) GetComponentInParent<Couple>()?.Quick();
        if (Input.GetKeyDown(KeyCode.D)) GetComponentInParent<Couple>()?.Slow();
        KillOrExchangeBySpace();
    }

    private void OnTriggerEnter2D(Collider2D other) { colliders = other; }
    private void OnTriggerExit2D(Collider2D collision) { colliders = null; }

    public void KillOrExchangeBySpace()
    {
        spaceCDTimer -= Time.deltaTime;
        if (spaceCDTimer > 0) return;

        if (Input.GetKey(KeyCode.Space)) spaceTime += Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (spaceTime > 0.5f)
            {
                if (colliders != null)
                {
                    Debug.Log("kill");
                    // ========== 核心修改：killAC分支加场景跳转 ==========
                    if (colliders.transform.gameObject.CompareTag("Enemy"))
                    {
                        Debug.Log("killAC");
                        // 直接跳转到Kill场景，无任何红块调用（自然不亮红块）
                        SceneManager.LoadScene("Kill");
                    }
                    else
                    {
                        Debug.Log("killW");
                        // 原有killW分支逻辑：每次触发点亮下一个红块，保留不变
                        KillWUI_Manager.Instance?.LightNextKillWGrid();
                    }
                }
            }
            else
            {
                // 原有交换逻辑，完全保留
                if (colliders != null)
                {
                    Debug.Log("exchange");
                    Transform transformMid = colliders.transform.parent;
                    Vector3 v3 = colliders.transform.position;
                    Vector3 v3r = colliders.transform.localScale;

                    colliders.transform.parent = transform.parent;
                    colliders.transform.position = transform.position;
                    colliders.transform.localScale = transform.localScale;

                    transform.parent = transformMid;
                    transform.position = v3;
                    transform.localScale = v3r;
                }
            }
            spaceTime = 0;
            spaceCDTimer = spaceCD;
        }
    }
}