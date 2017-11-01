using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tally : MonoBehaviour {
    public string operation = "plus";
    public bool isSecond = false;
    float distance = 10;
    Vector3 origPosition;
    public GameObject[] allTallies;
    public float xStart = -5;
    public float xEnd = -1;
    public float yStart = 0;
    public float yEnd = 0;

    public float finalXMove = -5;
    public float finalYMove = -1;

	// Use this for initialization
	void Start () {
        Debug.Log("In start");
        origPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update () {
		
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
        if (operation == "plus")
            return;

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

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
        Debug.Log("X and Y" + objPosition.x + ":" + objPosition.y);

        Debug.Log("In X Range");

        for (int i = 0; i < allTallies.Length; i++)

        {
            if ((objPosition.x > xStart) && (objPosition.x < xEnd))
            {
                allTallies[i].GetComponent<Tally>().moveThisTally(finalXMove, finalYMove);
            } else {
                allTallies[i].GetComponent<Tally>().moveBack();
            }
        }

        if ((objPosition.x > xStart) && (objPosition.x < xEnd))
        {
            GameObject g = GameObject.Find("SecondNumber");
            g.GetComponent<SecondNumberHandler>().CreateCrossOuts();
        }
    }
}
