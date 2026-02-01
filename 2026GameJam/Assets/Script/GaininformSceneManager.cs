using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GaininformSceneManager : MonoBehaviour
{
    public Image firstImage;  // 第一张图片
    public Image secondImage; // 第二张图片

    private bool isFirstImageVisible = false;
    private bool isSecondImageVisible = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!isFirstImageVisible && !isSecondImageVisible)
            {
                // 显示第一张图片
                firstImage.gameObject.SetActive(true);
                isFirstImageVisible = true;
            }
            else if (isFirstImageVisible && !isSecondImageVisible)
            {
                // 隐藏第一张图片，显示第二张图片
                firstImage.gameObject.SetActive(false);
                secondImage.gameObject.SetActive(true);
                isFirstImageVisible = false;
                isSecondImageVisible = true;
            }
            else if (isSecondImageVisible)
            {
                // 跳转到Game场景
                SceneManager.LoadScene("Game");
            }
        }
    }
}
