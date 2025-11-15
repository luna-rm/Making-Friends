using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public DialogueEvents dialogueEvents;

    public static GameEventManager instance { get; private set; }

    public static InputContextEnum InputContext {  get; set; } = InputContextEnum.DEFAULT;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }

        dialogueEvents = new DialogueEvents();
    }
}
