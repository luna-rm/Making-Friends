using UnityEngine;

public class InteractionScript : MonoBehaviour {

    [SerializeField] private SpriteRenderer interaction;
    [SerializeField] private string dialogue;

    private bool enableInteraction = false;

    public bool dialogueFinished = false;
    private bool isCurrentConversation = false; // Tracks if WE started the dialogue

    private void OnEnable() {
        // Subscribe to the dialogue finished event
        GameEventManager.instance.dialogueEvents.onDialogueFinished += OnDialogueFinished;
    }

    private void OnDisable() {
        // Always unsubscribe when disabled to prevent errors
        if (GameEventManager.instance != null) {
            GameEventManager.instance.dialogueEvents.onDialogueFinished -= OnDialogueFinished;
        }
    }

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
            isCurrentConversation = true; // Mark that WE started this conversation
            GameEventManager.instance.dialogueEvents.EnterDialogue(dialogue);
            GameEventManager.InputContext = InputContextEnum.DIALOGUE;
        }
    }

    private void OnDialogueFinished() {
        // Only mark as finished if we were the ones who started it
        if(isCurrentConversation) {
            dialogueFinished = true;
            isCurrentConversation = false;
            Debug.Log("B");
        }
    }
}