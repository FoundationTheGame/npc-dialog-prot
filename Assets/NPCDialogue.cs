using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour {

    public DialogueFile dialogueFile = null;

    public GameObject button;

    private DialogueManager manager;
    private Dialogue currentDialogue;
    private Dialogue.Choice currentChoice = null;

    private GameObject npcDialogue = null;
    private Text npcText = null;
    private GameObject playerPanel = null;

    // Use this for initialization
    void Start () {
        manager = DialogueManager.LoadDialogueFile(dialogueFile);
        currentDialogue = manager.GetDialogue("Claude");
        currentChoice = currentDialogue.GetChoices()[0];
        currentDialogue.PickChoice(currentChoice);

        npcDialogue = GameObject.Find("NPC-Dialogue");
        npcText = npcDialogue.GetComponent<Text>();

        playerPanel = GameObject.Find("Player-Panel");
    }

    void OnGUI() {
        Rect r = playerPanel.GetComponent<RectTransform>().rect;
        GUILayout.BeginArea(new Rect(25, 375, r.width - 15, r.height));

        //Display NPC sentence
        npcText.text = currentChoice.dialogue;
        
        
        //show coices for player
        if (currentDialogue.GetChoices().Length > 1){
            // sort list
            Dialogue.Choice[] list = currentDialogue.GetChoices();
            System.Array.Sort(list, (o1, o2) => o1.userData.CompareTo(o2.userData));

            GUILayout.BeginVertical();
            foreach (Dialogue.Choice choice in list) {
                if (GUILayout.Button(choice.dialogue))
                {
                    currentDialogue.PickChoice(choice);
                    currentChoice = choice;
                    
                }
            }
            GUILayout.EndVertical();
        }
        else if (currentDialogue.GetChoices().Length == 1) {
            if (GUILayout.Button("Next"))
            {
                currentChoice = currentDialogue.GetChoices()[0];
                currentDialogue.PickChoice(currentChoice);
            }
        }
        else {
            if (GUILayout.Button("Go away."))
            {
                Application.Quit();
            }
        }
        GUILayout.EndArea();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
