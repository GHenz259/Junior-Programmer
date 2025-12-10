using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongratScript : MonoBehaviour
{
    public TextMesh Text;
    public ParticleSystem SparksParticles;

    private List<string> TextToDisplay = new List<string>();

    private float RotatingSpeed;
    private float TimeToNextText;

    private int CurrentText;

    void Start()
    {
        TimeToNextText = 0.0f;
        CurrentText = 0;

        RotatingSpeed = 1.0f;

        // Initialize messages
        TextToDisplay.Add("Congratulation");
        TextToDisplay.Add("All Errors Fixed");

        // Display the first message
        Text.text = TextToDisplay[0];

        SparksParticles.Play();
    }

    void Update()
    {
        TimeToNextText += Time.deltaTime;

        if (TimeToNextText > 1.5f)
        {
            TimeToNextText = 0.0f;

            CurrentText++;

            if (CurrentText >= TextToDisplay.Count)
            {
                CurrentText = 0;
            }

            // MOVE THIS OUTSIDE SO TEXT UPDATES EVERY TIME
            Text.text = TextToDisplay[CurrentText];
        }
    }
}
