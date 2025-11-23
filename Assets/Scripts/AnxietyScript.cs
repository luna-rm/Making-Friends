using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;

public class AnxietyScript : MonoBehaviour {

    [Range(0f, 1f)]
    public float anxiety = 0f;

    public Volume postProcessing;

    private UnityEngine.Rendering.Universal.Vignette vignette;
    private UnityEngine.Rendering.Universal.ChromaticAberration chromaticAberration;
    private FilmGrain filmGrain;

    private float valueTo = 0f;

    private Coroutine currentAnimCoroutine;

    [SerializeField] private AudioSource noiseObject;

    void Start() {
        if (postProcessing.profile.TryGet(out UnityEngine.Rendering.Universal.Vignette vg)) {
            vignette = vg;
        }
        else {
            Debug.LogError("Vignette component not found on the Volume profile.");
        }

        if (postProcessing.profile.TryGet(out UnityEngine.Rendering.Universal.FilmGrain fg)) {
            filmGrain = fg;
        }
        else {
            Debug.LogError("FilmGrain component not found on the Volume profile.");
        }

        if (postProcessing.profile.TryGet(out UnityEngine.Rendering.Universal.ChromaticAberration ca)) {
            chromaticAberration = ca;
        }
        else {
            Debug.LogError("Vignette component not found on the Volume profile.");
        }
    }

    void Update() {

        if (filmGrain != null) {
            filmGrain.intensity.value = anxiety;
        }

        if (chromaticAberration != null) {
            chromaticAberration.intensity.value = anxiety;
        }

        float targetIntensity = Mathf.Lerp(anxiety/8+0.15f, anxiety/8+0.2f, Mathf.PingPong(Time.time * 0.33f, 1));
        vignette.intensity.value = targetIntensity;
    }

    public void addValue(bool anim, float add) {
        if (currentAnimCoroutine != null) {
            StopCoroutine(currentAnimCoroutine);
            anxiety = valueTo;
        }
        
        if (anim) {
            currentAnimCoroutine = StartCoroutine(addValueAnim(add));
        } else {
            anxiety += add;
        }
    }

    public IEnumerator addValueAnim(float add) {
        valueTo = anxiety + add;
        Debug.Log(valueTo);
        anxiety = 0.99f;
        noiseObject.volume = 0.1f;

        float elapsedTime = 0f;

        while (elapsedTime < 2f) {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;

        while (elapsedTime < 2f) {
            float newValue = Mathf.Lerp(0.99f, valueTo, (elapsedTime / 2f));
            anxiety = newValue;

            float noiseSound = Mathf.Lerp(0.1f, 0f, (elapsedTime/2f));
            noiseObject.volume = noiseSound;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        anxiety = valueTo;
        noiseObject.volume = 0f;
        currentAnimCoroutine = null;
    }
}
