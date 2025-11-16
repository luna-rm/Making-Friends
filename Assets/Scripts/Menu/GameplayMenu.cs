using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayMenu : MonoBehaviour {

    public static GameplayMenu instance {  get; private set; }

    [SerializeField] public float timeText = 5f;

    [SerializeField] private GameObject textField;
    [SerializeField] private List<string> textList = new List<string>();
    [SerializeField] public List<AudioClip> voice;

    [SerializeField] private Transform cameraTransform; 
    [SerializeField] private float maxDistance = 7.5f;

    [SerializeField] private float timeTextFall = 2.0f;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    public void init() {
        StartCoroutine(startCount());
    }
    private IEnumerator startCount() {
        float elapsedTime = 0f;

        while (elapsedTime < 7.5f) {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for(int i = 0; i < textList.Count; i++) {
            elapsedTime = 0f;

            while (elapsedTime < timeText) {
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            awakeTextAppear(i);
        }        
        for(int i = 0; i < 10; i++){
            elapsedTime = 0f;

            while (elapsedTime < 1.0f) {
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            timeTextFall = 12f;
            awakeTextAppear(3);
        }
    }

    private void awakeTextAppear(int text) {
        RaycastHit hitInfo;
        Vector3 spawnPosition;
        Quaternion spawnRotation = Quaternion.identity;

        bool didHit = Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hitInfo, maxDistance);

        if (didHit) {
            spawnPosition = hitInfo.point;
            spawnRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal); 
        }
        else {
            spawnPosition = cameraTransform.position + cameraTransform.forward * maxDistance;
        }

        if(text == 3) {
            spawnPosition.y += (Random.value - 0.5f) * 7f;
            spawnPosition.x += (Random.value - 0.5f) * 7f;
            spawnPosition.z += (Random.value - 0.5f) * 7f;
        }

        GameObject textObj = Instantiate(textField, spawnPosition, spawnRotation);
        textObj.GetComponent<TextScript>().voice = voice;
        textObj.GetComponent<TextMeshPro>().enabled = true;
        textObj.GetComponent<TextMeshPro>().fontSize = 8;
        textObj.GetComponent<TextScript>().DisplayText(textList[text]);
        StartCoroutine(makeTextFall(textObj));
    }

    private IEnumerator makeTextFall(GameObject textObj) {
        float elapsedTime = 0f;

        while (elapsedTime < timeTextFall) {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textObj.GetComponent<Rigidbody>().useGravity = true;
        textObj.GetComponent<BoxCollider>().enabled = false;
        textObj.GetComponent<ImageScript>().enabled = false;
    }
}
