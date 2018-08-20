using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaypointSystem;
public class CharacterScript : MonoBehaviour {

    private Animator animatorManager;
    private splineMove agent;
    void Awake()
    {
        // referencing our character
        animatorManager = GetComponent<Animator>();
        agent = GetComponent<splineMove>();
    }

    // public methods are done here

    public void OnPathStart()
    {
        animatorManager.SetFloat("Speed", agent.speed);
    }

    public void OnPathEnd()
    {
        animatorManager.SetFloat("Speed", 0);
        Vector3 target = Camera.main.transform.position;
        target.y = transform.position.y;
        transform.LookAt(target, Vector3.up);
    }

    public void OnCharacterClick()
    {
        Destroy(gameObject);
    }

    #region Character Interaction
    public void OnCharacterInteractionStart()
    {
        Debug.Log("Character Collision Detected");
    }

    public void OnCharacterInteractionEnds()
    {
        Debug.Log("Character Collision Ended");
    }

    #endregion

}
