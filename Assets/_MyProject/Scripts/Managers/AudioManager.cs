using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public const string ANGRY_CAT = "Angry Cat";
    public const string BONUS_ACTIVATED = "Bonus Activated";
    public const string CAT_SELECT = "Cat Select";
    public const string GAME_OVER = "Game Over";
    public const string GAMEPLAY = "Gameplay_Repetitive";
    public const string ICE_CREAM_COLLECT = "Ice Cream Collect";
    public const string MAIN_THEME_SONG = "Main Theme song";
    public const string VICTORY = "Victory";


    [SerializeField] private List<AudioClip> audios;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            audioSource = GetComponent<AudioSource>();
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        PlayerData.UpdatedMusic += ToggleMusic;
    }

    private void OnDisable()
    {
        PlayerData.UpdatedMusic -= ToggleMusic;
    }

    private void ToggleMusic()
    {
        if (SceneManager.IsDataCollectorScene)
        {
            return;
        }
        if (DataManager.Instance.PlayerData.PlayMusic)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }

    public void PlayBackgroundMusic(string _key)
    {
        if (!DataManager.Instance.PlayerData.PlayMusic)
        {
            return;
        }
        AudioClip _audioClip = audios.Find(_element => _element.name == _key);
        if (_audioClip != null && _audioClip != audioSource.clip)
        {
            audioSource.clip = _audioClip;
            audioSource.Play();
        }
    }

    public void Play(string _key)
    {
        if (!DataManager.Instance.PlayerData.PlaySound)
        {
            return;
        }
        AudioClip _audioClip = audios.Find(_element => _element.name == _key);
        if (_audioClip != null)
        {
            audioSource.PlayOneShot(_audioClip);
        }
    }

}
