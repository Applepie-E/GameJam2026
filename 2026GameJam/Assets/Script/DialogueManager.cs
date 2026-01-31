using UnityEngine;
using UnityEngine.UI; // 引入UI命名空间，才能操作Text组件

// 对话管理核心脚本，挂载在Canvas上即可
public class DialogueManager : MonoBehaviour
{
    [Header("对话文本配置")]
    public Text dialogueText; // 绑定对话框的Text组件（显示文字）
    public string[] dialogueLines; // 对话文本数组，可在Inspector中输入多段文本

    [Header("内部状态（无需手动修改）")]
    private int currentLineIndex = 0; // 当前显示的文本索引，初始为0（第一段）

    // 场景加载时执行一次，初始化显示第一段文本
    private void Start()
    {
        // 判空：避免未绑定组件/未输入文本时报错
        if (dialogueText != null && dialogueLines != null && dialogueLines.Length > 0)
        {
            ShowCurrentDialogue(); // 显示第一段文本
        }
    }

    // 每一帧执行，检测空格键输入
    private void Update()
    {
        // 检测【空格键按下】（仅按一次触发一次，避免长按连续切换）
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextDialogue(); // 切换到下一段文本
        }
    }

    // 切换下一段文本的核心方法
    private void NextDialogue()
    {
        // 索引+1，指向下一段文本
        currentLineIndex++;

        // 判断：如果索引未超出数组长度，显示当前段；否则清空文本（对话结束）
        if (currentLineIndex < dialogueLines.Length)
        {
            ShowCurrentDialogue();
        }
        else
        {
            dialogueText.text = ""; // 对话全部显示完，清空文本（可自定义为“对话结束”等）
        }
    }

    // 显示当前索引对应的文本（覆盖原有内容，实现“前一段消失”）
    private void ShowCurrentDialogue()
    {
        dialogueText.text = dialogueLines[currentLineIndex]; // 直接替换Text内容，天然实现覆盖
    }
}