using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 右侧边界遮罩管理：KillAC触发激活，透明度可面板调节
/// </summary>
public class MaskUIManager : MonoBehaviour
{
    public static MaskUIManager Instance;
    [SerializeField] private Image maskImage;
    [Range(0f, 1f)][SerializeField] private float maskAlpha = 0.5f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (maskImage == null)
        {
            Debug.LogError("MaskUIManager：请绑定maskImage！");
            enabled = false;
            return;
        }

        SetMaskAlpha(maskAlpha);
        maskImage.gameObject.SetActive(false);
    }

    public void SetMaskAlpha(float alpha)
    {
        if (maskImage == null) return;
        Color c = maskImage.color;
        c.a = Mathf.Clamp01(alpha);
        maskImage.color = c;
    }

    public void ActivateMask()
    {
        if (maskImage == null) return;
        maskImage.gameObject.SetActive(true);
        Debug.Log("MaskUIManager：右侧遮罩激活，底图暗淡显示");
    }

    public void CloseMask()
    {
        if (maskImage == null) return;
        maskImage.gameObject.SetActive(false);
    }
}