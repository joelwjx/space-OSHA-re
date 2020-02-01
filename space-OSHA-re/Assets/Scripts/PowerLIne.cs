using UnityEngine;
using System;
using System.Linq;

[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class PowerLIne : MonoBehaviour
{
    [SerializeField] private Color activeLineColor = Color.white;
    [SerializeField] private Color inactiveLineColor = Color.gray;
    [SerializeField] private Transform[] lineControlPoints;
    [SerializeField] private PowerGeneratorController generator;

    private LineRenderer lineRenderer;
    private Action state; // state machine action

    public bool isActive => state == ActiveState;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startColor = lineRenderer.endColor = activeLineColor;
        lineRenderer.positionCount = lineControlPoints.Length;
        lineRenderer.SetPositions(lineControlPoints.Select(p => p.position).ToArray());
        state = ActiveState;
    }

    #region State Machine
    private void Update()
    {
        // Update line positions
        lineRenderer.positionCount = lineControlPoints.Length;
        lineRenderer.SetPositions(lineControlPoints.Select(p => p.position).ToArray());

        state?.Invoke(); // execute the current state if not null
    }

    private void ActiveState()
    {
        if(generator.PowerLevel <= 0)
        {
            lineRenderer.startColor = lineRenderer.endColor = inactiveLineColor;
            state = InactiveState;
        }
    }

    private void InactiveState()
    {
        if(generator.PowerLevel > 0)
        {
            lineRenderer.startColor = lineRenderer.endColor = activeLineColor;
            state = ActiveState;
        }
    }
    #endregion
}
