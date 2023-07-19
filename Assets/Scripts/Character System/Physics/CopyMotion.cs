using UnityEngine;

namespace Character_System.Physics
{
    public class CopyMotion : MonoBehaviour
    {
        [SerializeField] private Transform targetLimb;
        private ConfigurableJoint _mConfigurableJoint;
    
        private Quaternion _targetInitialRotation;

        private void Start()
        {
            this._mConfigurableJoint = this.GetComponent<ConfigurableJoint>();
            this._targetInitialRotation = this.targetLimb.transform.localRotation;
        }

        private void FixedUpdate()
        {
            if (!CrashController.Instance.hasCrash)
            {
                this._mConfigurableJoint.targetRotation = CopyRotation();
            }
        }

        private Quaternion CopyRotation()
        {
            return Quaternion.Inverse(this.targetLimb.localRotation) * this._targetInitialRotation;
        }
    }
}
