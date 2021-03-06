﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class God : MonoBehaviour {

    private static God instance;
    public static God Get
    {
        get
        {
            return instance;
        }
    }

    private static List<string> tagsPlatforms
    {
        get
        {
            return new List<string>
            {
                "upside",
                "leftside",
                "rightside",
                "downside",
                "platform"
            };
        }
    }

    private Animator animator;
    private AudioSource source;

    private float time = 0f;
    private bool block = false;
    private Quaternion minRotation;
    private Quaternion maxRotation;

    [Tooltip("Contrôleur lumière")]
    public Transform lightController;
    [Tooltip("Lumière divine")]
    public Light sight;
    [Tooltip("Position de départ")]
    public Transform startPosition;
    [Tooltip("Position de fin")]
    public Transform endPosition;
    [Tooltip("Vitesse de déplacement")]
    public float moveSpeed = 1f;
    [Tooltip("Angle minimum")]
    public float minAngle = -30f;
    [Tooltip("Angle maximum")]
    public float maxAngle = 30f;
    [Tooltip("Vitesse de rotation")]
    public float rotationSpeed = 3f;
    [Tooltip("Sons de joie")]
    public AudioClip[] happySounds;
    [Tooltip("Sons de colère")]
    public AudioClip[] angrySounds;

	// Use this for initialization
	void Start () {
        if (instance == null)
        {
            instance = this;
        }
        sight.type = LightType.Spot;

        Renderer renStart = startPosition.GetComponent<Renderer>();
        if (renStart != null)
        {
            renStart.enabled = false;
        }
        Renderer renEnd = endPosition.GetComponent<Renderer>();
        if (renEnd != null)
        {
            renEnd.enabled = false;
        }

        minRotation = Quaternion.AngleAxis(minAngle, Vector3.forward);
        maxRotation = Quaternion.AngleAxis(maxAngle, Vector3.forward);

        float z = transform.position.z;
        startPosition.position = new Vector3(startPosition.position.x, startPosition.position.y, z);
        endPosition.position = new Vector3(endPosition.position.x, endPosition.position.y, z);
        transform.position = (startPosition.position + endPosition.position) / 2f;
        lightController.rotation = Quaternion.identity;

        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (block)
        {
            //Nice to have
        }
        else
        {
            float delta = Time.deltaTime;
            time += delta;
            float coefPosition = (1 + Mathf.Sin(moveSpeed * time)) / 2f;
            float coefRotation = (1 + Mathf.Sin(rotationSpeed * time)) / 2f;

            transform.position = Vector3.Lerp(startPosition.position, endPosition.position, coefPosition);
            lightController.rotation = Quaternion.Lerp(minRotation, maxRotation, coefRotation);
        }
	}

    public bool IsUnderGodView(Vector3 pos)
    {
        Vector3 direction = pos - sight.transform.position;
        Vector3 forward = sight.transform.forward;

        if (Vector3.Angle(direction, forward) > sight.spotAngle / 2f)
        {
            return false;
        }

        RaycastHit hit;
        if (Physics.Raycast(sight.transform.position, direction, out hit))
        {
            if (tagsPlatforms.Contains(hit.transform.tag))
            {
                return direction.magnitude < hit.distance;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
    }

    public IEnumerator Happy()
    {
        source.PlayOneShot(happySounds[Random.Range(0, happySounds.Length)]);
        animator.SetBool("colere", false);
        animator.SetBool("content", true);
        yield return new WaitForSeconds(5f);
        animator.SetBool("content", false);
    }

    public IEnumerator Angry()
    {
        source.PlayOneShot(angrySounds[Random.Range(0, angrySounds.Length)]);
        animator.SetBool("content", false);
        animator.SetBool("colere", true);
        yield return new WaitForSeconds(5f);
        animator.SetBool("colere", false);
    }
}
