using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInteractable : InteractiveObjectBase {

    // properties
    private bool DebugModeBitch;
    private Vector3 initPosition;
    private Quaternion initRotation;

    // unity specific methods


    // public methods

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

    void OnTriggerEnter(Collider col)
    {
        CharacterScript character = col.gameObject.GetComponent<CharacterScript>();
        if (character != null)
        {
            character.OnCharacterInteractionStart();
        }
    }

    void OnTriggerExit(Collider col)
    {
        CharacterScript character = col.gameObject.GetComponent<CharacterScript>();
        if (character != null)
        {
            character.OnCharacterInteractionEnds();
        }
    }

    #endregion

}
