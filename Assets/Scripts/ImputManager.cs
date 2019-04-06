using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImputManager : MonoBehaviour
{
    int[] password;
    public StateManager manager;
    public ViewManager view;
    public BallMover ball;
    int passwordPlace = 0;

    

    //runs on creation
    private void Start()
    {
        //sets password to be the right length
        password = new int[StaticVariables.passwordLength];

        view.ShowEnteredArrows(passwordPlace, password); 
    }

    //does this every frame
    private void Update()
    {/*
        //if user presses a key and its on the show password stage, move to next;
        if (Input.anyKey)
        {
            if (manager.stage % 2 == 0 && manager.stage < 6)
            {
                manager.NextScreen(1);
            }
        }*/
    }

    //gets input from button, each button corisponds from a number 0-7
    public void DirectionChosen(int i)
    {
        if (passwordPlace == 7)
            return;
        //moves ball in the center
        if(ball!=null)
            ball.Direction(i);
        //adds button chosen to password entered
        password[passwordPlace] = i;
        passwordPlace++;
        view.ShowEnteredArrows(passwordPlace, password);
        
    }

    public void CheckGuess() {
        if (passwordPlace == StaticVariables.passwordLength)
        {
            print(manager.CheckCorrectPassword(password));
        }
    }

    public void SubmitGuess() {
        if (passwordPlace == StaticVariables.passwordLength )
        {
            bool correct = manager.CheckCorrectPassword(password);
            passwordPlace = 0;
            if (correct)
                manager.NextScreen(1);
            else
                manager.NextScreen(0);
        }
        view.ShowEnteredArrows(passwordPlace, password);
    }

    public void UndoArrow() {
        if (passwordPlace > 0)
            passwordPlace--;
        view.ShowEnteredArrows(passwordPlace, password);
    }

    

}
