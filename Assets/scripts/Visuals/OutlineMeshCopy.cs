using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class OutlineMeshCopy : MonoBehaviour {

    private MeshRenderer renderer;
	// Use this for initialization
	void Start () {
        InitHighlighter();
	}

    // private methods

    private void InitHighlighter()
    {
        renderer = GetComponent<MeshRenderer>();
        renderer.enabled = false;
    }

    // public methods

    public void Highlight(bool value)
    {
        renderer.enabled = value;
    }
}
