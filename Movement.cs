using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;

    public float walkS = 4f;
    public float speedLim = 0.7f;
    public float inputHor;
    public float inputVer;

    Animator animator;
    string currentAnimState;
    const string Player_Idle = "Player_Idle";
    const string Player_Walk_Down = "Player_Run_Down";
    const string Player_Walk_Up = "Player_Run_Up";
    const string Player_Walk_Left = "Player_Run_Left";
    const string Player_Walk_Right = "Player_Run_Right";

    public AudioSource footstepSound; // Reference to the AudioSource component for the footstep sound effect

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputHor = Input.GetAxisRaw("Horizontal");
        inputVer = Input.GetAxisRaw("Vertical");

        // Check if any movement input is being received
        if (inputHor != 0 || inputVer != 0)
        {
            // Play the footstep sound if it's not already playing
            if (!footstepSound.isPlaying)
            {
                footstepSound.Play();
            }
        }
        else
        {
            // Stop the footstep sound if it's playing
            if (footstepSound.isPlaying)
            {
                footstepSound.Stop();
            }
        }
    }

    private void FixedUpdate()
    {
        if (inputHor != 0 || inputVer != 0)
        {
            if (inputHor != 0 && inputVer != 0)
            {
                inputHor *= speedLim;
                inputVer *= speedLim;
            }

            rb.velocity = new Vector2(inputHor * walkS, inputVer * walkS);

            if (inputHor < 0)
            {
                ChangeAnimState(Player_Walk_Right); // Flip
            }
            if (inputHor > 0)
            {
                ChangeAnimState(Player_Walk_Left); // Not Flip
            }
            if (inputVer > 0)
            {
                ChangeAnimState(Player_Walk_Up);
            }
            if (inputVer < 0)
            {
                ChangeAnimState(Player_Walk_Down);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            ChangeAnimState(Player_Idle);
        }
    }

    void ChangeAnimState(string newState)
    {
        if (currentAnimState == newState) return;

        animator.Play(newState);

        currentAnimState = newState;
    }
}
