using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondNumberHandler : MonoBehaviour
{
    GameObject stick = null;
    GameObject cross = null;
    GameObject box = null;
    public Boolean hasBeenClicked = false;
    public GameObject[] tallyMarks = null;
    public GameObject[,] multTallies = null;
    public GameObject[] crossOuts = null;
    public int numberSelected = 1;
    public float moveLeftX = 0.0f;
    public GameObject[] boxes = null;
    public int[] boxTallies = null;

    // Use this for initialization
    void Start()
    {
        cross = (GameObject)Instantiate(Resources.Load("cross"));
        cross.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeleteCross()
    {
        for (int cCounter = 0; cCounter < crossOuts.Length; cCounter++)
        {
            Destroy(crossOuts[cCounter]);
        }
    }

    public void CreateCrossOuts()
    {
        crossOuts = new GameObject[numberSelected];
        cross.SetActive(true);
        for (int i = 0; i < numberSelected; i++)
        {
            Vector3 pos = tallyMarks[i].transform.position;
            pos.y = pos.y + 0.75f;
            crossOuts[i] = (GameObject)Instantiate(cross, pos, Quaternion.identity);
        }
        cross.SetActive(false);
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
        if (box == null)
        {
            box = (GameObject)Instantiate(Resources.Load("box"));
        }
        stick.SetActive(true);
        cross.SetActive(true);
        box.SetActive(true);

        tallyMarks = new GameObject[numberSelected];

        GameObject newQuestion = GameObject.Find("New");
        string operation = newQuestion.GetComponent<NewQuestion>().operation;

        if (operation == "divide")
        {
            boxes = new GameObject[numberSelected];
            boxTallies = new int[numberSelected];
        }

        for (int i = 0; i < numberSelected; i++)
        {
            if (operation == "multiplication")
            {
                float newX = ((float)(1 + (i * 0.5)));
                float newY = ((float)-0);
                Vector3 pos = new Vector3(newX, newY, -2);
                Debug.Log("Created mark: " + newX);
                tallyMarks[i] = (GameObject)Instantiate(stick, pos, Quaternion.identity);
                Vector3 scale = tallyMarks[i].transform.localScale;
                scale.y = scale.y * 0.5f;
                tallyMarks[i].transform.localScale = scale;

                tallyMarks[i].GetComponent<Tally>().isSecond = true;
                tallyMarks[i].GetComponent<Tally>().operation = "multiplication";
                tallyMarks[i].GetComponent<Tally>().allTallies = tallyMarks;
                tallyMarks[i].GetComponent<Tally>().finalXMove = moveLeftX;
                tallyMarks[i].GetComponent<Tally>().xStart = moveLeftX;
                tallyMarks[i].GetComponent<Tally>().yStart = -1;
            }
            else if (operation == "divide")
            {
                float newX = 2.5f;
                float newY = 0.5f - (1.1f * i);
                Vector3 pos = new Vector3(newX, newY, -2);
                Debug.Log("Created box: " + newX);
                boxes[i] = (GameObject)Instantiate(box, pos, Quaternion.identity);
            }
            else
            {
                float newX = ((float)(1 + (i * 0.5)));
                float newY = ((float)-1);
                Vector3 pos = new Vector3(newX, newY, -2);
                Debug.Log("Created mark: " + newX);
                tallyMarks[i] = (GameObject)Instantiate(stick, pos, Quaternion.identity);
                if (operation == "plus")
                {
                    tallyMarks[i].GetComponent<Tally>().isSecond = false;
                    tallyMarks[i].GetComponent<Tally>().operation = "plus";
                }
                else if (operation == "minus")
                {
                    tallyMarks[i].GetComponent<Tally>().isSecond = true;
                    tallyMarks[i].GetComponent<Tally>().operation = "minus";
                    tallyMarks[i].GetComponent<Tally>().allTallies = tallyMarks;
                    tallyMarks[i].GetComponent<Tally>().finalXMove = moveLeftX;
                    tallyMarks[i].GetComponent<Tally>().xStart = moveLeftX;
                }
            }
        }

        stick.SetActive(false);
        cross.SetActive(false);
        box.SetActive(false);
    }
}
