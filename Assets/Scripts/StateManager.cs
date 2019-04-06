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
        wrongPassword = StaticVariables.passwordAttempts;
        //gets random order for second half of assignment
        for(int i=0;i<order.Length;i++)
            order[i] = i;
        RandomOrder(order);

        //creates passwords
        //if dev true it puts it in developer mode
        bool dev = true;
        if(dev)
            CreateDevExample();
        else
            CreateExample();

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
        print("here " + stageChange);
        //sees if stage was changed
        stage += stageChange;

        if (stageChange == 0&&stage>2)
        {
            wrongPassword--;
            if (wrongPassword == 0)
            {
                stage++;
                wrongPassword = StaticVariables.passwordAttempts;
            }
        }

        switch (stage) {
            //re-submit CORRECTLY the assigned passwords
            case 0:
            case 1:
            case 2:
                view.ShowPasswordCode(true);
                view.EnterPassword(example[stage], GetPassword(example[stage]),true, true,-1);
                log.StartTimer();
                break;
            //Have 3 tries to get correct password;
            case 3:
            case 4:
            case 5:
                view.ShowPasswordCode(false);
                view.EnterPassword(example[stage-3], GetPassword(example[stage-3]), true, true, wrongPassword);
                log.StartTimer();
                break;
                //end game and export to Excel File
            case 6:
                view.TextScreen("You have completed the Password Entry.  \nPlease fill out the associated Survey");
                log.ExportLog();
                break;
            default:
                print("hit default and I shouldnt be here");
                break;
            
        }
    }

    public bool CheckCorrectPassword(int[] enteredPassword) {
        int currentStage = stage;
        if (stage > 2)
            currentStage -= 3;

        if (passHolder.CheckLogin(example[currentStage], enteredPassword))
        {
            previousAttempt = true;
        }
        //reshows password
        else
        {
            previousAttempt = false;
        }
        return previousAttempt;
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
