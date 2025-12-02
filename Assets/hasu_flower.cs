using UnityEngine;

public class hasu_flower : MonoBehaviour
{
    // OnMouseDown: 2Dコライダーを持つオブジェクトがクリックされた際に自動で呼ばれる
    void OnMouseDown()
    {
        Debug.Log("ハスがクリックされました。");
        
        // 環境メーターの値を増やす処理を呼び出す
        // シーン内の EnvironmentController を探す
        EnvironmentController ec = FindObjectOfType<EnvironmentController>();
        if (ec != null)
        {
            // メーター増加関数を呼び出し
            ec.AddEnvironmentValue();
        }

        // このハスオブジェクトを削除する
        Destroy(gameObject); 
    }
}