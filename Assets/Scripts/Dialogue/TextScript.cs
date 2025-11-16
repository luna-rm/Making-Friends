using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TextScript : MonoBehaviour {

    [SerializeField] private TextMeshPro dialogueText;
    [SerializeField] private float typingSpeed = 0.02f;

    private Coroutine typingCoroutine;
    private string currentSentence;
    private int enterCount = 0;
    private int charCount = 0;
    

    [SerializeField] public List<AudioClip> voice;
    private int voiceCount = 0;

    public bool isDialogue = true;

    private void Awake() {
        dialogueText.enabled = false;
        ResetPanel();
    }

    private void OnEnable() {
        if (isDialogue) {
            GameEventManager.instance.dialogueEvents.onDialogueStarted += DialogueStarted;
            GameEventManager.instance.dialogueEvents.onDialogueFinished += DialogueFinished;
            GameEventManager.instance.dialogueEvents.onDisplayDialogue += DisplayDialogue;
            GameEventManager.instance.dialogueEvents.onSkipTyping += CompleteSentence;
        }
    }

    private void OnDisable() {
        if(isDialogue) {
            GameEventManager.instance.dialogueEvents.onDialogueStarted -= DialogueStarted;
            GameEventManager.instance.dialogueEvents.onDialogueFinished -= DialogueFinished;
            GameEventManager.instance.dialogueEvents.onDisplayDialogue -= DisplayDialogue;
            GameEventManager.instance.dialogueEvents.onSkipTyping -= CompleteSentence;
        }
    }

    private void DialogueStarted() {
        dialogueText.enabled = true;
    }

    private void DialogueFinished() {
        dialogueText.enabled = false;
    }

    private void DisplayDialogue(string dialogueLine) {
        currentSentence = dialogueLine;

        if (typingCoroutine != null) {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeSentence());
    }

    public void DisplayText(string textLine) {
        currentSentence = textLine;
        typingCoroutine = StartCoroutine(TypeSentence());
    }

    private IEnumerator TypeSentence() {
        dialogueText.text = ""; 
        enterCount = 0;
        charCount = 0;
        GameEventManager.instance.dialogueEvents.isTyping = true;

        foreach (char letter in currentSentence.ToCharArray()) {
            if (GameEventManager.instance.dialogueEvents.isTyping) {
                if(enterCount >= 25 && letter.Equals(' ')) {
                    dialogueText.text += "\n";
                    enterCount = 0;
                } else {
                    dialogueText.text += letter;
                    enterCount++;
                    PlayVoice();
                    yield return new WaitForSeconds(typingSpeed);
                }
                charCount++;
            }
        }
        GameEventManager.instance.dialogueEvents.isTyping = false;
    }

    private void CompleteSentence() {
        if (GameEventManager.instance.dialogueEvents.isTyping) { 
            GameEventManager.instance.dialogueEvents.isTyping = false;

            int charAux = 0;
            string auxDialogueText = dialogueText.text;
            foreach (char letter in currentSentence.ToCharArray()) {
                if(charCount >= charAux) {
                    charAux++;
                    continue;
                }
                if(enterCount >= 25 && letter.Equals(' ')) {
                    auxDialogueText += "\n";
                    enterCount = 0;
                } else {
                    auxDialogueText += letter;
                    enterCount++;
                }
            }
            dialogueText.text = auxDialogueText;
            PlayVoice();
        }
    }

    private void ResetPanel() {
        dialogueText.text =  "";
        currentSentence = "";
        enterCount = 0;
        charCount = 0;
    }

    private void PlayVoice() {
        SoundFXManager.instance.PlaySoundFXClip(voice[voiceCount], transform, 0.5f);
        voiceCount++;
        if(voiceCount >= voice.Count) { 
            voiceCount = 0;
        }
    }
}
