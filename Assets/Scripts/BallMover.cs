using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMover : MonoBehaviour
{
    Rigidbody2D rb;
    //8 long contains rotations where ball can move, 0 is starting point
    private float currentRotation=0;
    //how far and fast ball moves/goes
    public float time=2;
    public float speed=4;
    //this is method which moves ball
    private Coroutine moveBall;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//rigid body is  a helpful class which contains variables/code for moving/collisions, change this velocity will cause ball to move automatically
    }

    //resets ball position and variables
    private void ResetBallPosition()
    {
        if(moveBall!=null)
            StopCoroutine(moveBall);
        transform.position = new Vector3(0, 0, 0);
        rb.velocity = new Vector3(0, 0, 0);
    }

    //gets arrow directoin as i and moves ball;
    public void Direction(int i) {
        ResetBallPosition();
        currentRotation = 45*i;
        StartCoroutine("MoveBall");
    }

    //this is cooroutine, this method is done in real time
    IEnumerator MoveBall()
    {
        //sets velocity of ball
        rb.velocity = speed*(Vector2)(Quaternion.Euler(0, 0, currentRotation) * Vector2.up);
        //delays ending the method
        for (int i = 0; i < time; i++)
            yield return new WaitForSeconds(.1f);//everytime it runs this method, it pauses the method for .1 seconds
        //resets balls stats
        transform.position = new Vector3(0, 0, 0);
        rb.velocity = new Vector3(0, 0, 0);
    }

}
