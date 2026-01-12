using UnityEngine;

public class ResultDisplay : MonoBehaviour
{
    public GameObject retryEnd;
    public GameObject normalEnd;
    public GameObject highEnd;

    void Start()
    {
        // シーンが始まった瞬間に自動で判定を開始
        ShowResult();
    }

    public void ShowResult()
    {
        // ★共有の箱(static)からスコアを取り出す
        float envScore = EnvironmentController.finalEnvScore;
        float swanScore = EnvironmentController.finalSwanScore;
        float totalAverage = (envScore + swanScore) / 2f;

        retryEnd.SetActive(false);
        normalEnd.SetActive(false);
        highEnd.SetActive(false);

        if (totalAverage <= 33f) retryEnd.SetActive(true);
        else if (totalAverage <= 67f) normalEnd.SetActive(true);
        else highEnd.SetActive(true);
    }
}