using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 交换成功红块管理单例脚本（独立于进度条）
/// 功能：逐格点亮3个红块，满3格后停止，支持重置
/// </summary>
public class ExchangeRedBlockManager : MonoBehaviour
{
    // 单例实例：外部通过ExchangeRedBlockManager.Instance调用
    public static ExchangeRedBlockManager Instance;

    [Header("按顺序绑定3个红块（Grid1→Grid2→Grid3）")]
    [SerializeField] private Image[] redBlocks; // 3个红块数组（必须按左到右顺序绑定）
    private int currentLightIndex = 0; // 当前要点亮的红块索引（初始0，满3则停止）

    private void Awake()
    {
        // 单例核心逻辑：确保全局唯一
        if (Instance == null)
        {
            Instance = this;
            // 可选：跨场景保留红块UI（根据需求选择是否保留）
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 初始化检查
        if (redBlocks == null || redBlocks.Length != 3)
        {
            Debug.LogError("请按顺序绑定3个红块（Grid1→Grid2→Grid3）！");
            enabled = false;
            return;
        }

        // 确保所有红块初始隐藏
        ResetAllRedBlocks();
    }

    // 核心公开方法：点亮下一个红块（逐格，供Player脚本调用）
    public void LightNextRedBlock()
    {
        // 前置判断：红块数组异常/已点亮全部3格，直接返回
        if (redBlocks == null || redBlocks.Length != 3 || currentLightIndex >= 3)
        {
            return;
        }

        // 点亮当前索引的红块
        redBlocks[currentLightIndex].gameObject.SetActive(true);
        Debug.Log($"点亮第{currentLightIndex + 1}个交换红块");
        currentLightIndex++; // 索引+1，为下一次点亮做准备
    }

    // 公开方法：重置所有红块（隐藏+索引归0，外部可调用）
    public void ResetAllRedBlocks()
    {
        if (redBlocks == null || redBlocks.Length != 3) return;

        // 隐藏所有红块
        foreach (var block in redBlocks)
        {
            if (block != null)
            {
                block.gameObject.SetActive(false);
            }
        }
        // 索引重置为0，重新开始点亮
        currentLightIndex = 0;
        Debug.Log("交换红块已全部重置");
    }
}