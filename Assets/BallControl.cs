using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{

    private Rigidbody2D rb2d;
    public float pushForce = .2f;
    private static int isStuck = 0;
    public static float thrust = .8f;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Invoke("GoBall", 2);
    }

    // Update is called once per frame
    void Update()
    {
        // rb2d.velocity = Vector2.;
        if (isStuck > 10)
        {
            ResetBall();
            Invoke("GoBall", 1);
            isStuck = 0;
        }
    }

    void GoBall()
    {
        float rand = Random.Range(0, 2);
        if (rand < 1)
        {
            rb2d.AddForce(new Vector2(20, -15)*thrust);
        }
        else
        {
            rb2d.AddForce(new Vector2(-20, -15)*thrust);
        }
    }

    void ResetBall()
    {
        rb2d.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    void RestartGame()
    {
        ResetBall();    
        Invoke("GoBall", 2);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            /*Vector2 vel;
            vel.x = rb2d.velocity.x;
            vel.y = (rb2d.velocity.y / 2) + (coll.collider.attachedRigidbody.velocity.y / 3);
            rb2d.velocity = vel;*/
            var opposite = -rb2d.velocity;
            rb2d.AddForce(opposite * Time.deltaTime);
            isStuck = 0;
        }

        else if (coll.collider.CompareTag("TopWall"))
        {
            rb2d.AddForce(Vector2.down*.5f);
            isStuck++;
        }

        else if (coll.collider.CompareTag("BottomWall"))
        {
            rb2d.AddForce(Vector2.up*.5f);
            isStuck++;
        }
    }
}
