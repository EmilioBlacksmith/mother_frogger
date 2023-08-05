using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMusicSystem : MonoBehaviour
{
    public static MainMusicSystem Instance { get; private set; }
    public AudioSource AudioSource { get; private set; }

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
}
