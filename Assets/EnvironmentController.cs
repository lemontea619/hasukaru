using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnvironmentController : MonoBehaviour
{
    [Header("UI設定")]
    public Image fillImage; 
    private float currentEnvironmentValue = 0.0f; 

    [Header("ポイント設定")]
    public int maxPointsFor100Percent = 100;
    public int totalPoints = 0;

    [Header("見た目の設定（省略可）")]
    public SpriteRenderer groundSpriteRenderer;
    public Sprite cleanGroundSprite;
    public SpriteRenderer swampRenderer;

    // 管理リスト
    private List<hasu_flower> allLotuses = new List<hasu_flower>();
    private List<GameObject> allDeadRenkon = new List<GameObject>(); // 枯れたレンコン用

    public void RegisterLotus(hasu_flower lotus) => allLotuses.Add(lotus);
    
    // 枯れたレンコンが生成されたらこれを呼ぶ
    public void RegisterDeadRenkon(GameObject renkon) => allDeadRenkon.Add(renkon);

    void Update()
    {
        CalculateTotalPoints();
        
        currentEnvironmentValue = (float)totalPoints / maxPointsFor100Percent;
        currentEnvironmentValue = Mathf.Clamp01(currentEnvironmentValue);
        
        if (fillImage != null) fillImage.fillAmount = currentEnvironmentValue;
        // （沼の色変化などのUpdateVisuals処理は前回同様）
    }

    void CalculateTotalPoints()
    {
        allLotuses.RemoveAll(l => l == null);
        allDeadRenkon.RemoveAll(r => r == null);

        int livingLotusCount = 0;
        int witheredLotusCount = 0;
        int deadRenkonCount = allDeadRenkon.Count;

        foreach (var lotus in allLotuses)
        {
            if (lotus.isWithered) witheredLotusCount++;
            else livingLotusCount++;
        }

        // ポイント計算ルール
        // 生きているハス: +5pt
        // 枯れたハス: -5pt
        // 枯れたレンコン: -5pt
        totalPoints = (livingLotusCount * 5) + (witheredLotusCount * -5) + (deadRenkonCount * -5);
    }
}