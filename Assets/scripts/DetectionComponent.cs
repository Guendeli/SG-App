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
        DetectInteractable(raycastSource);
	}

    // private methods

    private void DetectInteractable(Transform source)
    {
        // TODO: Assert Line Visauliser, should be independent from business logic
        RaycastHit hit;
        if (lineVisualizer != null) { lineVisualizer.SetLine(source.position, source.forward * 100.0f); }
        #region Interactive Object Detection
        if (Physics.Raycast(source.position, source.forward, out hit))
        {
            InteractiveObjectBase interactable = hit.collider.GetComponent<InteractiveObjectBase>();
            if (interactable != null)
            {
                if (lineVisualizer != null) { lineVisualizer.SetLine(source.position, hit.point); }
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
                if (lastInteractable != null)
                {
                    lastInteractable.OnHoverEnds();
                    lastInteractable = null;
                }
            }
        }
        else
        {
            if (lastInteractable != null)
            {
                    lastInteractable.OnHoverEnds();
                    lastInteractable = null;
            }
        }
        #endregion
    }

    // private methods

}
