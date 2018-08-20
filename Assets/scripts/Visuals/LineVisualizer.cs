using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineVisualizer : MonoBehaviour {

    public GameObject lineObject;
    private LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
        InitLine();
	}

    // private methods

    private void InitLine()
    {
        GameObject line = (GameObject)Instantiate(lineObject);
        lineRenderer = line.GetComponent<LineRenderer>();
    }

    public void SetLine(Vector3 start, Vector3 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        
    }
}
