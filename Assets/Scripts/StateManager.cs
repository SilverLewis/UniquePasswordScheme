using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateManager : MonoBehaviour
{

    public PasswordHolder passHolder;
    public ViewManager view;
    public StatLogger log;

    public int stage = 0;
    public int wrongPassword;
    bool previousAttempt;

    string[] example = {"Bank", "Email", "Carleton" };
    int[] order = new int[3];

    //runs on start of program
    private void Start()
    {
        previousAttempt = true;

        //gets random order for second half of assignment
        for(int i=0;i<order.Length;i++)
            order[i] = i;
        RandomOrder(order);

        //creates passwords
       //CreateExample();
        CreateDevExample();

        //sets UI
        NextScreen(0);
    }

    //creates passwords of ONLY 0's for testing purposes
    private void CreateDevExample() {
        int[] newPassword = new int[StaticVariables.passwordLength];
        for (int i = 0; i < example.Length; i++)
        {
            for (int j = 0; j < newPassword.Length; j++)
                newPassword[j] = 0;
            AddPassword(example[i], newPassword);
        }
    }
    //creates random passwords;
    private void CreateExample()
    {
        int[] newPassword = new int[StaticVariables.passwordLength];
        for (int i = 0; i < example.Length; i++)
        {
            for (int j = 0; j < newPassword.Length; j++)
                newPassword[j] = Random.Range(0, StaticVariables.passwordOptions);
            AddPassword(example[i], newPassword);
        }
    }

    //randomizes given array;
    private void RandomOrder(int[] order) {
        for(int i=0;i<order.Length;i++)
        {
            int k = Random.Range(0, order.Length-1);
            int value = order[k];
            order[k] = order[i];
            order[i] = value;
        }
    }

    //crux of class, determines UI based on varable Stage
    public void NextScreen(int stageChange)
    {
        //sees if stage was changed
        stage += stageChange;

        switch (stage) {
            //assign password screens
            case 0:
            case 2:
            case 4:
                view.ShowPassword(example[stage/2], GetPassword(example[stage/2]), previousAttempt);
                break;
            //re-submit CORRECTLY the assigned passwords
            case 1:
            case 3:
            case 5:
                view.EnterPassword(example[(int)Mathf.Floor(stage/2)], true,-1);
                log.StartTimer();
                break;
            //Have 3 tries to get correct password;
            case 6:
            case 7:
            case 8:
                view.EnterPassword(example[order[stage - 6]], previousAttempt,3-wrongPassword);
                log.StartTimer();
                break;
                //end game and export to Excel File
            case 9:
                view.EndScreen();
                log.ExportLog();
                break;
            default:
                print("hit default and I shouldnt be here");
                break;
            
        }
    }

    //checks if password obtained by InputManager is correct
    public void CheckPassword(int[] enteredPassword) {
        //in assigning and resubmitting screens
        if (stage < 6 && stage % 2 == 1)
        {
            //entered correct password
            if (passHolder.CheckLogin(example[(int)Mathf.Floor(stage / 2)], enteredPassword))
            {
                previousAttempt = true;
                NextScreen(1);
            }
            //reshows password
            else
            {
                previousAttempt = false;
                NextScreen(-1);
            }
            log.EndTimer(previousAttempt);
        }
        //in recall password within 3 attemps screens
        else if(stage<9)
        {
            //if entered correct password
            if (passHolder.CheckLogin(example[order[stage - 6]], enteredPassword)) {
                wrongPassword = 0;
                previousAttempt = true;
                NextScreen(1);
                log.EndTimer(previousAttempt);
            }
            //if entered incorrect password
            else{
                wrongPassword++;
                log.EndTimer(false);
                //if entered 3 wrong passwords
                if (wrongPassword >= 3)
                {
                    wrongPassword = 0;
                    previousAttempt = true;
                    NextScreen(1);
                }
                //if not out of guesses
                else {
                    previousAttempt = false;
                    NextScreen(0); 
                }
            }
        }
    }
    
    void AddPassword(string domain, int[] pass)
    {
        passHolder.AddPassword(domain, pass);
    }

    int[] GetPassword(string domain) {
        return passHolder.GetPassword((domain));
    }

    public void PrintRealPassword() {
        int[] a;
        if (stage<6)
            a = GetPassword(example[(int)Mathf.Floor(stage / 2)]);
        else
            a = GetPassword(example[order[stage - 6]]);

        string real="";
        for (int i = 0; i < a.Length; i++)
        {
            real+=a[i];
        }
        print("real: " + real);
    }

}
