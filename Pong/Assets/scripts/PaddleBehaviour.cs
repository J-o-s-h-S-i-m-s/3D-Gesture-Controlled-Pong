﻿using UnityEngine;
using System.Collections;

public class PaddleBehaviour : MonoBehaviour
{
    public PaddleSpeed paddleSpeed;

    private float paddleSpeedFactor;

    public PaddleUser paddleUser;

    private float fixedXPositionOfPaddle;

    private const float fixedYPositionOfPaddle = 0.5F;

    private float lastZPositionOfPaddle;

    private float updatedZPositionOfPaddle;

    private float movementFromUserInput;

    private const float movementAcrossxAxis = 0;

    private const float movementAcrossYAxis = 0;

    private float movementAcrossZAxis;

    public GameObject ball;

    private Vector3 positionOfBall;

    private void Start()
    {
        fixedXPositionOfPaddle = GetComponent<Transform>().position.x;

        bool isAI = false;
        if (paddleUser == PaddleUser.AI)
        {
            isAI = true;
        }
        determinePaddleSpeedFactor(isAI);
    }

    private void determinePaddleSpeedFactor(bool isAI)
    {
        if (isAI)
        {
            switch (paddleSpeed)
            {
                case PaddleSpeed.Slow:
                    paddleSpeedFactor = 0.5F;
                    break;
                case PaddleSpeed.Moderate:
                    paddleSpeedFactor = 0.25F;
                    break;
                case PaddleSpeed.Fast:
                    paddleSpeedFactor = 0.125F;
                    break;
            }
        }
        else
        {
            switch (paddleSpeed)
            {
                case PaddleSpeed.Slow:
                    paddleSpeedFactor = 2.5f;
                    break;
                case PaddleSpeed.Moderate:
                    paddleSpeedFactor = 5;
                    break;
                case PaddleSpeed.Fast:
                    paddleSpeedFactor = 10;
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        switch (paddleUser)
        {
            case PaddleUser.Player01:
                movementFromUserInput = Input.GetAxis("Player01");
                movePlayerPaddle(movementFromUserInput);
                break;
            case PaddleUser.Player02:
                movementFromUserInput = Input.GetAxis("Player02");
                movePlayerPaddle(movementFromUserInput);
                break;
            case PaddleUser.AI: 
                moveAIPaddle();
                break;
        }
    }

    private void movePlayerPaddle(float userInput)
    {
        movementAcrossZAxis = userInput * paddleSpeedFactor * Time.deltaTime;
        
        transform.position += new Vector3(
            movementAcrossxAxis, movementAcrossYAxis, movementAcrossZAxis);
    }

    private void moveAIPaddle()
    {
        updatedZPositionOfPaddle = Mathf.SmoothDamp(
            transform.position.z, 
            ball.transform.position.z, 
            ref lastZPositionOfPaddle, 
            paddleSpeedFactor);

        transform.position = new Vector3(
            fixedXPositionOfPaddle,
            fixedYPositionOfPaddle,
            updatedZPositionOfPaddle);
    }
}

public enum PaddleUser
{
    AI, Player01, Player02
}

public enum PaddleSpeed
{
    Slow, Moderate, Fast
}
