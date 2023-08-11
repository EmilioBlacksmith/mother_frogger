using Unity.Mathematics;
using UnityEngine;

namespace Particles
{
    public class ParticleSpawningSystem : MonoBehaviour
    {
        public enum FootUsed
        {
            Right,
            Left
        }
        
        [SerializeField] private Transform rightFoot;
        [SerializeField] private Transform leftFoot;
        
        [Space]
        [SerializeField] private GameObject bloodParticle;
        [SerializeField] private GameObject crashParticles;
        [SerializeField] private GameObject spawningParticles;
        [SerializeField] private GameObject placingObjectParticles;
        [SerializeField] private GameObject crossSpotParticles;
        [SerializeField] private GameObject footstepParticles;
        
        public static ParticleSpawningSystem Instance { get; private set; }
    
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
        }

        public void SpawnBloodParticle(Transform positionToSpawn) => SpawnParticle(bloodParticle, positionToSpawn);
        public void SpawnCrashParticle(Transform positionToSpawn) => SpawnParticle(crashParticles, positionToSpawn);
        public void SpawnSpawningParticle(Transform positionToSpawn) => SpawnParticle(spawningParticles, positionToSpawn);
        public void SpawnPlacingObjectParticle(Transform positionToSpawn) => SpawnParticle(placingObjectParticles, positionToSpawn);
        public void SpawnCrossSpotParticle(Transform positionToSpawn) => SpawnParticle(crossSpotParticles, positionToSpawn);

        public void TakeStep(FootUsed footUsed)
        {
            switch (footUsed)
            {
                case FootUsed.Left:
                    SpawnParticle(footstepParticles, leftFoot);
                    break;
                case FootUsed.Right:
                    SpawnParticle(footstepParticles, rightFoot);
                    break;
                default:
                    break;
            }
        }

        private void SpawnParticle(GameObject particleToSpawn, Transform spawnPosition)
        {
            ObjectPoolManager.SpawnObject(particleToSpawn, spawnPosition.position, particleToSpawn.transform.rotation, ObjectPoolManager.PoolType.ParticleObject);
        }
    }
}
