using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer : MonoBehaviour
{
    GameObject wrong = null;
    GameObject right = null;
    float second = 0;
    bool correct = false;

    // Use this for initialization
    void Start()
    {
        wrong = (GameObject)Instantiate(Resources.Load("Wrong"));
        wrong.SetActive(false);
        right = (GameObject)Instantiate(Resources.Load("Right"));
        right.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        second -= Time.deltaTime;

        if (second <= 0)
        {
            right.SetActive(false);
            wrong.SetActive(false);

            if (correct)
            {
                GameObject g = GameObject.Find("New");
                g.GetComponent<NewQuestion>().getNewQuestion();
                correct = false;
            }
        }
    }

    public void AnswerThis(int whichAnswer)
    {

        Debug.Log("Hit a button " + whichAnswer);

        GameObject g = GameObject.Find("New");
        int correctAnswer = g.GetComponent<NewQuestion>().whichAnswer;

        if (whichAnswer == correctAnswer)
        {
            Debug.Log("Correct");
            wrong.SetActive(false);
            right.SetActive(true);
            second = 2;
            correct = true;
        }
        else
        {
            Debug.Log("Incorrect ");
            right.SetActive(false);
            wrong.SetActive(true);
            second = 2;
            correct = false;
        }

    }
}
