using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 红块计数器+场景跳转核心（单例）
/// 挂载对象：Game场景 → 新建空对象GameManager → 挂载该脚本
/// </summary>
public class RedBlockCounterManager : MonoBehaviour
{
    public static RedBlockCounterManager Instance; // 全局单例

    [Header("红块配置（固定3块，无需修改）")]
    private const int MaxRedBlockNum = 3;
    private int killWCount = 0; // KillW红块计数（长按无敌人/敌人已死）
    private int killACCount = 0; // KillAC红块计数（长按+存活敌人+进度判定成功）

    [Header("场景跳转配置（必须与Build Settings一致）")]
    [SerializeField] private string sceneBE = "BE";   // 条件1：KillW满3
    [SerializeField] private string sceneBE2 = "BE2"; // 条件2：两者均不满3
    [SerializeField] private string sceneHE = "HE";   // 条件3：KillAC满3且KillW不满3

    private void Awake()
    {
        // 单例初始化（确保场景唯一）
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 跨场景保留计数（可选）
            ResetCounter(); // 初始化计数为0
            Debug.Log("[Counter] 单例初始化成功，计数器重置为0");
        }
        else
        {
            Destroy(gameObject); // 避免重复挂载
        }
    }

    #region 对外计数方法（Player调用，计数+红块点亮）
    /// <summary>
    /// KillW红块计数+1（满3直接跳BE）
    /// </summary>
    public void AddKillWCount()
    {
        if (killWCount >= MaxRedBlockNum)
        {
            Debug.LogWarning("[Counter] KillW红块已达上限3块，无法继续计数");
            return;
        }
        killWCount++;
        Debug.Log($"[Counter] KillW红块计数+1，当前：{killWCount}/{MaxRedBlockNum}");

        // 条件1：KillW满3，直接跳转BE（无需等进度条）
        if (killWCount >= MaxRedBlockNum)
        {
            Debug.Log("[Counter] KillW红块满3，直接跳转BE场景");
            LoadTargetScene(sceneBE);
        }
    }

    /// <summary>
    /// KillAC红块计数+1（最大3块）
    /// </summary>
    public void AddKillACCount()
    {
        if (killACCount >= MaxRedBlockNum)
        {
            Debug.LogWarning("[Counter] KillAC红块已达上限3块，无法继续计数");
            return;
        }
        killACCount++;
        Debug.Log($"[Counter] KillAC红块计数+1，当前：{killACCount}/{MaxRedBlockNum}");
    }
    #endregion

    #region 进度条结尾触发：3条件场景跳转
    public void OnProgressBarComplete()
    {
        Debug.Log($"[Counter] 进度条走到结尾，开始跳转判断 | KillW：{killWCount}/3 | KillAC：{killACCount}/3");
        // 条件1：KillW满3（双重校验，防止中途计数满3未跳转）
        if (killWCount >= MaxRedBlockNum)
        {
            LoadTargetScene(sceneBE);
        }
        // 条件3：KillAC满3，KillW不满3
        else if (killACCount >= MaxRedBlockNum)
        {
            LoadTargetScene(sceneHE);
        }
        // 条件2：两者均不满3
        else
        {
            LoadTargetScene(sceneBE2);
        }
    }
    #endregion

    #region 工具方法：场景加载+容错
    /// <summary>
    /// 加载场景（检查Build Settings，避免跳转失败）
    /// </summary>
    private void LoadTargetScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("[Counter] 场景名称为空，无法跳转");
            return;
        }
        if (!IsSceneInBuild(sceneName))
        {
            Debug.LogError($"[Counter] 场景{sceneName}未添加到Build Settings！请添加后再试");
            return;
        }
        SceneManager.LoadScene(sceneName);
        Debug.Log($"[Counter] 开始加载场景：{sceneName}");
    }

    /// <summary>
    /// 检查场景是否在Build Settings中
    /// </summary>
    private bool IsSceneInBuild(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            Scene scene = SceneManager.GetSceneByBuildIndex(i);
            string scenePath = scene.path;
            if (Path.GetFileNameWithoutExtension(scenePath).Equals(sceneName))
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 重置计数器（重新开始游戏时调用，可选）
    /// </summary>
    public void ResetCounter()
    {
        killWCount = 0;
        killACCount = 0;
    }
    #endregion
}