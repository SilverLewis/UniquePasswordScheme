using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewManager : MonoBehaviour
{
    Arrow[] arrows;
    public Image[] instructArrows;
    public Text passDomain;
    public Text arrowPassDomain;

    public GameObject arrowScreen, InstructionScreen;
    
    //runs on program start
    void Awake()
    {
        arrows = new Arrow[StaticVariables.passwordOptions];
        InitiateArrows();
    }

    
    //sets all the UI arrows that would spawn when user gets assigned password
    void InitiateArrows()
    {
        for (int i = 0; i < arrows.Length; i++)
            arrows[i] = new Arrow();

        arrows[0].color = Color.white;
        arrows[1].color = Color.yellow;
        arrows[2].color = Color.green;
        arrows[3].color = new Color(.226f, .198f, .198f);//offset of black so it doesnt match outline
        arrows[4].color = Color.grey;
        arrows[5].color = Color.blue;
        arrows[6].color = Color.red;
        arrows[7].color = Color.magenta;

        for (int i = 0; i < arrows.Length; i++)
            arrows[i].rotation = 45 * i;
    }

    //shows password assigning screen, manipulates UI by setting the pages to active (true) or inactive (false)
    public void ShowPassword(string domain, int[] password, bool correct)
    {
        arrowScreen.SetActive(false);
        InstructionScreen.SetActive(true);
        SetArrows(password);
  
        //correct is if false if user re-entered the password incorrectly
        if (correct)
            passDomain.text = "Your "+domain + "'s password is:";
        else
            passDomain.text = "Wrong Password!\nYour " + domain + "'s password is:";


    }

    //password entering screen, manipulates UI by setting the pages to active (true) or inactive (false)
    public void EnterPassword(string domain, bool correct, int AttemptsRemaining)
    {
        arrowScreen.SetActive(true);
        InstructionScreen.SetActive(false);

       arrowPassDomain.text = "Your " + domain + "'s password is:";

        //if 0 doesnt show attempts left
        if(AttemptsRemaining>0)
            arrowPassDomain.text += "\nYou have " + AttemptsRemaining + " remaining";
        //if last entry was wrong
        if (!correct)
            arrowPassDomain.text += "\nWrong Password!";
    }

    //End Screen
    public void EndScreen()
    {
        arrowScreen.SetActive(false);
        InstructionScreen.SetActive(true);
    }

    //converts password of integers to UI arrows that will be shown to screen when getting assigned a password
    private void SetArrows(int[] chosen)
    {
        //each arrow in the assign password screen gets casted into one of the 8 preset arrows created in initiate arrows
        for (int i = 0; i < instructArrows.Length; i++)
        {
            instructArrows[i].color = arrows[chosen[i]].color;
            instructArrows[i].transform.rotation = Quaternion.Euler(0, 0, arrows[chosen[i]].rotation);
        }
    }


}
