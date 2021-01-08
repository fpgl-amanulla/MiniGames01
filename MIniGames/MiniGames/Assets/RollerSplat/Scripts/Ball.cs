using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball instance;

    private Rigidbody rb;
    public float speed = 15;

    public int minSwipeRecognition = 500;

    private bool isTravelling;
    private Vector3 travelDirection;


    private Vector2 swipePosLastframe;
    private Vector2 swipePosCurrentframe;
    private Vector2 currentSwipe;

    private Vector3 nextCollPos;

    private Color selectedColor;
    public Material mat;

    public bool isGameStarted = false;

    private void Start()
    {
        if (instance == null)
            instance = this;

        rb = GetComponent<Rigidbody>();
        selectedColor = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        //mat = GetComponent<MeshRenderer>().material;
        //GetComponent<MeshRenderer>().material.color = selectedColor;
        mat.color = selectedColor;
    }

    private void FixedUpdate()
    {
        if (isTravelling)
        {
            rb.velocity = travelDirection * speed;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position - (Vector3.up / 2), .05f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            GroundPiece ground = hitColliders[i].transform.GetComponent<GroundPiece>();

            if (ground && !ground.isColored)
            {
                ground.Colored(selectedColor);
                if (ground.GetComponentInChildren<DustparticleSystem>() == null)
                    return;

                ground.GetComponentInChildren<DustparticleSystem>().PlayDust();
            }
            if (ground && isTravelling)
            {
                if (ground.GetComponentInChildren<DustparticleSystem>() == null)
                    return;
                ground.GetComponentInChildren<DustparticleSystem>().PlayDust();
            }
            //if (GameManager.singleton.levelClear)
            //{
            //    if (ground.GetComponentInChildren<DustparticleSystem>() != null)
            //        ground.GetComponentInChildren<DustparticleSystem>().PlayDust();
            //}


            i++;
        }



        if (nextCollPos != Vector3.zero)
        {
            if (Vector3.Distance(transform.position, nextCollPos) < 1)
            {
                isTravelling = false;
                travelDirection = Vector3.zero;
                nextCollPos = Vector3.zero;
            }
        }

        if (isTravelling)
            return;

        if (Input.GetMouseButton(0) && isGameStarted)
        {
            swipePosCurrentframe = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if (swipePosLastframe != Vector2.zero)
            {
                currentSwipe = swipePosCurrentframe - swipePosLastframe;

                if (currentSwipe.sqrMagnitude < minSwipeRecognition)
                    return;
                currentSwipe.Normalize();

                if (currentSwipe.x > -.5f && currentSwipe.x < .5f)
                {
                    SetDestination(currentSwipe.y > 0 ? Vector3.forward : Vector3.back);
                }
                if (currentSwipe.y > -.5f && currentSwipe.y < .5f)
                {
                    SetDestination(currentSwipe.x > 0 ? Vector3.right : Vector3.left);
                }
            }

            swipePosLastframe = swipePosCurrentframe;
        }

        if (Input.GetMouseButtonUp(0))
        {
            swipePosLastframe = Vector2.zero;
            currentSwipe = Vector2.zero;
        }
    }

    private void SetDestination(Vector3 direction)
    {
        travelDirection = direction;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 100f))
        {
            nextCollPos = hit.point;
        }
        isTravelling = true;
    }
}
