using System.Collections.Generic;
using UnityEngine;

public class LinkLine : MonoBehaviour
{
    [SerializeField] float maxPointDistance;    // Distancia máxima entre dois pontos
    public List<Transform> points;

    private LineRenderer lineRenderer;
    private List<Rigidbody2D> pointsRb = new();

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = points.Count;
        lineRenderer.useWorldSpace = true;

        int i = 0;
        foreach(Transform point in points)
        {
            pointsRb.Add(point.gameObject.GetComponent<Rigidbody2D>());
            i++;
        }
    }



    void LateUpdate()
    {
        int i = 0;
        foreach(Transform point in points)
        {
            lineRenderer.SetPosition(i, point.position);
            i++;
        }
    }

    void FixedUpdate()
    {
        int i = 0;
        Rigidbody2D lastRb = pointsRb[i];

        foreach(Rigidbody2D rb in pointsRb)
        {
            if (i == 0)
            {
                i++;
                continue;
            }

            if (Vector2.Distance(lastRb.position, rb.position) > maxPointDistance)
            {
                Vector2 direction = lastRb.position - rb.position;

                lastRb.position = rb.position + direction.normalized * maxPointDistance;
            }

            lastRb = rb;
            i++;
        }
    }
}

