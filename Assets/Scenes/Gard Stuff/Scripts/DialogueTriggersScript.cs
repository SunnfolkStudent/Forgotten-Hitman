using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggersScript : MonoBehaviour
{
    public GameObject TriggerZone1; // are you ok? dialogue
    public GameObject TriggerZone2; // Basement keys gone

    private AudioSource _AudioSource;
    public AudioClip AreYouOK;
    public AudioClip BasementKeyGone;

    private bool hasTriggeredZone1;
    private bool hasTriggeredZone2;


    private void Start()
    {
        TriggerZone1.SetActive(true);
        TriggerZone2.SetActive(false);
        _AudioSource = GetComponent<AudioSource>();
        hasTriggeredZone1 = false;
        hasTriggeredZone2 = false;
    }

    private void Update()
    {
        TurnOffZones();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TriggerZone1")
        {
            _AudioSource.PlayOneShot(AreYouOK);
            TriggerZone2.SetActive(true);
            hasTriggeredZone1 = true;
        }

        if (other.gameObject.tag == "TriggerZone2")
        {
            _AudioSource.PlayOneShot(BasementKeyGone);
            hasTriggeredZone2 = true;
        }
    }

    private void TurnOffZones()
    {
        if (hasTriggeredZone1)
        {
            TriggerZone1.SetActive(false);
        }

        if (hasTriggeredZone2)
        {
            TriggerZone2.SetActive(false);
        }
    }
}
