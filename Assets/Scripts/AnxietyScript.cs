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

        
        if (anxiety > 0.75f) {
            if (vignette != null) {
                float targetIntensity = Mathf.Lerp(0.25f, 0.30f, Mathf.PingPong(Time.time * 0.33f, 1));
                vignette.intensity.value = targetIntensity;
            }
        } else if (anxiety > 0.50f) {
            if (vignette != null) {
                float targetIntensity = Mathf.Lerp(0.20f, 0.25f, Mathf.PingPong(Time.time * 0.66f, 1));
                vignette.intensity.value = targetIntensity;
            }
        } else if (anxiety > 0.25f) {
            if (vignette != null) {
                float targetIntensity = Mathf.Lerp(0.15f, 0.20f, Mathf.PingPong(Time.time * 1f, 1));
                vignette.intensity.value = targetIntensity;
            }
        } else {
            vignette.intensity.value = 0.15f;
        }
    }

    public void addValue(bool anim, float add) {
        if (anim) {
            StartCoroutine(addValueAnim(add));
        } else {
            anxiety += add;
        }
    }

    public IEnumerator addValueAnim(float add) {
        float value = anxiety + add;
        Debug.Log(value);
        anxiety = 0.8f;

        float elapsedTime = 0f;

        while (elapsedTime < 2f) {
            float newValue = Mathf.Lerp(0.8f, value, (elapsedTime / 2f));
            anxiety = newValue;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        anxiety = value;
    }
}
