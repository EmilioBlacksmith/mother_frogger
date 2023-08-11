using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnParticlesToPool : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }
}
