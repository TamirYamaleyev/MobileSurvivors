using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        float savedVol = PlayerPrefs.GetFloat("BGMVolume", 1f); // default 1
        SetVolume(savedVol);
    }

    public void Play()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    public void Stop()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }

    public void SetVolume(float vol)
    {
        audioSource.volume = Mathf.Clamp01(vol);
        PlayerPrefs.SetFloat("BGMVolume", audioSource.volume);
        PlayerPrefs.Save();
    }
}
