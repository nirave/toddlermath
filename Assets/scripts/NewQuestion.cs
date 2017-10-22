using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewQuestion : MonoBehaviour
{
    int first = 5;
    int second = 2;
    public int answer = 7;
    public int whichAnswer = 1;
    System.Random random = new System.Random();

    // Use this for initialization
    void Start()
    {
        OnMouseDown();
    }

    private string getRandomNumber()
    {
        int ret = answer;


        while (ret == answer)
        {
            ret = random.Next(0, 15);
        }

        return ret.ToString();

    }

    public void getNewQuestion()
    {
        GameObject g = GameObject.Find("FirstNumber");

        first = random.Next(1, 10);
        second = random.Next(1, 10);
        answer = first + second;

        Sprite newNumber = (Sprite)Instantiate(Resources.Load<Sprite>(first.ToString()));
        g.GetComponent<SpriteRenderer>().sprite = newNumber;

        g.GetComponent<FirstNumberHandler>().hasBeenClicked = false;
        g.GetComponent<FirstNumberHandler>().numberSelected = first;
        for (int tCounter = 0; tCounter < g.GetComponent<FirstNumberHandler>().tallyMarks.Length; tCounter++)
        {
            Destroy(g.GetComponent<FirstNumberHandler>().tallyMarks[tCounter]);
        }

        g = GameObject.Find("SecondNumber");
        newNumber = (Sprite)Instantiate(Resources.Load<Sprite>(second.ToString()));
        g.GetComponent<SpriteRenderer>().sprite = newNumber;

        g.GetComponent<SecondNumberHandler>().hasBeenClicked = false;
        g.GetComponent<SecondNumberHandler>().numberSelected = second;
        for (int tCounter = 0; tCounter < g.GetComponent<SecondNumberHandler>().tallyMarks.Length; tCounter++)
        {
            Destroy(g.GetComponent<SecondNumberHandler>().tallyMarks[tCounter]);
        }

        whichAnswer = random.Next(1, 5);

        GameObject a1 = GameObject.Find("Answer1");
        if (whichAnswer == 1)
        {
            a1.GetComponent<Button>().GetComponentInChildren<Text>().text = answer.ToString();
        }
        else
        {
            a1.GetComponent<Button>().GetComponentInChildren<Text>().text = getRandomNumber();
        }

        GameObject a2 = GameObject.Find("Answer2");
        if (whichAnswer == 2)
        {
            a2.GetComponent<Button>().GetComponentInChildren<Text>().text = answer.ToString();
        }
        else
        {
            a2.GetComponent<Button>().GetComponentInChildren<Text>().text = getRandomNumber();
        }

        GameObject a3 = GameObject.Find("Answer3");
        if (whichAnswer == 3)
        {
            a3.GetComponent<Button>().GetComponentInChildren<Text>().text = answer.ToString();
        }
        else
        {
            a3.GetComponent<Button>().GetComponentInChildren<Text>().text = getRandomNumber();
        }

        GameObject a4 = GameObject.Find("Answer4");
        if (whichAnswer == 4)
        {
            a4.GetComponent<Button>().GetComponentInChildren<Text>().text = answer.ToString();
        }
        else
        {
            a4.GetComponent<Button>().GetComponentInChildren<Text>().text = getRandomNumber();
        }
    }


    void OnMouseDown()
    {

        Debug.Log("You have clicked new");
        getNewQuestion();

    }
}