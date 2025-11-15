using UnityEngine;

public class ImageScript : MonoBehaviour {

    private Camera m_Camera;

    private void Start() {
        m_Camera = Camera.main;
    }

    void Update() { 
        Vector3 cameraPos = m_Camera.transform.position;

        transform.LookAt(cameraPos);
        transform.Rotate(0f, 180f, 0f);
    }
}
