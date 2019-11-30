using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField] GameObject linePrefab;
    GameObject currentLine;

    LineRenderer lineRenderer;
    string currentBox = "";
    GameObject currentGameObject = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();

        } else if (Input.GetMouseButtonUp (0))
        {
            if (currentBox != "")
            {
                currentBox = "";
                currentGameObject = null;
                Destroy(currentLine);
            }
        }
        if (currentBox != "" && Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                UpdateLine(hit.point);
                if (currentGameObject != hit.collider.gameObject && hit.collider.name.StartsWith("A_"))
                {
                    if (hit.collider.name == currentBox)
                    {
                        Debug.Log("Yes");
                    } else
                    {
                        Debug.Log("wrong");
                    }
                    hit.collider.gameObject.name = "S_" + hit.collider.name;
                    currentGameObject.name = "S_" + currentGameObject.name;
                    currentBox = "";
                    
                }
            }
        }
    }
    void CreateLine()
    {
        currentBox = "";
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.name.StartsWith("A_")) {
                currentBox = hit.collider.name;
                currentGameObject = hit.collider.gameObject;
                currentLine = Instantiate(linePrefab, Vector3.right * -1, Quaternion.identity);
                lineRenderer = currentLine.GetComponent<LineRenderer>();
                lineRenderer.SetPosition(0, hit.point);
            }
        }
        
    }

    void UpdateLine(Vector3 newFingerPos)
    {
		if (lineRenderer.positionCount < 2)
			lineRenderer.positionCount = 2;
        //lineRenderer.positionCount++;
        lineRenderer.SetPosition(1, newFingerPos);
    }
}
