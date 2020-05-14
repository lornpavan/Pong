using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int PlayerScore1 = 0;
    public static int PlayerScore2 = 0;
    public GUISkin layout;
    GameObject theBall;
    private Rigidbody2D ballrb;
    public static float timeLeft = 30f;
    public Text TimerText;
    public Text notifyText;
    private int challenge = 0;
    private static float changeDirection = .5f;
    private static int percentChangeDirection;
    // Start is called before the first frame update
    void Start()
    {
        theBall = GameObject.FindGameObjectWithTag("ball");
        ballrb = theBall.GetComponent<Rigidbody2D>();
        percentChangeDirection = 3;
        notifyText.text = "Game Changes: \n";
    }

    public static void Score(string wallID)
    {
        if (wallID == "RightWall")
        {
            PlayerScore1++;
        }
        else
        {
            PlayerScore2++;
        }
    }

    void OnGUI()
    {
        GUI.skin = layout;
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + PlayerScore1);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + PlayerScore2);

        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
            PlayerScore1 = 0;
            PlayerScore2 = 0;
            theBall.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        }

        if (PlayerScore1 == 10)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "PLAYER ONE WINS");
            theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
        else if (PlayerScore2 == 10)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "PLAYER TWO WINS");
            theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
    }

    // Update is called once per frame
    void Update()
    {
        print(challenge);
       // print(timeLeft);
            if (timeLeft >= 0)
            timeLeft -= Time.deltaTime;
            TimerText.text = "Next Game Change In: " + (timeLeft).ToString("0");

        if (timeLeft <= 30 && timeLeft >= 29 || timeLeft <= 20 && timeLeft >= 19 || timeLeft <= 10 && timeLeft >= 9)
            print(ballrb.velocity);
    }

    void LateUpdate()
    {
        if (timeLeft < 0.5f)
        {
            //Do something useful or Load a new game scene depending on your use-case
            challenge++;
            if (challenge == 1)
            {
                //ballrb.mass = .08f;
                ballrb.AddForce(ballrb.velocity * 1.2f);
                BallControl.thrust += .2f;
                notifyText.text += "\n- Speed Increased \n";
            }
            else if (challenge == 2)
            {
                //30% chance to change direction every 10s 
                notifyText.text += "\n- 30% Chance to Change Direction Every 10 Seconds\n";
            }
            else if (challenge == 3)
            {
                //ballrb.mass = .07f;
                ballrb.AddForce(ballrb.velocity * 1.2f);
                BallControl.thrust += .4f;
                notifyText.text += "\n- Speed Increased\n";                
            }
            else if (challenge == 4)
            {
                //60% chance to change direction every 10s
                percentChangeDirection = 6;
                notifyText.text += "\n- 60% Chance to Change Direction Every 10 Seconds\n";
            }
            else if (challenge == 5)
            {
                //ballrb.mass = .06f;
                ballrb.AddForce(ballrb.velocity * 1.4f);
                BallControl.thrust += .6f;
                notifyText.text += "\n- Final Speed Increase\n";
            }
            timeLeft = 30f;
        }

        if (challenge >= 2)
        {
            if (timeLeft <= 30f && timeLeft >= 29.8f  || timeLeft <= 20f && timeLeft >= 19.8f ||
                timeLeft <= 10f && timeLeft >= 9.8f)
            {
                int range1 = Random.Range(1, 10);
                if (range1 <= percentChangeDirection)
                {
                    int range2 = Random.Range(1, 4);
                    if (range2 == 1)
                        ballrb.AddForce(Vector2.down * 1);
                    if (range2 == 2)
                        ballrb.AddForce(Vector2.up * 1);
                    if (range2 == 3)
                        ballrb.AddForce(Vector2.left * 1);
                    if (range2 == 4)
                        ballrb.AddForce(Vector2.right * 1);
                }
            }
        }
    }
}
