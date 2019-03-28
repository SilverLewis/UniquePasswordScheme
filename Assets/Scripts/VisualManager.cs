using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualManager : MonoBehaviour
{
    Arrow[] arrows = new Arrow[7];
    int[] randomPassword = new int[7];
    public GameObject arrowScreen, InstructionScreen;
    public Text instruction;
    public Image[] instructArrows = new Image[7];
    int screenCount = 0;

    void Start()
    {
        InitiateArrows();
        


    }

    void NextScreen() {
        //sets proper screen
        if (screenCount % 2 == 0)
        {
            arrowScreen.SetActive(true);
            InstructionScreen.SetActive(false);
            //creates random password


        }
        else {
            arrowScreen.SetActive(true);
            InstructionScreen.SetActive(false);
        }
    }

    private void RandomPassword() {
        for (int i = 0; i < instructArrows.Length; i++)
        {
            int chosen = Random.Range(0, 7);
            instructArrows[i].color = arrows[chosen].color;
            instructArrows[i].gameObject.transform.rotation = Quaternion.Euler(0, 0, arrows[chosen].rotation); 
        }
    }

    void InitiateArrows() {
        arrows[0].color =Color.white;
        arrows[1].color = Color.yellow;
        arrows[2].color = Color.green;
        arrows[3].color = Color.black;
        arrows[4].color = Color.grey;
        arrows[5].color = Color.blue;
        arrows[6].color = Color.magenta;
        arrows[6].color = Color.red;

        for (int i =0;i<7;i++)
            arrows[0].rotation = 45*i+45;
    }
}
