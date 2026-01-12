using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBGMManager : MonoBehaviour
{
    public static StartBGMManager Instance;

    public AudioSource audioSource;
    public AudioClip startBGM;

// ★追加: 0.0（無音）から 1.0（最大）の間で設定
    [Range(0f, 1f)] 
    public float volume = 0.2f;

    void Awake()
    {
        // シングルトン
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        audioSource.loop = true;
        audioSource.clip = startBGM;
        audioSource.Play();
    }

    void Update()
    {
        // ResultScene に入ったら止める
        if (SceneManager.GetActiveScene().name == "ResultScene")
        {
            audioSource.Stop();
        }
    }
    
    void OnValidate()
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}
