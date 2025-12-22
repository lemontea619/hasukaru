using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnvironmentController : MonoBehaviour
{
    [Header("UI設定")]
    public Image fillImage; 
    private float currentEnvironmentValue = 0.0f; 

    [Header("ポイント設定")]
    public int totalPoints = 0;
    public int maxPointsFor100Percent = 100; // 何ptで満タンか

    [Header("地面の切り替え設定")]
    public SpriteRenderer groundSpriteRenderer;
    public Sprite roughGroundSprite;
    public Sprite cleanGroundSprite;
    public float cleanGroundThreshold = 0.9f;
    private bool isGroundClean = false;

    [Header("沼の色設定")]
    public SpriteRenderer swampRenderer; 
    public Color roughSwampColor = new Color(0.48f, 0.58f, 0.47f, 0.5f);
    public Color cleanSwampColor = new Color(0.62f, 0.86f, 0.96f, 0.5f);
    public float colorTransitionThreshold = 0.9f;

    // シーン内のハスを管理するリスト
    private List<hasu_flower> allLotuses = new List<hasu_flower>();

    void Start()
    {
        if (groundSpriteRenderer != null) groundSpriteRenderer.sprite = roughGroundSprite;
    }

    // ハスが生成された時に自分を登録する窓口
    public void RegisterLotus(hasu_flower lotus)
    {
        allLotuses.Add(lotus);
    }

    void Update()
    {
        // 1. ポイントを計算
        CalculateTotalPoints();

        // 2. 環境値(0.0~1.0)を更新
        currentEnvironmentValue = (float)totalPoints / maxPointsFor100Percent;
        currentEnvironmentValue = Mathf.Clamp01(currentEnvironmentValue);

        // 3. 見た目に反映
        UpdateVisuals();
    }

    void CalculateTotalPoints()
    {
        // 削除されたハス（Destroyされたもの）をリストから掃除
        allLotuses.RemoveAll(l => l == null);

        int livingCount = 0;
        foreach (var lotus in allLotuses)
        {
            // 「生きている（枯れていない）」ハスだけを数える
            if (!lotus.isWithered) 
            {
                livingCount++;
            }
        }
        // 1つにつき5ポイント
        totalPoints = livingCount * 5;
    }

    void UpdateVisuals()
    {
        if (fillImage != null) fillImage.fillAmount = currentEnvironmentValue;

        // 地面の切り替え
        if (currentEnvironmentValue >= cleanGroundThreshold && !isGroundClean)
        {
            if (groundSpriteRenderer != null && cleanGroundSprite != null)
            {
                groundSpriteRenderer.sprite = cleanGroundSprite;
                isGroundClean = true;
            }
        }

        // 沼の色（グラデーション）
        if (swampRenderer != null)
        {
            float lerpRate = Mathf.Clamp01(currentEnvironmentValue / colorTransitionThreshold);
            swampRenderer.color = Color.Lerp(roughSwampColor, cleanSwampColor, lerpRate);
        }
    }
}