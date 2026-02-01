using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 保留命名空间，无需删除

public class Player : MonoBehaviour
{
    private float spaceTime;
    private Collider2D colliders;
    private float spaceCDTimer;
    [SerializeField] private float spaceCD;
    public UIFrameAnimation ufa;
    public int gentle;
    public int alarm;
    public int killNum;
    void Start()
    {
        spaceTime = 0;
        alarm=0;
        gentle=0;
        killNum=0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) GetComponentInParent<Couple>()?.Quick();
        if (Input.GetKeyDown(KeyCode.D)) GetComponentInParent<Couple>()?.Slow();
        KillOrExchangeBySpace();
        if(ufa!=null)
        Debug.Log("111");
        if ((gentle >= 3) && (killNum >= 3))
            Invoke("EndScene", 3f);
            
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
            if (spaceTime > 0.5f) // 长按空格（>0.5秒）触发Kill/AC
            {
                if (colliders != null)
                {
                    Debug.Log("kill");
                    Enemy enemy = colliders.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        if (enemy.isliving)
                        {
                            // ================ KillAC核心触发代码 ================
                            Debug.Log("KillAC触发，播放4帧动画");
                            if (ufa != null)
                            {
                                ufa.Play();

                            }
                            enemy.Dead();
                            killNum++;
                            // ====================================================
                            MaskUIManager.Instance?.ActivateMask();
                        }
                        else
                        {
                            alarm++;
                            Debug.Log("killW");
                            KillWUI_Manager.Instance?.LightNextKillWGrid();
                        }
                    }
                    else
                    {
                        Debug.Log("killW");
                        alarm++;
                        KillWUI_Manager.Instance?.LightNextKillWGrid();
                    }
                }
            }
            else // 短按空格（<=0.5秒）触发交换
            {
                if (colliders != null)
                {
                    transform.parent.GetComponent<Couple>().leave();
                    Transform transformMid = colliders.transform.parent;
                    colliders.transform.parent = transform.parent;
                    transform.parent = transformMid;

                    faceDir qw = colliders.GetComponent<dancer>().LorR;
                    colliders.GetComponent<dancer>().LorR = GetComponent<dancer>().LorR;
                    GetComponent<dancer>().LorR = qw;
                    GetComponent<dancer>().Face(colliders.GetComponent<dancer>().sign);
                    colliders.GetComponent<dancer>().Face(GetComponent<dancer>().sign);

                    // 交换成功触发红块的代码（保留，不影响KillAC动画）
                    ProgressBarLoader progressBar = FindObjectOfType<ProgressBarLoader>();
                    if (progressBar != null && progressBar.IsProgressReachMark())
                    {
                        gentle++;
                        ExchangeRedBlockManager.Instance?.LightNextRedBlock();
                    }
                }
            }
            // 重置计时和CD，原有逻辑保留
            spaceTime = 0;
            spaceCDTimer = spaceCD;
        }
    }
    public void EndScene()
    {
        SceneManager.LoadScene("HE");
    }
}