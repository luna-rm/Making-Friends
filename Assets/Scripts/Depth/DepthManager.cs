using UnityEngine;

public class DepthManager : MonoBehaviour {
    public static DepthManager instance { get; private set; }

    public bool eyeOpen = true;
    public bool inProtection = false;

    private void Awake() {
        if(instance == null) {
            instance = this;
        } 
    }
}
