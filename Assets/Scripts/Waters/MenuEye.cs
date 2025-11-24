using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEye : MonoBehaviour {

    [SerializeField] private CanvasGroup eye;
    [SerializeField] private CanvasGroup background;
    
    private bool anim = false;

    void Update() {
        if(!anim) {
            if(this.GetComponent<CanvasGroup>().alpha == 1) {
                eye.alpha = 1;
                //add animation
                anim = true;
                StartCoroutine(appear());
            }
        }
    }

    public IEnumerator appear() {
        background.alpha = 0;

        float elapsedTime = 0f;

        while (elapsedTime < 1f) {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;

        while (elapsedTime < 1) {
            float newAlpha = Mathf.Lerp(0f, 1f, (elapsedTime / 1));
            background.alpha = newAlpha;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        background.alpha = 1f;

        SceneManager.LoadScene("depth");
    }
}
