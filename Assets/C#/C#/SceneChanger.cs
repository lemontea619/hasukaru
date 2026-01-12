using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    string nextSceneName;

    // 既存：即シーン移動
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // 追加：クリック音を鳴らしてから移動
    public void ChangeSceneWithSE(string sceneName)
    {
        nextSceneName = sceneName;

        // クリック音を鳴らす
        FindObjectOfType<SEManager>().PlayClick();

        // 少し待ってからシーン移動
        Invoke(nameof(LoadScene), 0.15f);
    }

     public void ChangeSceneWithBackSE(string sceneName)
    {
        nextSceneName = sceneName;
        FindObjectOfType<SEManager>().PlayBack();
        Invoke(nameof(LoadScene), 0.15f);
    }


    void LoadScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
