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

    void Start()
    {
        arrows = new Arrow[StaticVariables.passwordOptions];
        InitiateArrows();
    }

    

    void InitiateArrows()
    {
        for (int i = 0; i < arrows.Length; i++)
            arrows[i] = new Arrow();

        arrows[0].color = Color.white;
        arrows[1].color = Color.yellow;
        arrows[2].color = Color.green;
        arrows[3].color = Color.black;
        arrows[4].color = Color.grey;
        arrows[5].color = Color.blue;
        arrows[6].color = Color.red;
        arrows[7].color = Color.magenta;

        for (int i = 0; i < arrows.Length; i++)
            arrows[i].rotation = 45 * i;
    }

    public void ShowPassword(string domain, int[] password, bool correct)
    {
        arrowScreen.SetActive(false);
        InstructionScreen.SetActive(true);
        SetArrows(password);
  
        if (correct)
            passDomain.text = "Your "+domain + "'s password is:";
        else
            passDomain.text = "Wrong Password!\nYour " + domain + "'s password is:";


    }

    public void EnterPassword(string domain, bool correct, int AttemptsRemaining)
    {
        arrowScreen.SetActive(true);
        InstructionScreen.SetActive(false);

       arrowPassDomain.text = "Your " + domain + "'s password is:";

        if(AttemptsRemaining>0)
            arrowPassDomain.text += "\nYou have " + AttemptsRemaining + " remaining";

        if (!correct)
            arrowPassDomain.text += "\nWrong Password!";
    }

    public void EndScreen()
    {
        arrowScreen.SetActive(false);
        InstructionScreen.SetActive(true);
    }


    private void SetArrows(int[] chosen)
    {
        for (int i = 0; i < instructArrows.Length; i++)
        {
            instructArrows[i].color = arrows[chosen[i]].color;
            instructArrows[i].transform.rotation = Quaternion.Euler(0, 0, arrows[chosen[i]].rotation);
        }
    }


}
