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

// ... 既存の変数の後に追加
private List<SwanFish> allFishes = new List<SwanFish>();

// 魚が生成された時に自分を登録する窓口
public void RegisterFish(SwanFish fish)
{
    allFishes.Add(fish);
}

// --- 既存のリストに追加 ---
private List<BlackBass> allBlackBasses = new List<BlackBass>(); // ブラックバス用リスト

// ブラックバスが生成された時に登録する窓口
public void RegisterBlackBass(BlackBass bass)
{
    allBlackBasses.Add(bass);
}

void CalculateTotalPoints()
{
    allLotuses.RemoveAll(l => l == null);
    allDeadRenkon.RemoveAll(r => r == null);
    allFishes.RemoveAll(f => f == null);
    allBlackBasses.RemoveAll(b => b == null); // 削除されたバスを掃除

    // 各要素のカウント
    int livingLotusCount = 0;
    foreach (var lotus in allLotuses)
    {
        if (!lotus.isWithered) livingLotusCount++;
    }
    int witheredLotusCount = allLotuses.Count - livingLotusCount;
    int deadRenkonCount = allDeadRenkon.Count;
    int fishCount = allFishes.Count;
    int blackBassCount = allBlackBasses.Count; // ブラックバスの数

    // 【最新のポイント計算ルール】
    // 生きているハス: +5pt
    // 枯れたハス: -5pt
    // 枯れたレンコン: -5pt
    // 魚(SwanFish): +5pt
    // ブラックバス: -2pt (★追加★)
    
    totalPoints = (livingLotusCount * 5) 
                + (witheredLotusCount * -5) 
                + (deadRenkonCount * -5) 
                + (fishCount * 5) 
                + (blackBassCount * -2); // ブラックバスはマイナス
}

}