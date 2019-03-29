using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PasswordCompiler : MonoBehaviour
{
    // Start is called before the first frame update
    int passwordLength = 7; 

    int[] password;
    int currentDirection = 0;
    public PasswordHolder passHolder;
    public ViewManager view;
    int[] randomPassword;

    int stage = 0;
    string[] example = {"Bank", "Email", "Carleton" };

    int screenCount = 0;

    private void Start()
    {
        password = new int[passwordLength];
        randomPassword = new int[passwordLength];

        CreateExample();
        NextScreen();
    }


    private void Update()
    {
        if (Input.anyKey)
        {
            if (stage%2==0&&stage<6)
            {
                print("here?");
                stage++;
                NextScreen();
            }
        }
    }


    private void CreateExample()
    {
        int[] newPassword = new int[passwordLength];
        for (int i = 0; i < example.Length; i++)
        {
            for (int j = 0; j < newPassword.Length; j++)
                newPassword[j] = Random.Range(0, 8);
            AddPassword(example[i], newPassword);
        }
    }

    void NextScreen()
    {
        print("here");
        //will only get in here at 1,3,5
        if (stage < 6 && currentDirection == passwordLength - 1) {

            PrintRealPassword();
            PrintEnteredPassword();

            //entered correct password
            if (passHolder.CheckLogin(example[(int)Mathf.Floor(stage / 2)], password))
            {
                print("correct");
                stage++;
            }
            //reshows password
            else
            {
                print("idiot");
                stage--;
            }
        }

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

    public void DirectionChosen(int i)
    {
        if (currentDirection == passwordLength-1)
        {
            password[currentDirection] = i;
            NextScreen();
            currentDirection = 0;

        }
        else if (currentDirection < passwordLength - 1)
        {
            password[currentDirection] = i;
            currentDirection++;
        }
    }

    void PrintRealPassword() {
       int [] a = GetPassword(example[(int)Mathf.Floor(stage / 2)]);
        string real="";
        for (int i = 0; i < a.Length; i++)
        {
            real+=a[i];
        }
        print("real: " + real);
    }
    void PrintEnteredPassword()
    {
        int[] a =password;
        string fake = "";
        for (int i = 0; i < a.Length; i++)
        {
            fake += a[i];
        }
        print("fake: " + fake);
    }


}
