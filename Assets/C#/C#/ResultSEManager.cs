using UnityEngine;

public class ResultSEManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip retrySE;
    public AudioClip normalSE;
    public AudioClip highSE;

    public void PlayResultSE(int score)
    {
        if (audioSource == null) return;

        if (score <= 33)
        {
            audioSource.PlayOneShot(retrySE);
        }
        else if (score <= 67)
        {
            audioSource.PlayOneShot(normalSE);
        }
        else
        {
            audioSource.PlayOneShot(highSE);
        }
    }
}
