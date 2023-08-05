using UnityEngine;
using Particles;
using UnityEngine.Audio;

namespace Audio
{
    public class AudioSystem : MonoBehaviour
    {
        [SerializeField] private AudioSource sfxAudioSource;
        [SerializeField] private AudioMixer musicMixer;

        [SerializeField] private float sfxAudioSourceVolume;

        [Header("AudioClips")]
        [SerializeField] private AudioClip rightFootstepClip;
        [SerializeField] private AudioClip leftFootstepClip;
        [SerializeField] private AudioClip explosionClip;

        public static AudioSystem Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public void PlayFootstep(ParticleSpawningSystem.FootUsed footUsed)
        {
            switch (footUsed)
            {
                case ParticleSpawningSystem.FootUsed.Left:
                    sfxAudioSource.PlayOneShot(leftFootstepClip, sfxAudioSourceVolume);
                    break;
                case ParticleSpawningSystem.FootUsed.Right:
                    sfxAudioSource.PlayOneShot(rightFootstepClip, sfxAudioSourceVolume);
                    break;
                default: break;
            }
        }

        public void PlayCrash(Transform positionOfExplosion)
        {
            GameObject emptyObj = new GameObject();
            emptyObj.name = "(temporal)Collision AudioSrc";
            AudioSource audioSource = emptyObj.AddComponent<AudioSource>();
            emptyObj.transform.position = positionOfExplosion.position;
            audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 1f;
            audioSource.minDistance = 5f;
            audioSource.clip = explosionClip;
            audioSource.Play();
            Destroy(emptyObj, 3f);
        }

        public void NextLevelAudioChange(int level)
        {
            float currentPitch = (float)(1f + (.15f * level));
            musicMixer.SetFloat("pitchBend", 1f / currentPitch);
            MainMusicSystem.Instance.AudioSource.pitch = currentPitch;
        }

        public void ResetMainAudioSpeed()
        {
            musicMixer.SetFloat("pitchBend", 1f);
            MainMusicSystem.Instance.AudioSource.pitch = 1f;
        }
    }
}
