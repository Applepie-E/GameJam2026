using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // 跳转到wordsScene场景（原有方法，无需修改）
    public void GotoWordsScene()
    {
        SceneManager.LoadScene("wordsScene");
    }

    // ========== 新增：退出游戏方法 ==========
    public void QuitGame()
    {
        // 打包后真正退出游戏的核心代码
        Application.Quit();

        // 编辑器专属：测试时点击按钮直接停止Play模式（打包后该行代码会自动忽略）
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}