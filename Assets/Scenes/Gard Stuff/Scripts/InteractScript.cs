using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{

    [SerializeField] private float RaycastDistance = 10f;
    public LayerMask interactable;
    public LayerMask showerLayer;
    public LayerMask TvLayer;
    private bool canInteract;
    public Input _Input;
    public GameObject interactCrossHair;
    private bool canShowInteractCrosshair;
    
    private RaycastHit hit;
    
    private bool canInteractWithShower;
    [HideInInspector]public bool hasInteractedWithShower;

    private bool canInteractWithTV;
    [HideInInspector] public bool hasInteractedWithTV;

    private void Start()
    {
        canInteract = false;
        hasInteractedWithShower = false;
        canInteractWithShower = false;
        canInteractWithTV = false;
        hasInteractedWithTV = false;
        interactCrossHair.SetActive(false);
    }

    private void Update()
    {
        DetectInteractable();
        Interact();
        InteractShower();
        ShowInteractCrosshair();
        InteractTV();
    }

    private void DetectInteractable()
    {
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
            hit.transform.GetComponent<AnimationController>().Interacting();
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
        else
        {
            canInteractWithShower = false;
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

    private void InteractTV()
    {
        RaycastHit TVhit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),out TVhit,RaycastDistance,TvLayer))
        {
            canInteractWithTV = true;
            canShowInteractCrosshair = true;
        }

        if (canInteractWithTV && _Input.Interact)
        {
            hasInteractedWithTV = true;
            print("has tv indeed");
        }
    }
}
