using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 进度条脚本（极致简化版）- 固定进度标记+容错范围 判定
/// 核心：走到标记值附近即满足红块显示条件，无复杂逻辑
/// </summary>
public class ProgressBarLoader : MonoBehaviour
{
    [Header("进度条基础组件（必须绑定）")]
    [SerializeField] private RectTransform progressOut; // 进度条外框（拖入自身即可）
    [SerializeField] private Image progressFill;       // 进度条覆盖块（拖入填充Image）
    [Header("加载配置（可视化调整）")]
    [SerializeField] private float loadTotalTime = 5f; // 加载总时长（5秒，变慢方便测试）
    [Header("红块显示判定（核心！简化版）")]
    [SerializeField] private float[] progressMarks = new float[] { 0.2f, 0.5f, 0.8f }; // 进度标记值（20%/50%/80%）
    [SerializeField] private float errorTolerance = 0.15f; // 容错范围（15%，超大范围必命中）
    [Header("内部状态（无需修改）")]
    private RectTransform fillRect;
    private float fillTotalWidth;
    public float CurrentProgress { get; private set; } = 0f; // 实时进度（供Player调用）
    private bool isLoading = false;

    private void Awake()
    {
        // 基础判空（有红色报错直接拖入对应组件）
        if (progressOut == null || progressFill == null)
        {
            Debug.LogError("【进度条】请绑定外框（RectTransform）和覆盖块（Image）！");
            enabled = false;
            return;
        }
        // 初始化组件
        fillRect = progressFill.GetComponent<RectTransform>();
        fillTotalWidth = progressOut.rect.width;
        ResetProgress();
        // 自动启动加载（无需手动调用，彻底避免遗漏）
        StartLoad();
    }

    private void Update()
    {
        if (!isLoading) return;
        // 计算实时进度（0→1）
        CurrentProgress = Mathf.Clamp01(CurrentProgress + Time.deltaTime / loadTotalTime);
        // 同步进度条宽度+透明度（原有加载效果保留）
        fillRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fillTotalWidth * CurrentProgress);
        Color c = progressFill.color;
        c.a = CurrentProgress;
        progressFill.color = c;
        // 加载完成停止
        if (CurrentProgress >= 1f) isLoading = false;
    }

    /// <summary>
    /// 核心判定方法：当前进度是否在「任意标记值±容错」区间内
    /// 供Player脚本调用，返回true即满足红块显示条件
    /// </summary>
    public bool IsProgressReachMark()
    {
        // 基础校验：未加载/无标记值/加载完成 → 不满足
        if (!isLoading || progressMarks == null || progressMarks.Length == 0 || CurrentProgress >= 1f)
        {
            Debug.Log($"【判定失败】加载中：{isLoading} | 当前进度：{CurrentProgress:F2} | 标记值数量：{progressMarks?.Length ?? 0}");
            return false;
        }
        // 遍历标记值，判断是否在「标记值±容错」区间
        foreach (float mark in progressMarks)
        {
            float min = Mathf.Clamp01(mark - errorTolerance); // 限制0~1，避免负数
            float max = Mathf.Clamp01(mark + errorTolerance);
            if (CurrentProgress >= min && CurrentProgress <= max)
            {
                Debug.Log($"✅【判定成功】当前进度：{CurrentProgress:F2} | 命中标记值：{mark:F2} | 判定区间：{min:F2}~{max:F2}");
                return true;
            }
        }
        // 未命中任何标记值
        string marksStr = string.Join(",", progressMarks);
        Debug.Log($"❌【判定失败】当前进度：{CurrentProgress:F2} | 所有标记值：{marksStr} | 容错范围：{errorTolerance}");
        return false;
    }

    // 开始加载（自动调用，无需外部操作）
    public void StartLoad()
    {
        ResetProgress();
        isLoading = true;
        Debug.Log("【进度条】已自动启动加载！");
    }

    // 重置进度条（初始状态：宽0+透明）
    private void ResetProgress()
    {
        CurrentProgress = 0f;
        fillRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
        Color c = progressFill.color;
        c.a = 0;
        progressFill.color = c;
    }
}