using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class DialogueGenerator  {

    private static DialogueList listOfDialogues;
    private static string path;

    public static DialogueList GenerateDialogues()
    {
        path = Application.streamingAssetsPath + "/dialogues.json";
        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);
            listOfDialogues = JsonUtility.FromJson<DialogueList>(jsonString);
            return listOfDialogues;
        }
        else
        {
            Debug.LogError("dialogue path couldn't be opened");
            return null;
        }
    }
}

[System.Serializable]
public class DialogueList
{
    public List<DialogueDescription> WTAFDialogues;
}

[System.Serializable]
public class DialogueDescription
{
    public int dialogueID;
    public bool autoDisplayDialogue;
    public string dialogueGiverName;
    public string[] dialogueLines;
    public int dialogueActivated;
    public int[] questsNeededToActivateDialogue;
    public int[] questsActivatedByDialgue;
}
