
using System.Collections.Generic;
using UnityEngine;

public class PlayerLog : MonoBehaviour
{

    private List<string> eventLog = new List<string>();
    private string guiText = "";
    private int maxLines = 12;
 
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
