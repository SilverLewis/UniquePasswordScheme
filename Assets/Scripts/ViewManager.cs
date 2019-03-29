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

    public void ShowPassword(string domain, int[] password)
    {
        arrowScreen.SetActive(false);
        InstructionScreen.SetActive(true);
        SetArrows(password);
        passDomain.text = "Your "+domain + "'s password is:";
    }

    public void EnterPassword(string domain)
    {
        arrowScreen.SetActive(true);
        InstructionScreen.SetActive(false);
        arrowPassDomain.text = "Please enter your " + domain + "'s password";
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
