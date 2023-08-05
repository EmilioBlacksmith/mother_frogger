using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMusicSystem : MonoBehaviour
{
    public static MainMusicSystem Instance { get; private set; }
    public AudioSource AudioSource { get; private set; }

    [SerializeField] private AudioMixer gameAudioMixer;
    [SerializeField] private Sprite mutedSprite;
    [SerializeField] private Sprite unmutedSprite;

    private bool isMuted = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
        AudioSource = this.gameObject.GetComponent<AudioSource>();
    }

    private void Mute(Image imageButton)
    {
        if (isMuted)
        {
            isMuted = false;
            gameAudioMixer.SetFloat("mainVolume", 0f);
            imageButton.sprite = mutedSprite;
        }
        else
        {
            isMuted = true;
            gameAudioMixer.SetFloat("mainVolume", -80f);
            imageButton.sprite = unmutedSprite;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }

    private void OnLevelLoaded(Scene currentScene, LoadSceneMode loadSceneMode)
    {
        if(currentScene.buildIndex == 0)
        {
            GameObject muteButton = GameObject.FindGameObjectWithTag("Mute Button");
            if (muteButton != null)
            {
                Button button = muteButton.GetComponent<Button>();
                Image imageButton = button.GetComponent<Image>();
                button.onClick.AddListener(() => Mute(imageButton));

                if (isMuted)
                    imageButton.sprite = unmutedSprite;
                else
                    imageButton.sprite = mutedSprite;
            }
        }
    }
}
