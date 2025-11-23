using System.Collections;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public DialogueEvents dialogueEvents;

    public static GameEventManager instance { get; private set; }

    public static InputContextEnum InputContext {  get; set; } = InputContextEnum.DEFAULT;

    public Coroutine MapCoroutine = null;
    public bool canOpenMap = false;
    [SerializeField] CanvasGroup map;
    [SerializeField] private float defaultFadeDuration = 0.5f;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }

        dialogueEvents = new DialogueEvents();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.M) && MapCoroutine == null && canOpenMap){
            if(map.alpha == 0f) {
                MapCoroutine = StartCoroutine(openMap());
            } else {
                MapCoroutine = StartCoroutine(closeMap());
            }
        }
    }

    public IEnumerator closeMap() {
        map.alpha = 1;

        float elapsedTime = 0f;

        while (elapsedTime < defaultFadeDuration) {
            float newAlpha = Mathf.Lerp(1f, 0f, (elapsedTime / defaultFadeDuration));
            map.alpha = newAlpha;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        map.alpha = 0f;
        MapCoroutine = null;
        InputContext = InputContextEnum.DEFAULT;
    }

    public IEnumerator openMap() {
        InputContext = InputContextEnum.LOCKED;
        map.alpha = 0;

        float elapsedTime = 0f;

        while (elapsedTime < defaultFadeDuration) {
            float newAlpha = Mathf.Lerp(0f, 1f, (elapsedTime / defaultFadeDuration));
            map.alpha = newAlpha;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        map.alpha = 1f;
        MapCoroutine = null;
    }
}
