using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableInteractable : InteractiveObjectBase {

    // properties
    public ObjectType type;
    private Vector3 initPosition;
    private Quaternion initRotation;
    
    // public Methods

    public override void OnHoverStart()
    {
        base.OnHoverStart();
    }
    public override void OnHoverEnds()
    {
        base.OnHoverEnds();
    }

    // TODO: shoudl be moved to Command Pattern, and called regardless of the input method
    public override void OnInteractionStart(GameObject sender)
    {
        base.OnInteractionStart(sender);
        // let's store the position;
        initPosition = transform.position;
        initRotation = transform.rotation;
        // let's make the object a child of the anchor transform
        transform.SetParent(sender.transform, true);
       
    }

    public override void OnInteractionEnds()
    {
        base.OnInteractionEnds();
        transform.SetParent(null);
        transform.SetPositionAndRotation(initPosition, initRotation);
    }

    #region Private Methods
    // TODO: Try OnCollider methods

    

    #endregion

}
