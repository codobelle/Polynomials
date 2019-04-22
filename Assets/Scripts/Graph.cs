using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    private LineRenderer lr;
    private int minX = -100;
    private int maxX = 100;
    private List<Vector3> positionsList = new List<Vector3>();
    private int zoomOut = 10;

    public void CloseGraph()
    {
        transform.parent.gameObject.SetActive(false);
    }
    //Drow graph of polynomial
    public void DrawGraph(int[] coefficientsValue)
    {
        positionsList = new List<Vector3>();
        lr = GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));

        for (float x = minX; x < maxX; x += 0.1f)
        {
            float y = 0;
            for (int i = 0; i < coefficientsValue.Length; i++)
            {
                y += coefficientsValue[i] * Mathf.Pow(x, coefficientsValue.Length - 1 - i);
            }
            Vector3 newPosition = new Vector3(x, y);
            positionsList.Add(newPosition);
        }

        Vector3[] positions = new Vector3[positionsList.Count];
        for (int i = 0; i < positionsList.Count; i++)
        {
            positions[i] = positionsList[i]/ zoomOut;
        }
        lr.positionCount = positions.Length;
        lr.SetPositions(positions);
    }
}
