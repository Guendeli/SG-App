using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObjectBase : MonoBehaviour {

    /*      Intercatable Base Class
     * Handles all interactable elements events
     * OnEventStart/End : called when the user clicks the OVR.PrimaryIndexTrigger while pointing the given object
     * OnHoverStart/End: called when the user points to the relevant object with the GearVR/Oculus Go Controller 
     */
    
    public bool isInHand { get; private set; }
    private bool isHovering;
    private OutlineMeshCopy highlighter;

    
	// Use this for initialization
	void Awake () {
        highlighter = GetComponentInChildren<OutlineMeshCopy>();
        if (highlighter == null)
        {
            Debug.LogWarning("Highlighter Not Found, make sure an OutlineMesh copy is attached to the mesh Renderer");
        }
        isHovering = false;
        isInHand = false;
	}

    // protected methods
    public virtual void OnHoverStart()
    {
        if (isHovering)
        {
            return;
        }
        // should show outline
        if (highlighter != null) { highlighter.Highlight(true); }
        isHovering = true;
    }

    public virtual void OnHoverEnds()
    {
        if (!isHovering)
        {
            return;
        }
        //should hide outline
        if (highlighter != null) { highlighter.Highlight(false); }
        isHovering = false;
    }

    public virtual void OnInteractionStart(GameObject sender)
    {
        if (isInHand)
        {
            return;
        }
        isInHand = true;
        GetComponent<Collider>().enabled = false;
    }

    public virtual void OnInteractionEnds()
    {
        if (!isInHand)
        {
            return;
        }
        isInHand = false;
        GetComponent<Collider>().enabled = true;
    }

}
