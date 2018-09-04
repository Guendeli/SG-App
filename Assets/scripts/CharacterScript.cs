using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WaypointSystem;
public class CharacterScript : MonoBehaviour {

    public ObjectType targetType;
    public GameObject bubbleReference;
    public UnityEvent onSucessEvent;
    public UnityEvent onCompleteEvent;
    private Animator animatorManager;
    private splineMove agent;

    public bool canInteract { get; private set; }

    void Awake()
    {
        // referencing our character
        animatorManager = GetComponent<Animator>();
        agent = GetComponent<splineMove>();
        canInteract = false;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (canInteract)
            {
                ReversePath();
            }
        }
    }
    // public methods are done here
    public void InitCharacter(PathManager path)
    {
        //set path
        agent.pathContainer = path;
        //set events
        UnityEvent start = new UnityEvent();
        UnityEvent end = new UnityEvent();
        start.AddListener(OnPathStart);
        end.AddListener(OnPathEnd);
        agent.events.Clear();
        agent.events.Add(start);
        agent.events.Add(new UnityEvent());
        agent.events.Add(end);

        // move character
        agent.StartMove();

    }
    public void OnPathStart()
    {
        animatorManager.SetFloat("Speed", 1);
        
    }

    public void OnPathEnd()
    {
        StartCoroutine(OnPathEndRoutine());
    }

    public void OnCharacterClick()
    {
        Destroy(gameObject);
    }

    private void ReversePath()
    {
        // remove all path index listeners
        agent.events.Clear();
        agent.events.Add(onCompleteEvent);
        agent.events.Add(new UnityEvent());
        agent.events.Add(onSucessEvent);
        // inverse path
        agent.reverse = true;
        // set new OnPathEnd / OnPathStart events
        agent.StartMove();
    }

    #region Character Interaction
    public void OnCharacterInteractionStart(ObjectType type)
    {
        if (!canInteract)
        {
            return;
        }

        if(type == targetType){
            StartCoroutine(Happy());
        } else {
            UnHappy();
        }
    }

    public void OnCharacterInteractionEnds()
    {
        Debug.Log("Character Collision Ended");
    }

    private void Greet()
    {
        animatorManager.Play("Greet");
    }

    private IEnumerator Happy()
    {
        bubbleReference.SetActive(false);
        animatorManager.Play("Happy");
        yield return new WaitForSeconds(0.8f);
        ReversePath();
    }

    private void UnHappy()
    {
        animatorManager.Play("UnHappy");
    }

    public void EndCharacter()
    {
        GameManager.Instance.NextLevel();
        Destroy(gameObject);
    }

    private IEnumerator OnPathEndRoutine()
    {
        animatorManager.SetFloat("Speed", 0);
        Vector3 target = Camera.main.transform.position;
        target.y = transform.position.y;
        transform.LookAt(target, Vector3.up);
        yield return new WaitForSeconds(0.5f);
        Greet();
        bubbleReference.SetActive(true);
        canInteract = true;
    }
    #endregion

}
