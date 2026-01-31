using UnityEngine;
using UnityEngine.UI;
// 【新增】引入场景管理命名空间（必须加，否则无法使用SceneManager跳转场景）
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("对话文本配置")]
    public Text dialogueText;
    public string[] dialogueLines;

    [Header("内部状态（无需手动修改）")]
    private int currentLineIndex = 0;

    private void Start()
    {
        if (dialogueText != null && dialogueLines != null && dialogueLines.Length > 0)
        {
            ShowCurrentDialogue();
        }
    }

    private void Update()
    {
        // 空格键检测逻辑不变，保留原有功能
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextDialogue();
        }
    }

    private void NextDialogue()
    {
        currentLineIndex++;
        // 【修改核心逻辑】：索引未超长度则显示下一段，超了则跳转到Game场景
        if (currentLineIndex < dialogueLines.Length)
        {
            ShowCurrentDialogue(); // 原有逻辑：显示下一段对话
        }
        else
        {
            // 【新增】最后一段显示完，按空格直接跳转到Game场景
            SceneManager.LoadScene("Game");
        }
    }

    private void ShowCurrentDialogue()
    {
        // 原有文本覆盖逻辑不变，实现“前一段消失，后一段显示”
        dialogueText.text = dialogueLines[currentLineIndex];
    }
}