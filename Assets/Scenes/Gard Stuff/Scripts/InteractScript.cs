using UnityEngine;

public class InteractScript : MonoBehaviour
{

    [SerializeField] private float RaycastDistance = 10f;
    public LayerMask interactableLayer;
    public LayerMask pickupLayer;
    public LayerMask showerLayer;
    public LayerMask TvLayer;
    private bool canInteract;
    private bool canPickup;
    private GameObject targetKey;
    public Input _Input;
    public GameObject interactCrossHair;
    public GameObject keyUI;
    private bool canShowInteractCrosshair;
    
    private RaycastHit hit;
    
    private bool canInteractWithShower;
    [HideInInspector]public bool hasInteractedWithShower;

    private bool canInteractWithTV;
    [HideInInspector] public bool hasInteractedWithTV;
    
    private bool hasKey;

    private void Start()
    {
        canInteract = false;
        hasInteractedWithShower = false;
        canInteractWithShower = false;
        canInteractWithTV = false;
        hasInteractedWithTV = false;
        interactCrossHair.SetActive(false);
        keyUI.SetActive(false);
    }

    private void Update()
    {
        DetectInteractable();
        Interact();
        InteractShower();
        ShowInteractCrosshair();
        ShowKeyUI();
        InteractTV();
        Pickup();
    }

    private void DetectInteractable()
    {
        canInteract = false;
        canShowInteractCrosshair = false;
        targetKey = null;
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, RaycastDistance))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {
                canInteract = true;
                canShowInteractCrosshair = true;
            }
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("PickUp"))
            {
                print("Looking at key");
                canPickup = true;
                canShowInteractCrosshair = true;
                targetKey = hit.transform.gameObject;
            }
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("BasementDoor"))
            {
                if (hasKey)
                {
                    //TODO: Open basement door
                    hasKey = false;
                }
            }
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

    private void Pickup()
    {
        if (canPickup && _Input.Interact && targetKey != null)
        {
            Destroy(targetKey);
            hasKey = true;
            targetKey = null;
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

    private void ShowKeyUI()
    {
        if (hasKey) keyUI.SetActive(true);
        else keyUI.SetActive(false);
    }

    private void InteractTV()
    {
        RaycastHit TVhit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),out TVhit,RaycastDistance,TvLayer))
        {
            canInteractWithTV = true;
            canShowInteractCrosshair = true;
            interactCrossHair.SetActive(true);
        }

        if (canInteractWithTV && _Input.Interact)
        {
            hasInteractedWithTV = true;
            print("has tv indeed");
        }
    }
}
