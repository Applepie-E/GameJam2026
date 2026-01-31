using UnityEngine;
using UnityEngine.UI; // 引入UI命名空间，操作Text组件

// 文字淡入+放大脚本，直接挂载到目标Text对象上
public class TextFadeInScale : MonoBehaviour
{
    [Header("动画配置（可直接在面板调整）")]
    [Tooltip("从开始到完成的过渡时间，单位：秒（建议1-2秒，小且自然）")]
    public float fadeScaleDuration = 1.5f; // 过渡时长，默认1.5秒，可自定义

    [Header("内部状态（无需手动修改）")]
    private Text happyEndText; // 目标文字组件
    private Color targetColor; // 文字目标颜色（不透明，保留原有RGB）
    private Vector3 targetScale; // 文字目标缩放（正常大小，Scale=1,1,1）
    private float startTime; // 动画开始时间（场景加载时的时间）

    // 场景加载时执行一次，初始化参数
    private void Start()
    {
        // 获取当前对象的Text组件（脚本直接挂在Text上，无需手动绑定）
        happyEndText = GetComponent<Text>();
        // 记录目标状态：缩放为1（正常大小）、颜色为原有RGB+不透明（Alpha=1）
        targetScale = new Vector3(1, 1, 1);
        targetColor = happyEndText.color;
        targetColor.a = 1; // 仅将透明度设为1，保留原有文字颜色
        // 记录动画开始时间（场景进入的时间）
        startTime = Time.timeSinceLevelLoad;
    }

    // 每一帧执行，更新缩放和透明度
    private void Update()
    {
        // 计算当前动画进度：(当前时间-开始时间)/过渡时长 → 范围0→1
        float progress = (Time.timeSinceLevelLoad - startTime) / fadeScaleDuration;
        // 限制进度在0~1之间，避免超出后继续变化
        progress = Mathf.Clamp01(progress);

        // 1. 平滑插值更新缩放：从初始0→目标1
        transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, progress);
        // 2. 平滑插值更新颜色（仅透明度变化）：从初始0→目标1
        Color currentColor = happyEndText.color;
        currentColor.a = Mathf.Lerp(0, 1, progress);
        happyEndText.color = currentColor;

        // 进度达到1时，动画完成，销毁脚本（避免后续无效执行）
        if (progress >= 1)
        {
            Destroy(this);
        }
    }
}