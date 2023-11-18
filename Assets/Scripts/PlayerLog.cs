using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PlayerLog : MonoBehaviour
{

    private List<string> eventLog = new List<string>();
    private string guiText = "";
    private int maxLines = 12;
    private void OnGUI() {
        GUI.skin.textArea.fontSize = 25;
        GUI.Label(new Rect(485, Screen.height - (Screen.height / 2), Screen.width - (Screen.width / 2.2f), Screen.height / 3), guiText, GUI.skin.textArea);
    }

    public void AddEvent(string eventString) {
        eventLog.Add(eventString);
        guiText = "";
        if (eventLog.Count > maxLines ) {
            eventLog.RemoveAt(0);
        }
        foreach (string log in eventLog) {
            guiText += log;
            guiText += "\n";
        }
    }
}
