using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FirstNumberHandler : MonoBehaviour
{

    GameObject stick = null;
    GameObject box = null;
    public Boolean hasBeenClicked = false;
    public GameObject[] tallyMarks = null;
    public GameObject[] boxes = null;
    public int numberSelected = 2;
    public bool[] hasAlreadyMoved = null;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

        if (box == null)
        {
            box = (GameObject)Instantiate(Resources.Load("box"));
        }
        box.SetActive(true);

        tallyMarks = new GameObject[numberSelected];

        GameObject newQuestion = GameObject.Find("New");
        string operation = newQuestion.GetComponent<NewQuestion>().operation;

        if ((operation == "minus") || (operation == "plus"))
        {
            for (int i = 0; i < numberSelected; i++)
            {
                float newX = ((float)(-1 - (i * 0.5)));
                float newY = ((float)-1);
                Vector3 pos = new Vector3(newX, newY, -2);
                Debug.Log("Created mark: " + newX);
                tallyMarks[i] = (GameObject)Instantiate(stick, pos, Quaternion.identity);
            }
        }
        else if (operation == "multiplication")
        {
            boxes = new GameObject[numberSelected];
            for (int i = 0; i < numberSelected; i++)
            {
                float newX = -2.5f;
                float newY = 0.5f - (1.1f * i);
                Vector3 pos = new Vector3(newX, newY, -2);
                Debug.Log("Created box: " + newX);
                boxes[i] = (GameObject)Instantiate(box, pos, Quaternion.identity);
            }
        }
        else if (operation == "divide")
        {
            hasAlreadyMoved = new bool[numberSelected];
            for (int i = 0; i < numberSelected; i++)
            {
                float newX = ((float)(-1 - (i * 0.5)));
                float newY = ((float)-1);
                Vector3 pos = new Vector3(newX, newY, -2);
                Debug.Log("Created mark: " + newX);
                tallyMarks[i] = (GameObject)Instantiate(stick, pos, Quaternion.identity);
                tallyMarks[i].GetComponent<Tally>().operation = "divide";
                Vector3 scale = tallyMarks[i].transform.localScale;
                scale.y = scale.y * 0.5f;
                tallyMarks[i].transform.localScale = scale;
            }
        }

        stick.SetActive(false);
        box.SetActive(false);
    }
}