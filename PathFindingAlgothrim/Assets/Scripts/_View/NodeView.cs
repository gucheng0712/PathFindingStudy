using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeView : MonoBehaviour
{
    public GameObject tile;
    public GameObject arrow;
    [Range(0, 0.5f)] public float borderSize = 0.15f;

    Node m_node;


    public void Init(Node node)
    {
        if (tile != null)
        {
            gameObject.name = "Node (" + node.xIndex + "," + node.yIndex + ")"; // change the NodeView GameObject's name
            gameObject.transform.position = node.position; // Set to the node Position
            tile.transform.localScale = new Vector3(1f - borderSize, 1f, 1f - borderSize); // set the size based on the borderSize
            m_node = node;
            EnableObject(arrow, false); // disable the arrow gameobject
        }
    }

    // Overload method of ColorNode
    public void ColorNode(Color color)
    {
        ColorNode(color, tile);
    }

    // Set the node color
    void ColorNode(Color color, GameObject go)
    {
        if (go != null)
        {
            Renderer goRenderer = go.GetComponent<Renderer>();

            if (goRenderer != null)
            {
                goRenderer.material.color = color;
            }
        }
    }


    void EnableObject(GameObject go, bool state)
    {
        if (go != null)
        {
            go.SetActive(state);
        }
    }

    // Show the Arrow GameObject
    public void ShowArrow(Color color)
    {
        if (m_node != null && arrow != null && m_node.previous != null)
        {
            EnableObject(arrow, true);
            Vector3 dirToPrevous = m_node.previous.position - m_node.position;
            arrow.transform.rotation = Quaternion.LookRotation(dirToPrevous);

            Renderer arrowRenderer = arrow.GetComponent<Renderer>();
            if (arrowRenderer != null)
            {
                arrowRenderer.material.color = color;
            }
        }
    }

}
