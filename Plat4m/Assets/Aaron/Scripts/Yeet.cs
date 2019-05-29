﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yeet : MonoBehaviour
{
    public GameObject Player2;
    public GameObject arrow;
    public bool isYeeting;
    public GameObject ArrowTip;
    public Canvas Landing;

    float YeetForce = 17;
    Animation arrowMove;
    PlayerMovement Player;
    Vector3 direction;
    RaycastHit hit;
    bool isAirborne;

    // Use this for initialization
    void Start()
    {
        Player = GetComponent<PlayerMovement>();
        isYeeting = false;
        isAirborne = false;
        arrow.gameObject.SetActive(false);
        Landing.gameObject.SetActive(false);       
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Y))
        {
            possibleYeets();
        }
        else if(Input.GetKeyUp(KeyCode.Y) && isYeeting)
        {
            Throw();
        }

        if(!Player.isGrounded)
        {
            isAirborne = true;
            PredictedLanding();
        }

        else if(!isAirborne)
        {
            Landing.gameObject.SetActive(false);
        }
        
    }

    public void Throw()
    {
        GetComponent<Rigidbody>().AddForce(- direction * YeetForce * 100, ForceMode.Force);

        isYeeting = false;
        Player.isGrounded = false;
        arrow.gameObject.SetActive(false);

        Time.timeScale = 1;
    }

    public void possibleYeets()
    {
        if (!isYeeting)
        {
            var heading = transform.position - Player2.transform.position;

            var distance = heading.magnitude;
            var direction = heading / distance; // This is now the normalized direction.

            if (heading.sqrMagnitude < 3 * 3)
            {
                isYeeting = true;
                arrow.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
        else CalculateDirection();
    }

    public void CalculateDirection()
    {
        direction = arrow.transform.up;

        float rot_z = Input.GetAxis("Mouse X") * 2;

        arrow.transform.RotateAround(ArrowTip.transform.position, new Vector3(0,0,1), - rot_z);
    }

    public void PredictedLanding()
    {
        if(isAirborne)
        {
            if (Physics.Raycast(transform.position, -transform.up, out hit, Mathf.Infinity))
            {
                Landing.gameObject.SetActive(true);

                Landing.transform.position = new Vector3(Landing.transform.position.x, hit.point.y + 0.2f, Landing.transform.position.z);

                isAirborne = false;
            }
        }
    }
}
