using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // 添加UI命名空间

public class WordsDelay : MonoBehaviour
{
    [SerializeField] private List<Text> words; // 使用List<Text>来存储每行文字
    [SerializeField] private float delayTimePerWord = 1f; // 每行文字显示的延迟时间
    [SerializeField] private float fadeInDuration = 1f; // 渐入效果的持续时间

    private int currentIndex = 0; // 当前显示的文字索引
    private float timer = 0f; // 计时器
    private bool isFadingIn = true; // 是否正在渐入

    // Start is called before the first frame update
    void Start()
    {
        if (words == null || words.Count == 0)
        {
            Debug.LogError("No words assigned to the list.");
            return;
        }

        // 初始化所有文字为不可见
        foreach (var word in words)
        {
            word.color = new Color(1f, 1f, 1f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentIndex >= words.Count)
        {   

            SceneManager.LoadScene(""); // 切换场景到
            return; // 如果所有文字都已显示完毕，停止更新
        }

        timer += Time.deltaTime;

        if (isFadingIn)
        {
            // 渐入效果
            float alpha = timer / fadeInDuration;
            alpha = Mathf.Clamp01(alpha); // 确保alpha在0到1之间
            words[currentIndex].color = new Color(1f, 1f, 1f, alpha);

            if (alpha >= 1f)
            {
                timer = 0f;
                isFadingIn = false;
                currentIndex++; // 移动到下一行文字
                // 如果还有更多的文字，开始下一个文字的渐入效果
                if (currentIndex < words.Count)
                {
                    isFadingIn = true;
                }
            }
        }
    }
}