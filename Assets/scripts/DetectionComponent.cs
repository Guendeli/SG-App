using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineVisualizer))]
public class DetectionComponent : MonoBehaviour {

    /* Handles only detecting InteractiveObjects */
    private Transform raycastSource;

    private LineVisualizer lineVisualizer;
    private InteractiveObjectBase lastInteractable;
	// Use this for initialization
	void Start () {
        lineVisualizer = GetComponent<LineVisualizer>();
        raycastSource = transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (lineVisualizer != null) { lineVisualizer.SetLine(raycastSource.position, raycastSource.forward * 100.0f); }
        DetectCharacter(raycastSource);
        DetectInteractable(raycastSource);
	}

    // private methods

    private void DetectInteractable(Transform source)
    {
        // TODO: Assert Line Visauliser, should be independent from business logic
        RaycastHit hit;
        #region Interactive Object Detection
        if (Physics.Raycast(source.position, source.forward, out hit))
        {
            if (lineVisualizer != null) { lineVisualizer.SetLine(source.position, hit.point); }
            InteractiveObjectBase interactable = hit.collider.GetComponent<InteractiveObjectBase>();
            if (interactable != null)
            {
                lastInteractable = interactable;
                lastInteractable.OnHoverStart();
                if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
                {

                    if (!lastInteractable.isInHand)
                    {
                        lastInteractable.OnInteractionStart(gameObject);
                    }
                    else
                    {
                        lastInteractable.OnInteractionEnds();
                    }
                }
            }
            else
            {
                if (lastInteractable != null && !lastInteractable.isInHand)
                {
                    lastInteractable.OnHoverEnds();
                    lastInteractable = null;
                }
                else if (lastInteractable != null && lastInteractable.isInHand)
                {
                    if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
                    {
                        lastInteractable.OnInteractionEnds();
                    }
                }
            }
        }
        else
        {
            if (lastInteractable != null && !lastInteractable.isInHand)
            {
                    lastInteractable.OnHoverEnds();
                    lastInteractable = null;
            }
            else if (lastInteractable != null && lastInteractable.isInHand)
            {
                if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
                {
                    lastInteractable.OnInteractionEnds();
                }
            }
        }
        #endregion
    }

    private void DetectCharacter(Transform source)
    {
        // TODO: Assert Line Visauliser, should be independent from business logic
        RaycastHit hit;
        if (Physics.Raycast(source.position, source.forward, out hit))
        {
             if (lineVisualizer != null) { lineVisualizer.SetLine(source.position, hit.point); }
            CharacterScript character = hit.collider.GetComponent<CharacterScript>();
            if (character != null)
            {
                // only trigger Character interaction if the user is already picking an Object
                if (lastInteractable != null && lastInteractable.isInHand)
                {
                    if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
                    {
                        GrabbableInteractable obj = (GrabbableInteractable)lastInteractable;
                        character.OnCharacterInteractionStart(obj.type);
                    }
                }
            }
        }
    }

}
