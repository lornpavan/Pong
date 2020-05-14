using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject Player;
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode tiltDown = KeyCode.D;
    public KeyCode tiltUp = KeyCode.A;
    public float speed = 10.0f;
    public float boundY = 2.25f;
    private Rigidbody2D rb2d;
    private float degrees = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var vel = rb2d.velocity;
        if (Input.GetKey(moveUp))
        {
            vel.y = speed;
        }
        else if (Input.GetKey(moveDown))
        {
            vel.y = -speed;
        }
        else
        {
            vel.y = 0;
        }
        if (Input.GetKey(tiltUp) && degrees < 15)
        {
            Player.transform.rotation = Quaternion.Euler(Vector3.forward * degrees);
            degrees++;
        }
        if (Input.GetKey(tiltDown) && degrees > -15)
        {
            Player.transform.rotation = Quaternion.Euler(Vector3.forward * degrees);
            degrees--;
        }
        else if (!Input.GetKey(tiltDown) && !Input.GetKey(tiltUp) && degrees != 0)
        {
            Player.transform.rotation = Quaternion.Euler(Vector3.forward * degrees);
            if (degrees < 0)
                degrees++;
            else if (degrees > 0)
                degrees--;
        }
        rb2d.velocity = vel;

        var pos = transform.position;
        if (pos.y > boundY)
        {
            pos.y = boundY;
        }
        else if (pos.y < -boundY)
        {
            pos.y = -boundY;
        }
        transform.position = pos;
    }
}
