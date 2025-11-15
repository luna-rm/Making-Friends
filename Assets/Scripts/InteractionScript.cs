using UnityEngine;

public class InteractionScript : MonoBehaviour {

    [SerializeField] private SpriteRenderer interaction;
    [SerializeField] private string dialogue;

    private bool enableInteraction = false;

    private void OnTriggerEnter(UnityEngine.Collider other) {
        if(other != null) {
            if(other.gameObject.name == "Player") {
                interaction.enabled = true;
                enableInteraction = true;
                if(GameEventManager.InputContext == InputContextEnum.DEFAULT && DialogueManager.alreadyStarted && dialogue != null){
                    GameEventManager.InputContext = InputContextEnum.DIALOGUE;
                }
            }
        }
    }

    private void OnTriggerExit(UnityEngine.Collider other) {
        if(other != null) {
            if(other.gameObject.name == "Player") {
                interaction.enabled = false;
                enableInteraction = false;

                if(dialogue != null) {
                    if(GameEventManager.InputContext == InputContextEnum.DIALOGUE) {
                        GameEventManager.InputContext = InputContextEnum.DEFAULT;
                    }
                }
            }
        }
    }

    private void Update() {
        if(enableInteraction && GameEventManager.InputContext == InputContextEnum.DEFAULT && !DialogueManager.alreadyStarted) {
            if (Input.GetKeyDown(KeyCode.E)){
                submitPressed();
            }
        }
    }

    private void submitPressed() {
        if(dialogue != null) {
            GameEventManager.instance.dialogueEvents.EnterDialogue(dialogue);
            GameEventManager.InputContext = InputContextEnum.DIALOGUE;
        }
    }
}
