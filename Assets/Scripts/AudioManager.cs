using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip gameOverClip;
    public static AudioManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameResumed += GameManager_OnGameResumed;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
    }
    public void PlayExplosionClip()
    {
        PlaySoundClip(explosionClip, 0.5f);
    }
    public void PlayGameOverClip()
    {
        PlaySoundClip(gameOverClip, 1f);
    }
    public void PlaySoundClip(AudioClip clip, float volume = 0.5f)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
    }
    public void PlayBackgroundMusic()
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.volume = 1f;
        audioSource.Play();
    }

    private void GameManager_OnGameStarted()
    {
        PlayBackgroundMusic();
    }
    private void GameManager_OnGamePaused()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Pause();
        }
    }
    private void GameManager_OnGameResumed()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.UnPause();
        }
    }
    private void GameManager_OnGameOver()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
