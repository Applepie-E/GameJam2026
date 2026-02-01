using UnityEngine;
using UnityEngine.UI;
// 【新增】引入场景管理命名空间（必须加，否则无法跳转BE场景）
using UnityEngine.SceneManagement;

public class KillWUI_Manager : MonoBehaviour
{
    public static KillWUI_Manager Instance;

    [Header("按顺序绑定：Grid1→Grid2→Grid3")]
    [SerializeField] private Image[] killWGrids;
    private int currentLightIndex = 0; // 0=未亮，1=亮1格，2=亮2格，3=亮3格（满）

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (killWGrids != null && killWGrids.Length != 3)
        {
            Debug.LogError("请按顺序绑定3个红块格子（Grid1→Grid2→Grid3）！");
        }
    }

    public void LightNextKillWGrid()
    {
        if (killWGrids == null || killWGrids.Length != 3 || currentLightIndex >= 3)
        {
            return;
        }

        // 原有逻辑：点亮当前索引的红块
        killWGrids[currentLightIndex].gameObject.SetActive(true);
        currentLightIndex++; // 索引+1，准备下一次点亮

        // ========== 核心新增：判断是否三格全亮，是则跳转到BE场景 ==========
        if (currentLightIndex == 3)
        {
            Debug.Log("三格红块全亮，跳转到BE坏结局场景");
            SceneManager.LoadScene("BE"); // 跳转到BE场景
            ResetKillWGrids(); // 可选：重置红块状态（避免返回Game场景后红块残留）
        }
    }

    // 原有重置方法：隐藏所有红块，索引归0
    public void ResetKillWGrids()
    {
        if (killWGrids == null || killWGrids.Length != 3) return;

        foreach (var grid in killWGrids)
        {
            grid.gameObject.SetActive(false);
        }
        currentLightIndex = 0;
    }
}