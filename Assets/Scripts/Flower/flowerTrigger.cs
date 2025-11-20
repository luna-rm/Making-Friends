using System.Collections;
using UnityEngine;

public class flowerTrigger : MonoBehaviour {
    [SerializeField] private GameObject flower; 
    [SerializeField] private ParticleSystem myParticleSystem;

    private void OnTriggerEnter(Collider other) {
        if(other != null) {
            if(other.gameObject.name == "Player") {
                Debug.Log("A");
                myParticleSystem.Emit(8);
                StartCoroutine(destroyFlower());
            }
        }
    }

    private IEnumerator destroyFlower() {
        yield return new WaitForSeconds(0.2f);
        Destroy(flower);
   }
}
