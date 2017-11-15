using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tally : MonoBehaviour
{
    public string operation = "plus";
    public bool isSecond = false;
    float distance = 10;
    Vector3 origPosition;
    public GameObject[] allTallies;
    public float xStart = -5;
    public float xEnd = -1;
    public float yStart = 0;
    public float yEnd = 0;
    public GameObject stick = null;
    public bool hasMoved = false;

    public float finalXMove = -5;
    public float finalYMove = -1;

    // Use this for initialization
    void Start()
    {
        Debug.Log("In start");
        origPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        Debug.Log("Clicked tally - is second: " + isSecond);
    }

    private void moveBack()
    {
        transform.position = origPosition;
    }

    private void moveThisTally(float x, float y)
    {
        Debug.Log("moved x " + x);
        transform.position = new Vector3(origPosition.x + x, origPosition.y + y, origPosition.z);
    }

    void OnMouseDrag()
    {
        Debug.Log("In mouse drag");
        if (operation == "plus")
            return;

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Debug.Log("In Mouse Drag " + operation + "," + objPosition.x + ":" + objPosition.y);

        if (operation == "divide")
        {
            if (this.GetComponent<Tally>().hasMoved)
                return;

            moveThisTally(objPosition.x - origPosition.x, objPosition.y - origPosition.y);
            return;
        }

        for (int i = 0; i < allTallies.Length; i++)
        {
            allTallies[i].GetComponent<Tally>().moveThisTally(objPosition.x - origPosition.x, objPosition.y - origPosition.y);
        }
        //transform.position = objPosition;
    }

    void OnMouseUp()
    {
        if (operation == "plus")
            return;

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //Debug.Log("X and Y" + objPosition.x + ":" + objPosition.y);

        //Debug.Log("In X Range");

        if (operation == "minus")
        {
            for (int i = 0; i < allTallies.Length; i++)
            {
                if ((objPosition.x > xStart) && (objPosition.x < xEnd))
                {
                    allTallies[i].GetComponent<Tally>().moveThisTally(finalXMove, finalYMove);
                }
                else
                {
                    allTallies[i].GetComponent<Tally>().moveBack();
                }
            }

            if ((objPosition.x > xStart) && (objPosition.x < xEnd))
            {
                GameObject g = GameObject.Find("SecondNumber");
                g.GetComponent<SecondNumberHandler>().CreateCrossOuts();
            }
        }


        if (operation == "multiplication")
        {

            GameObject g = GameObject.Find("FirstNumber");
            GameObject g2 = GameObject.Find("SecondNumber");

            if (g2.GetComponent<SecondNumberHandler>().multTallies == null)
            {
                g2.GetComponent<SecondNumberHandler>().multTallies = new GameObject[g.GetComponent<FirstNumberHandler>().boxes.Length, allTallies.Length];
            }

            for (int tCounter = 0; tCounter < g.GetComponent<FirstNumberHandler>().boxes.Length; tCounter++)
            {
                Debug.Log(g.GetComponent<FirstNumberHandler>().boxes[tCounter].transform.position);
                Vector3 boxPosition = g.GetComponent<FirstNumberHandler>().boxes[tCounter].transform.position;
                float xStart = boxPosition.x - 2.5f;
                float xEnd = xStart + 5;
                float yStart = boxPosition.y - 0.5f;
                float yEnd = yStart + 1.5f;

                if ((objPosition.x > xStart) && (objPosition.x < xEnd) &&
                    (objPosition.y > yStart) && (objPosition.y < yEnd))
                {
                    if (stick == null)
                    {
                        stick = (GameObject)Instantiate(Resources.Load("TallyMark"));
                    }
                    stick.SetActive(true);

                    for (int i = 0; i < allTallies.Length; i++)
                    {
                        float newX = ((float)(-3.8 + (i * 0.5)));
                        float newY = yStart + 0.5f;
                        Vector3 pos = new Vector3(newX, newY, -2);
                        allTallies[i].GetComponent<Tally>().moveBack();
                        g2.GetComponent<SecondNumberHandler>().multTallies[tCounter, i] = (GameObject)Instantiate(stick, pos, Quaternion.identity);
                        Vector3 scale = g2.GetComponent<SecondNumberHandler>().multTallies[tCounter, i].transform.localScale;
                        scale.y = scale.y * 0.5f;
                        g2.GetComponent<SecondNumberHandler>().multTallies[tCounter, i].transform.localScale = scale;
                    }
                    stick.SetActive(false);

                    g.GetComponent<FirstNumberHandler>().boxes[tCounter].SetActive(false);

                    break;
                }
            }

            bool eraseAll = true;
            for (int tCounter = 0; tCounter < g.GetComponent<FirstNumberHandler>().boxes.Length; tCounter++)
            {
                if (g.GetComponent<FirstNumberHandler>().boxes[tCounter].activeSelf)
                {
                    eraseAll = false;
                }
            }

            for (int i = 0; i < allTallies.Length; i++)
            {
                if (eraseAll)
                {
                    allTallies[i].SetActive(false);
                    Debug.Log("Removing tally");
                }
                else
                {
                    allTallies[i].GetComponent<Tally>().moveBack();
                }
            }

        }

        if (operation == "divide")
        {

            int answer = GameObject.Find("New").GetComponent<NewQuestion>().answer;
            Debug.Log("In divide " + objPosition.x + ":" + objPosition.y);
            GameObject g = GameObject.Find("FirstNumber");
            GameObject g2 = GameObject.Find("SecondNumber");
            bool inBox = false;

            if (g2.GetComponent<SecondNumberHandler>().boxes == null)
            {
                moveBack();
                return;
            }

            for (int tCounter = 0; tCounter < g2.GetComponent<SecondNumberHandler>().boxes.Length; tCounter++)
            {
                if (this.GetComponent<Tally>().hasMoved)
                {
                    inBox = true;
                    continue;
                }

                Vector3 boxPosition = g2.GetComponent<SecondNumberHandler>().boxes[tCounter].transform.position;

                float xStart = boxPosition.x - 2.5f;
                float xEnd = xStart + 5;
                float yStart = boxPosition.y - 0.5f;
                float yEnd = yStart + 1.5f;
                //Debug.Log("Second box " + g2.GetComponent<SecondNumberHandler>().boxes[tCounter].transform.position);
                //Debug.Log("OBJECT " + objPosition.x +":"+ objPosition.y + "," + xStart +":"+ xEnd + "," + yStart + ":" + yEnd);

                if (g2.GetComponent<SecondNumberHandler>().boxTallies[tCounter] < answer)
                {
                    if ((objPosition.x > xStart) && (objPosition.x < xEnd) &&
                        (objPosition.y > yStart) && (objPosition.y < yEnd))
                    {
                        Debug.Log("In Box " + this.GetComponent<Tally>().hasMoved);
                        float xPosition = boxPosition.x - 1.5f + (g2.GetComponent<SecondNumberHandler>().boxTallies[tCounter] * 0.5f);
                        transform.position = new Vector3(xPosition, boxPosition.y, origPosition.z);
                        g2.GetComponent<SecondNumberHandler>().boxTallies[tCounter]++;
                        inBox = true;

                        if (g2.GetComponent<SecondNumberHandler>().boxTallies[tCounter] >= answer)
                        {
                            g2.GetComponent<SecondNumberHandler>().boxes[tCounter].SetActive(false);
                        }
                        this.GetComponent<Tally>().hasMoved = true;
                        Debug.Log("In Box 2" + this.GetComponent<Tally>().hasMoved);
                        break;
                    }
                }

            }

            if (!inBox)
                moveBack();
        }
    }
}

