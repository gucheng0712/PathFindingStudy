using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoController : MonoBehaviour
{
    public MapData mapData;
    public Graph graph;

    public Pathfinder pathfinder;
    public int startX = 0;
    public int startY = 0;
    public int goalX = 15;
    public int goalY = 1;

    public float timeStep = 0.1f;

    void Start()
    {
        if (mapData != null && graph != null)
        {
            int[,] mapInstance = mapData.MakeMap();
            graph.Init(mapInstance);

            GraphView graphView = graph.gameObject.GetComponent<GraphView>();

            if (graphView != null)
            {
                graphView.Init(graph);
            }

            if (graph.IsWithinBounds(startX,startY) && graph.IsWithinBounds(goalX, goalY)
                && pathfinder != null)
            {
                Node startNode = graph.nodes[startX, startY];
                Node goalNode = graph.nodes[goalX, goalY];
                pathfinder.Init(graph, graphView, startNode, goalNode);
                StartCoroutine(pathfinder.SearchRoutine(timeStep));
            }
        }
    }

}
