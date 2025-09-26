using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DialogueSystem.Instance.ShowDialogue("You:", "Heard the folks at this joint were willing to have a little conversation.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
