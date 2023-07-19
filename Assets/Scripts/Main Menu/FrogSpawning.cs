using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Main_Menu
{
    public class FrogSpawning : MonoBehaviour
    {
        [SerializeField] private GameObject frogRagdoll;
        [SerializeField] private float timeBetweenSpawn;
        [SerializeField] private float lifeSpan;

        private void Start()
        {
            StartCoroutine(SpawnFrog());
        }

        private IEnumerator SpawnFrog()
        {
            var thisTransform = transform;
            var newFrog = Instantiate(frogRagdoll, thisTransform.position, thisTransform.rotation);
            Destroy(newFrog, lifeSpan);
            yield return new WaitForSecondsRealtime(timeBetweenSpawn);
            StartCoroutine(SpawnFrog());
        }
    }
}
