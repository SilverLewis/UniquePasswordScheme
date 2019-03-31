using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImputManager : MonoBehaviour
{
    int[] password;
    public StateManager manager;
    int passwordPlace = 0;

    //runs on creation
    private void Start()
    {
        //sets password to be the right length
        password = new int[StaticVariables.passwordLength];
    }

    //does this every frame
    private void Update()
    {
        //if user presses a key and its on the show password stage, move to next;
        if (Input.anyKey)
        {
            if (manager.stage % 2 == 0 && manager.stage < 6)
            {
                manager.NextScreen(1);
            }
        }
    }

    //gets input from button, each button corisponds from a number 0-7
    public void DirectionChosen(int i)
    {
        //adds button chosen to password entered
        password[passwordPlace] = i;

        //if password is full, resets password and check if what is entered is correct;
        if (passwordPlace == StaticVariables.passwordLength - 1)
        {
            manager.CheckPassword(password);
            passwordPlace = 0;
        }
        else if (passwordPlace < StaticVariables.passwordLength - 1)
        {
            passwordPlace++;
        }
    }
}
