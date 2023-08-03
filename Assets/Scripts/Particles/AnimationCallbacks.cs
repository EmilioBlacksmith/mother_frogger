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
        }
    
        public void MakeRightStep()
        {
            if(CrashController.Instance.hasCrash) return;
            ParticleSpawningSystem.Instance.TakeStep(ParticleSpawningSystem.FootUsed.Right);
        }
    }
}
