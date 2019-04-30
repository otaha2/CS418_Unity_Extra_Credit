using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public Text timeText;
    public Text speedText;
    public Button resetButton;
    public GameObject pickUpObject;

    private Rigidbody rb;
    private int count;
    private float timeCounter;
    private bool gameOver;
    private float speedMag;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winText.text = "";
        timeCounter = 0;
        gameOver = false;
        speedMag = 0;
        updateSpeed();
    }

    private void Update()
    {
        if (!gameOver)
        {
            timeCounter += Time.deltaTime;
            setTimeText();
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
        updateSpeed();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            //Deactivate Game Object
            other.gameObject.SetActive(false);
            //increment Count & String
            count = count + 1;
            if(count >= 12)
            {
                gameOver = true;
            }
            setCountText();
            Debug.Log("Collided with square");
        }
        else if(other.gameObject.CompareTag("Fast Patch"))
        {
            Debug.Log("Collided with fast patch");
            var vel = rb.velocity;

            rb.AddForce(vel * 5 * speed);
            updateSpeed();
        }
        else if (other.gameObject.CompareTag("Bouncer"))
        {
            rb.velocity = rb.velocity * -1;
        }
    }

    void setCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(gameOver)
        {
            winText.text = "You Win!";
        }
    }

    void setTimeText()
    {
        timeText.text = "Time: " + timeCounter.ToString();
    }

    void updateSpeed()
    {
        var vel = rb.velocity;
        var tmpSpeed = vel.magnitude;
        if(tmpSpeed > speedMag)
        {
            speedMag = tmpSpeed;
        }
        speedText.text = "Top Speed: " + speedMag.ToString();
    }
}
