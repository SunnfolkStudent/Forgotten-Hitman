using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{

    [SerializeField] private float RaycastDistance = 10f;
    public LayerMask interactable;
    public LayerMask showerLayer;
    private bool canInteract;
    public Input _Input;
    public GameObject interactCrossHair;
    private bool canShowInteractCrosshair;
    private bool canInteractWithShower;
    [HideInInspector]public bool hasInteractedWithShower;

    private void Start()
    {
        canInteract = false;
        hasInteractedWithShower = false;
        canInteractWithShower = false;
        interactCrossHair.SetActive(false);
    }

    private void Update()
    {
        DetectInteractable();
        Interact();
        InteractShower();
        ShowInteractCrosshair();
    }

    private void DetectInteractable()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, RaycastDistance, interactable))
        {
            canInteract = true;
            canShowInteractCrosshair = true;
        }
        else
        {
            canInteract = false;
            canShowInteractCrosshair = false;
        }
    }

    private void Interact()
    {
        if (canInteract && _Input.Interact)
        {
            print("interacted");
        }
    }

    private void InteractShower()
    {
        RaycastHit showerHit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out showerHit,
            RaycastDistance, showerLayer))
        {
            canShowInteractCrosshair = true;
            canInteractWithShower = true;
        }

        if (canInteractWithShower && _Input.Interact)
        {
            hasInteractedWithShower = true;
        }
    }

    private void ShowInteractCrosshair()
    {
        if (canShowInteractCrosshair)
        {
            interactCrossHair.SetActive(true);
        }
        else
        {
            interactCrossHair.SetActive(false);
        }
    }
}
