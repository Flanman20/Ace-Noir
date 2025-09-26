using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DialogueSystem.Instance.ShowDialogue("Hello world! this is a new message.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
