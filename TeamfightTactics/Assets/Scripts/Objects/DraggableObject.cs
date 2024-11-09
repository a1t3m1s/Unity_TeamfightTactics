using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private const float radius = 0.5f;
    private const int tileLayer = 3;
    private float yAxis = 1.527f;

    public static bool isDraggable;
    private bool isTileLayer;

    private Vector3 mouseOffset;
    private Vector3 oldPosition;
    
    private float distance;


    private void Awake()
    {
        isDraggable = false;
    }

    private void OnMouseDown()
    {
        oldPosition = transform.position;
        mouseOffset = transform.position - GetMouseWorldPosition();
        isDraggable = true;
    }

    private void OnMouseDrag()
    {
        if (isDraggable)
        {
            Vector3 currMousePos = GetMouseWorldPosition();
            transform.position = new Vector3(currMousePos.x + mouseOffset.x, yAxis, currMousePos.z + mouseOffset.z);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    private void OnMouseUp()
    {
        isDraggable = false;
        DropOnClosestArea();
    }

    private void DropOnClosestArea()
    {
        Collider[] colliders = Physics.OverlapCapsule(transform.position - Vector3.up * 0.5f, transform.position + Vector3.up * radius, radius);

        Transform closestObject = null;
        float closestDistance = float.MaxValue;

        foreach(var collider in colliders)
        {
            if(collider.gameObject.layer.Equals(tileLayer))
            {
                isTileLayer = true;
            }
        }

        foreach(var collider in colliders)
        {
            if(isTileLayer)
            {
                //if(collider.gameObject != gameObject)
                //{
                    float distance = Vector3.Distance(transform.position, collider.transform.position);

                    if(distance < closestDistance)
                    {
                        closestObject = collider.transform;
                        closestDistance = distance;
                    }
                //}
            }
        }

        if(closestObject != null)
        {
            transform.position = new Vector3(closestObject.position.x, transform.position.y, closestObject.position.z);
        }
        else
        {
            transform.position = oldPosition;
        }
    }
}
