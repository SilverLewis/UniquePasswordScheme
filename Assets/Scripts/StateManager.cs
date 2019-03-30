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

    private void Start()
    {
        previousAttempt = true;

        for(int i=0;i<order.Length;i++)
            order[i] = i;
        RandomOrder(order);

       // CreateExample();
        CreateDevExample();
        NextScreen();
    }

    private void CreateDevExample() {
        int[] newPassword = new int[StaticVariables.passwordLength];
        for (int i = 0; i < example.Length; i++)
        {
            for (int j = 0; j < newPassword.Length; j++)
                newPassword[j] = 0;
            AddPassword(example[i], newPassword);
        }
    }

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

    private void RandomOrder(int[] order) {
        for(int i=0;i<order.Length;i++)
        {
            int k = Random.Range(0, order.Length-1);
            int value = order[k];
            order[k] = order[i];
            order[i] = value;
        }
    }

    public void NextScreen()
    {
        switch (stage) {
            case 0:
            case 2:
            case 4:
                view.ShowPassword(example[stage/2], GetPassword(example[stage/2]), previousAttempt);
                break;
            case 1:
            case 3:
            case 5:
                view.EnterPassword(example[(int)Mathf.Floor(stage/2)], true,-1);
                log.StartTimer();
                break;
            case 6:
            case 7:
            case 8:
                print("prev: " + previousAttempt);
                view.EnterPassword(example[order[stage - 6]], previousAttempt,3-wrongPassword);
                break;
            case 9:
                view.EndScreen();
                log.PrintLog();
                break;
            default:
                print("hit default and I shouldnt be here");
                break;
            
        }
    }

    public void CheckPassword(int[] enteredPassword) {
        if (stage < 6 && stage % 2 == 1)
        {
            //entered correct password
            if (passHolder.CheckLogin(example[(int)Mathf.Floor(stage / 2)], enteredPassword))
            {
                stage++;
                previousAttempt = true;
                NextScreen();
            }
            //reshows password
            else
            {
                stage--;
                previousAttempt = false;
                NextScreen();
            }
            log.EndTimer(previousAttempt);
        }
        else if(stage<9)
        {
            print("checking Password");
            if (passHolder.CheckLogin(example[order[stage - 6]], enteredPassword)) {
                stage++;
                wrongPassword = 0;
                previousAttempt = true;
                log.EndTimer(previousAttempt);
                NextScreen();
            }
            else{
                wrongPassword++;
                previousAttempt = false;
                log.EndTimer(previousAttempt);
                if (wrongPassword >= 3)
                {
                    wrongPassword = 0;
                    previousAttempt = true;
                    stage++;
                }
                NextScreen();
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
