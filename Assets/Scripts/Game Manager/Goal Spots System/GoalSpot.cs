using System.Collections;
using Audio;
using Character_System.HP_System;
using Particles;
using UnityEngine;

namespace Game_Manager.Goal_Spots_System
{
    public class GoalSpot : MonoBehaviour
    {
        [SerializeField] private Material availableMaterial;
        [SerializeField] private Material notAvailableMaterial;
        [SerializeField] private Renderer spotRenderer;
        [SerializeField] private GameObject insideFrog;
        

        private bool _available = true;
        
        public bool IsAvailable() => _available;
    
        public void RestartGoalSpot()
        {
            _available = true;
            spotRenderer.material = availableMaterial;
            insideFrog.SetActive(false);

        }

        private void OnTriggerEnter(Collider other)
        {
            if ((!other.gameObject.CompareTag("Player") || other.gameObject.name != "Hip")) return;
        
            if (_available)
            {
                ParticleSpawningSystem.Instance.SpawnCrossSpotParticle(other.transform);
                AudioSystem.Instance.PlaySoundEffect(AudioSystem.SoundEffect.GoalSpotCrossed);
                GoalSpotCrossed();
            }
            else
            {
                HealthSystem.Instance.SubtractHealthPoint();
            }
        }

        private void GoalSpotCrossed()
        {
            _available = false;
            spotRenderer.material = notAvailableMaterial;
            insideFrog.SetActive(true);
            GameManager.Instance.GoalSpotCrossed();
            GameManager.Instance.GoalSpotsManager.CheckAllSpots();
        }
    }
}
