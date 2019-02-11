using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Graph))]
public class GraphView : MonoBehaviour
{
    public GameObject nodeViewPrefab;
    public NodeView[,] nodeViews;

    public void Init(Graph graph)
    {
        if (graph == null)
        {
            Debug.LogWarning("GRAPHVIEW No graph to initialize!");
            return;
        }
        nodeViews = new NodeView[graph.Width, graph.Height];

        foreach (Node n in graph.nodes)
        {
            GameObject instance = Instantiate(nodeViewPrefab, Vector3.zero, Quaternion.identity);
            NodeView nodeView = instance.GetComponent<NodeView>();

            if (nodeView != null)
            {
                nodeView.Init(n);
                nodeViews[n.xIndex, n.yIndex] = nodeView;
                Color originalColor = MapData.GetColorFromNodeType(n.nodeType);
                nodeView.ColorNode(originalColor);
            }
        }
    }

    public void ColorNodes(List<Node> nodes, Color color, bool lerpColor = false, float lerpValue = 0.5f)
    {
        foreach (Node n in nodes)
        {
            if (n != null)
            {
                NodeView nodeView = nodeViews[n.xIndex, n.yIndex];
                Color newColor = color;

                if (lerpColor)
                {
                    Color originalColor = MapData.GetColorFromNodeType(n.nodeType);
                    newColor = Color.Lerp(originalColor, newColor, lerpValue);
                }

                if (nodeView != null)
                {
                    nodeView.ColorNode(newColor);
                }
            }
        }
    }

    public void ShowNodeArrows(List<Node> nodes, Color color)
    {
        foreach (var n in nodes)
        {
            ShowNodeArrow(n, color);
        }
    }

    public void ShowNodeArrow(Node node, Color color)
    {
        if (node != null)
        {
            NodeView nodeView = nodeViews[node.xIndex, node.yIndex];
            if (nodeView != null)
            {
                nodeView.ShowArrow(color);
            }
        }
    }


}
