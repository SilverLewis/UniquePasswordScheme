using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewManager : MonoBehaviour
{
    Arrow[] arrows;
    public Image[] instructArrows, passwordEntered, PasswordExample;
    public Text passDomain;
    public Text arrowPassDomain;
    public Text notification;
    public Sprite arrow, noArrow;
    public GameObject arrowScreen, InstructionScreen, GameOverScreen, PasswordCode;

    
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

    //password entering screen, manipulates UI by setting the pages to active (true) or inactive (false)
    public void EnterPassword(string domain, int[] password, bool showPassword, bool correct, int AttemptsRemaining)
    {
        ScreenController(0);

       arrowPassDomain.text = "Your " + domain + "'s password is:";

        if (showPassword)
            ShowPasswordArrows(password);

        //if 0 doesnt show attempts left
        if(AttemptsRemaining>0)
            arrowPassDomain.text += "\nYou have " + AttemptsRemaining + " remaining";
        //if last entry was wrong
        if (!correct)
            arrowPassDomain.text += "\nWrong Password!";
    }

    //End Screen
    public void TextScreen(string text)
    {
        notification.text = text;
        ScreenController(2);
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

    //controlls which screen is online
    private void ScreenController(int i) {
        arrowScreen.SetActive(0 == i);
        InstructionScreen.SetActive(1 == i);
        GameOverScreen.SetActive(2 == i);
    }

    private void ShowPasswordArrows(int[]password) {
        for (int j = 0; j < StaticVariables.passwordLength; j++)
        {
            PasswordExample[j].sprite = arrow;
            PasswordExample[j].color = arrows[password[j]].color;
            PasswordExample[j].transform.rotation = Quaternion.Euler(0, 0, arrows[password[j]].rotation);
        }
    }

    public void ShowEnteredArrows(int pass, int[] enteredCode)
    {
        for (int j = 0; j < StaticVariables.passwordLength; j++)
        {
            if (j < pass)
            {
                passwordEntered[j].sprite = arrow;
                passwordEntered[j].color = arrows[enteredCode[j]].color;
                passwordEntered[j].transform.rotation = Quaternion.Euler(0, 0, arrows[enteredCode[j]].rotation);
            }
            else
            {
                passwordEntered[j].sprite = noArrow;
                passwordEntered[j].color = Color.white;
                passwordEntered[j].transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    public void ShowPasswordCode(bool active) {
        PasswordCode.SetActive(active);
    }

}
