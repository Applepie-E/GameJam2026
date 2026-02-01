using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // 必须保留，场景加载核心命名空间



public class Jump : MonoBehaviour
{
    [Header("延时跳转配置")]
    [SerializeField] private float delayTime = 5f; // 延时时间（秒），面板可直接修改
    private bool canAction;
    public GameObject tip;
    //[SerializeField] private string targetScene = "GameStart"; // 目标场景名，避免硬编码

    // Start is called before the first frame update
    void Start()
    {
        // 启动协程，实现延时加载场景
        canAction = false;
        StartCoroutine(LoadSceneWithDelay());
    }

    // 协程方法：延时指定时间后跳转场景
    private IEnumerator LoadSceneWithDelay()
    {
        // 等待delayTime秒（核心延时逻辑）
        yield return new WaitForSeconds(delayTime);
        // 延时结束后加载目标场景
        //Application.Quit();
        //Debug.Log("Quit");
        canAction = true;
        tip.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (canAction)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene("Game 1");
            if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene("GameStart");
        }
        // 保留原空方法，无需修改
    }
}