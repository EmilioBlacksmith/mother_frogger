using UnityEngine;

namespace Cars_System
{
    public class DeSpawner : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Car") || other.gameObject.CompareTag("WaterObj"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
