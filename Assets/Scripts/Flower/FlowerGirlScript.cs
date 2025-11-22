using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FlowerGirlScript : MonoBehaviour {
    [SerializeField] InteractionScript InteractionScript;
    [SerializeField] GameObject Player;
    [SerializeField] ImageScript imageScript;
    [SerializeField] GameObject maze;

    [SerializeField] int whichFlower = 0;

    private bool anim0Start = false;

    private void Update() {
        if(InteractionScript.dialogueFinished && whichFlower == 0 && !anim0Start) {
            StartCoroutine(anim0());
            anim0Start = true;
        }
    }

    private IEnumerator anim0() {
        float elapsedTime = 0f;
        
        Quaternion startRotation = Player.transform.rotation;

        Vector3 directionToTarget = this.transform.position - Player.transform.position;
        directionToTarget.y = 0;
        Quaternion endRotation = Quaternion.LookRotation(directionToTarget);

        while (elapsedTime < 0.5f) {
            float t = elapsedTime / 0.5f;

            Player.transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Player.transform.rotation = endRotation;

        StartCoroutine(imageScript.disapear());
        yield return new WaitForSeconds(imageScript.animTime + 0.2f);

        elapsedTime = 0f;
        
        startRotation = Player.transform.rotation;

        directionToTarget = maze.transform.position - Player.transform.position;
        directionToTarget.y = 0;
        endRotation = Quaternion.LookRotation(directionToTarget);

        while (elapsedTime < 0.5f) {
            float t = elapsedTime / 0.5f;

            Player.transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Player.transform.rotation = endRotation;
    }

}
