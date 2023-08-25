using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Main_Menu
{
    [RequireComponent(typeof(AudioSource))]
    public class MainMenuFunctionality : MonoBehaviour
    {
        [SerializeField] private int gameLevelSceneIndex = 1;
        [SerializeField] private AudioSource sfxAudioSource;

        [Header("Menu SFX")]
        [SerializeField] private AudioClip buttonPressSFX;
        [SerializeField] private AudioClip logoPressSFX;
        [SerializeField] private float sfxAudioVolume = 1f;

        private void Awake()
        {
            sfxAudioSource = GetComponent<AudioSource>();
        }

        public void StartGame() => SceneManager.LoadSceneAsync(gameLevelSceneIndex, LoadSceneMode.Single);

        public void ExitGame() => Application.Quit();

        public void ButtonPressSFX() => sfxAudioSource.PlayOneShot(buttonPressSFX, sfxAudioVolume);
        
        public void LogoPressSFX() => sfxAudioSource.PlayOneShot(logoPressSFX, sfxAudioVolume);
    }
}
