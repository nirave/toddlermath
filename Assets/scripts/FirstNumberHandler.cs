using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FirstNumberHandler : MonoBehaviour {

    GameObject stick = null;
    public Boolean hasBeenClicked = false;
    public GameObject[] tallyMarks = null;
    public int numberSelected = 2;

    // Use this for initialization
    void Start () {
        Debug.Log("Hello", gameObject);

    }

    // Update is called once per frame
    void Update () {

    }

    void TaskOnClick()
    {
        Debug.Log("You have done something with the button!");
    }

    void OnMouseDown()
    {
        Debug.Log("You have clicked the number!" + numberSelected);
        
        if (hasBeenClicked)
        {
            return;
        }

        hasBeenClicked = true;
        
        if (stick == null)
        {
            stick = (GameObject)Instantiate(Resources.Load("TallyMark"));
        }
        stick.SetActive(true);

        tallyMarks = new GameObject[numberSelected];

        for (int i = 0; i < numberSelected; i++)
        {
            float newX = ((float) (-1 - (i * 0.5)));
            float newY = ((float)-1);
            Vector3 pos = new Vector3(newX, newY, -2);
            Debug.Log("Created mark: " + newX);
            tallyMarks[i] = (GameObject)Instantiate(stick, pos, Quaternion.identity);
        }

        stick.SetActive(false);
    }
}
