using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LinkLineTest : MonoBehaviour
{
    [SerializeField] private float maxPointDistance = 1f;
    [SerializeField] private int solverIterations = 10; // Rigidez da restrição
    public List<Transform> points;

    private LineRenderer lineRenderer;
    private List<Rigidbody2D> pointsRb = new();

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = points.Count;
        lineRenderer.useWorldSpace = true;

        foreach (Transform point in points)
        {
            pointsRb.Add(point.GetComponent<Rigidbody2D>());
        }
    }

    void LateUpdate()
    {
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i].position);
        }
    }

    void FixedUpdate()
    {
        // Múltiplas iterações evitam o efeito "elástico" em correntes longas
        for (int iter = 0; iter < solverIterations; iter++)
        {
            for (int i = 1; i < pointsRb.Count; i++)
            {
                Rigidbody2D rbA = pointsRb[i - 1];
                Rigidbody2D rbB = pointsRb[i];

                Vector2 delta = rbB.position - rbA.position;
                float currentDistance = delta.magnitude;

                if (currentDistance > maxPointDistance)
                {
                    Vector2 direction = delta.normalized;
                    float excess = currentDistance - maxPointDistance;
                    Vector2 correction = direction * excess;

                    bool isADynamic = rbA.bodyType == RigidbodyType2D.Dynamic;
                    bool isBDynamic = rbB.bodyType == RigidbodyType2D.Dynamic;

                    if (isADynamic && isBDynamic)
                    {
                        // Divide o puxão igualmente entre os dois corpos
                        rbA.position += correction * 0.5f;
                        rbB.position -= correction * 0.5f;

                        // Zera a velocidade na direção do esticamento para não acumular energia
                        AdjustVelocity(rbA, direction);
                        AdjustVelocity(rbB, -direction);
                    }
                    else if (isBDynamic) // Apenas B se move (ex: A é Kinematic/âncora)
                    {
                        rbB.position -= correction;
                        AdjustVelocity(rbB, -direction);
                    }
                    else if (isADynamic) // Apenas A se move
                    {
                        rbA.position += correction;
                        AdjustVelocity(rbA, direction);
                    }
                }
            }
        }
    }

    private void AdjustVelocity(Rigidbody2D rb, Vector2 pullDirection)
    {
        // Remove apenas o componente da velocidade que está tentando afastar os pontos
        float velocityAlongPull = Vector2.Dot(rb.linearVelocity, pullDirection);
        if (velocityAlongPull < 0)
        {
            rb.linearVelocity -= pullDirection * velocityAlongPull;
        }
    }
}