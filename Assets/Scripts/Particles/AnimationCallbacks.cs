using Audio;
using Character_System.Physics;
using UnityEngine;

namespace Particles
{
    public class AnimationCallbacks : MonoBehaviour
    {
        public void MakeLeftStep()
        {
            if(CrashController.Instance.hasCrash) return;
            ParticleSpawningSystem.Instance.TakeStep(ParticleSpawningSystem.FootUsed.Left);
            AudioSystem.Instance.PlayFootstep(ParticleSpawningSystem.FootUsed.Left);
        }
    
        public void MakeRightStep()
        {
            if(CrashController.Instance.hasCrash) return;
            ParticleSpawningSystem.Instance.TakeStep(ParticleSpawningSystem.FootUsed.Right);
            AudioSystem.Instance.PlayFootstep(ParticleSpawningSystem.FootUsed.Right);
        }
    }
}
