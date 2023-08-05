using UnityEngine;
using Particles;
using UnityEngine.Audio;

namespace Audio
{
    public class AudioSystem : MonoBehaviour
    {
        public enum SoundEffect
        {
            GrabObj,
            PlaceObj,
            EatenByCrocodile,
            GameOver,
            GoalSpotCrossed,
            NextLevel,
            Drown,
            Error
        }

        [SerializeField] private AudioSource sfxAudioSource;
        [SerializeField] private AudioMixer musicMixer;
        [SerializeField] private AudioMixerGroup gameMixerGroup;

        [SerializeField] private float sfxAudioSourceVolume;

        [Header("AudioClips")]
        [SerializeField] private AudioClip rightFootstepClip;
        [SerializeField] private AudioClip leftFootstepClip;
        [SerializeField] private AudioClip explosionClip;

        [SerializeField] private AudioClip GrabObjClip;
        [SerializeField] private AudioClip PlaceObjClip;
        [SerializeField] private AudioClip eatenByCrocodileClip;
        [SerializeField] private AudioClip gameOverClip;
        [SerializeField] private AudioClip goalSpotCrossedClip;
        [SerializeField] private AudioClip nextLevelClip;
        [SerializeField] private AudioClip drownClip;
        [SerializeField] private AudioClip errorClip;

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
            audioSource.outputAudioMixerGroup = gameMixerGroup;
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

        public void PlaySoundEffect(SoundEffect effect)
        {
            switch (effect)
            {
                case SoundEffect.GrabObj:
                    sfxAudioSource.PlayOneShot(GrabObjClip, sfxAudioSourceVolume);
                    break;
                case SoundEffect.PlaceObj:
                    sfxAudioSource.PlayOneShot(PlaceObjClip, sfxAudioSourceVolume);
                    break;
                case SoundEffect.EatenByCrocodile:
                    sfxAudioSource.PlayOneShot(eatenByCrocodileClip);
                    break;
                case SoundEffect.GameOver:
                    sfxAudioSource.PlayOneShot(gameOverClip);
                    break;
                case SoundEffect.GoalSpotCrossed:
                    sfxAudioSource.PlayOneShot(goalSpotCrossedClip);
                    break;
                case SoundEffect.NextLevel:
                    sfxAudioSource.PlayOneShot(nextLevelClip);
                    break;
                case SoundEffect.Drown: 
                    sfxAudioSource.PlayOneShot(drownClip);
                    break;
                case SoundEffect.Error:
                    sfxAudioSource.PlayOneShot(errorClip);
                    break;
                default: break;
            }
        }
    }
}
