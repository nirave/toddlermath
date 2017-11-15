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
    public string operation = "plus";
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

        if (operation == "plus")
        {
            first = random.Next(1, 10);
            second = random.Next(1, 10);
            answer = first + second;
        }
        else if (operation == "minus")
        {
            first = random.Next(1, 10);
            second = random.Next(1, first);
            answer = first - second;
        }
        else if (operation == "multiplication")
        {
            first = random.Next(1, 5);
            second = random.Next(1, 6);
            answer = first * second;
        }
        else if (operation == "divide")
        {
            answer = random.Next(1, 5);
            second = random.Next(1, 4);
            first = answer * second;
        }

        Sprite newNumber = (Sprite)Instantiate(Resources.Load<Sprite>(first.ToString()));
        g.GetComponent<SpriteRenderer>().sprite = newNumber;

        g.GetComponent<FirstNumberHandler>().hasBeenClicked = false;
        g.GetComponent<FirstNumberHandler>().numberSelected = first;
        for (int tCounter = 0; tCounter < g.GetComponent<FirstNumberHandler>().tallyMarks.Length; tCounter++)
        {
            Destroy(g.GetComponent<FirstNumberHandler>().tallyMarks[tCounter]);
        }

        if (g.GetComponent<FirstNumberHandler>().boxes != null)
        {
            for (int tCounter = 0; tCounter < g.GetComponent<FirstNumberHandler>().boxes.Length; tCounter++)
            {
                Destroy(g.GetComponent<FirstNumberHandler>().boxes[tCounter]);
            }
            g.GetComponent<FirstNumberHandler>().boxes = null;
        }

        g = GameObject.Find("SecondNumber");
        newNumber = (Sprite)Instantiate(Resources.Load<Sprite>(second.ToString()));
        g.GetComponent<SpriteRenderer>().sprite = newNumber;

        g.GetComponent<SecondNumberHandler>().hasBeenClicked = false;
        g.GetComponent<SecondNumberHandler>().numberSelected = second;
        g.GetComponent<SecondNumberHandler>().moveLeftX = ((float)(-1.5 - (first * 0.5)));

        for (int tCounter = 0; tCounter < g.GetComponent<SecondNumberHandler>().tallyMarks.Length; tCounter++)
        {
            Destroy(g.GetComponent<SecondNumberHandler>().tallyMarks[tCounter]);
        }

        g.GetComponent<SecondNumberHandler>().DeleteCross();

        if (g.GetComponent<SecondNumberHandler>().multTallies != null)
        {
            for (int tCounter = 0; tCounter < g.GetComponent<SecondNumberHandler>().multTallies.GetLength(0); tCounter++)
            {
                for (int t2Counter = 0; t2Counter < g.GetComponent<SecondNumberHandler>().multTallies.GetLength(1); t2Counter++)
                {
                    Destroy(g.GetComponent<SecondNumberHandler>().multTallies[tCounter, t2Counter]);
                }
            }
            g.GetComponent<SecondNumberHandler>().multTallies = null;
        }

        if (g.GetComponent<SecondNumberHandler>().boxes != null)
        {
            for (int tCounter = 0; tCounter < g.GetComponent<SecondNumberHandler>().boxes.Length; tCounter++)
            {
                Destroy(g.GetComponent<SecondNumberHandler>().boxes[tCounter]);
            }
            g.GetComponent<SecondNumberHandler>().boxes = null;
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