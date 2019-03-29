using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PasswordCompiler : MonoBehaviour
{

    public PasswordHolder passHolder;
    public ViewManager view;
    public int stage = 0;

    string[] example = {"Bank", "Email", "Carleton" };
    int[] order = new int[3];

    private void Start()
    {
        for(int i=0;i<order.Length;i++)
            order[i] = i;
        RandomOrder(order);

        for (int i = 0; i < order.Length; i++)
            print(order[i]);

        CreateExample();
        NextScreen();
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
                print("getting password: "+stage / 2);
                view.ShowPassword(example[stage/2], GetPassword(example[stage/2]));
                break;
            case 1:
            case 3:
            case 5:
                view.EnterPassword(example[(int)Mathf.Floor(stage/2)]);
                break;
            case 6:
            case 7:
            case 8:
                break;
            case 9:
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
                print("correct");
                stage++;
                NextScreen();
            }
            //reshows password
            else
            {
                print("idiot");
                stage--;
                NextScreen();
            }
        }
    }
    
    void AddPassword(string domain, int[] pass)
    {
        passHolder.AddPassword(domain, pass);
    }

    void AddPassword(int[] pass)
    {
        passHolder.AddPassword("test", pass);
    }

    int[] GetPassword(string domain) {
        return passHolder.GetPassword((domain));
    }

    public void PrintRealPassword() {
       int [] a = GetPassword(example[(int)Mathf.Floor(stage / 2)]);
        string real="";
        for (int i = 0; i < a.Length; i++)
        {
            real+=a[i];
        }
        print("real: " + real);
    }
}
