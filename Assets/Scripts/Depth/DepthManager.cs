using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class DepthManager : MonoBehaviour {
    public static DepthManager instance { get; private set; }

    //Vector3(0,0,325.690002)

    public bool eyeOpen = true;
    public bool inProtection = false;
    public bool startParticles = false;
    public Vector3 save = new Vector3(0,0,-6.4f);

    [SerializeField] private CanvasGroup blackScreen;
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject barrierObj;
    [SerializeField] private float speedBarrier = 0.5f;

    private void Awake() {
        if(instance == null) {
            instance = this;
        } 
    }

    public void fall() {
        Debug.Log("FAA");
        blackScreen.alpha = 1.0f;
        player.transform.position = save;
        StartCoroutine(disappear());
    }

    public IEnumerator disappear() {
        float elapsedTime = 0f;

        while (elapsedTime < 1.0f) {
            float newAlpha = Mathf.Lerp(1f, 0f, (elapsedTime / 1.0f));
            blackScreen.alpha = newAlpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        blackScreen.alpha = 0f;
    }

    private void Update() {
        if (eyeOpen) {
            float step = speedBarrier * Time.deltaTime;

            barrierObj.transform.position = Vector3.MoveTowards(barrierObj.transform.position, barrierObj.transform.position + new Vector3(0, 0, 350f), step);
        }
    }
}
