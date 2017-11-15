using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationChange : MonoBehaviour
{
    GameObject newQuestion = null;
    private float second = 0;
    private string operation = "plus";

    private void Start()
    {
        newQuestion = GameObject.Find("New");
    }

    void OnMouseDown()
    {
        Debug.Log("You have clicked change " + second + " " + newQuestion.GetComponent<NewQuestion>().operation);

        if (newQuestion.GetComponent<NewQuestion>().operation == "plus")
        {
            Sprite newOp = (Sprite)Instantiate(Resources.Load<Sprite>("minus"));
            this.GetComponent<SpriteRenderer>().sprite = newOp;
            newQuestion.GetComponent<NewQuestion>().operation = "minus";
            newQuestion.GetComponent<NewQuestion>().getNewQuestion();
        }
        else if (newQuestion.GetComponent<NewQuestion>().operation == "minus")
        {
            Sprite newOp = (Sprite)Instantiate(Resources.Load<Sprite>("multiplication"));
            this.GetComponent<SpriteRenderer>().sprite = newOp;
            newQuestion.GetComponent<NewQuestion>().operation = "multiplication";
            newQuestion.GetComponent<NewQuestion>().getNewQuestion();
        }
        else if (newQuestion.GetComponent<NewQuestion>().operation == "multiplication")
        {
            Sprite newOp = (Sprite)Instantiate(Resources.Load<Sprite>("divide"));
            this.GetComponent<SpriteRenderer>().sprite = newOp;
            newQuestion.GetComponent<NewQuestion>().operation = "divide";
            newQuestion.GetComponent<NewQuestion>().getNewQuestion();
        }
        else if (newQuestion.GetComponent<NewQuestion>().operation == "divide")
        {
            Sprite newOp = (Sprite)Instantiate(Resources.Load<Sprite>("plus"));
            this.GetComponent<SpriteRenderer>().sprite = newOp;
            newQuestion.GetComponent<NewQuestion>().operation = "plus";
            newQuestion.GetComponent<NewQuestion>().getNewQuestion();
        }

    }
}
