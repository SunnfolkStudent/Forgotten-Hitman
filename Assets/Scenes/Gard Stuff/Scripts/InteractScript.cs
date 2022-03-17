using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{

    [SerializeField] private float RaycastDistance = 10f;
    public LayerMask interactable;
    private bool canInteract;
    public Input _Input;
    public GameObject interactCrossHair;

    private void Start()
    {
        canInteract = false;
        interactCrossHair.SetActive(false);
    }

    private void Update()
    {
        DetectInteractable();
        Interact();
        
    }

    private void DetectInteractable()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, RaycastDistance, interactable))
        {
            canInteract = true;
            interactCrossHair.SetActive(true);
        }
        else
        {
            canInteract = false;
            interactCrossHair.SetActive(false);
        }
    }

    private void Interact()
    {
        if (canInteract && _Input.Interact)
        {
            print("interacted");
        }
    }
}
