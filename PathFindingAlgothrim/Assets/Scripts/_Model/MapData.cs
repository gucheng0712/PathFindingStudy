using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class MapData : MonoBehaviour
{
    public int width = 10;  // the width of the map
    public int height = 5;  // the height of the map

    public TextAsset textAsset; // the optional data to load (text file)
    public Texture2D textureMap;// the optional data to load (image)
    public string resourcePath = "Mapdata";

    public Color32 openColor = Color.white;
    public Color32 blockedColor = Color.black;
    public Color32 lightTerrainColor = new Color32(124, 194, 78, 255);
    public Color32 mediumTerrainColor = new Color32(252, 255, 52, 255);
    public Color32 heavyTerrainColor = new Color32(255, 129, 12, 255);

    static Dictionary<Color32, NodeType> terrainLookupTable = new Dictionary<Color32, NodeType>();

    void Awake()
    {
        SetupLookupTable();
    }

    void Start()
    {
        string levelName = SceneManager.GetActiveScene().name;


        // If didn't find the data, then load the data from the resources
        if (textureMap == null)
        {
            textureMap = Resources.Load(resourcePath + "/" + levelName) as Texture2D;
        }

        if (textAsset == null)
        {

            textAsset = Resources.Load(resourcePath + "/" + levelName) as TextAsset;
        }
    }

    // Overload Method of GetMapFromTextFile
    public List<string> GetMapFromTextFile()
    {
        return GetMapFromTextFile(textAsset);
    }

    // Read the map data from the text file
    public List<string> GetMapFromTextFile(TextAsset tAsset)
    {
        List<string> lines = new List<string>();

        if (tAsset != null)
        {
            string textData = tAsset.text;
            string[] delimiters = { "\r\n", "\n" };
            lines.AddRange(textData.Split(delimiters, System.StringSplitOptions.None));
            lines.Reverse();
        }
        else
        {
            Debug.LogWarning("MAPDATA GetTextFromFile Error: invalid TextAsset");
        }
        return lines;
    }

    // Overload Method of GetMapFromTexture
    public List<string> GetMapFromTexture()
    {
        return GetMapFromTexture(textureMap);
    }

    // Read the map data from the Image file
    public List<string> GetMapFromTexture(Texture2D texture)
    {
        List<string> lines = new List<string>();

        if (texture != null)
        {
            for (int y = 0; y < texture.height; y++)
            {
                string newLine = "";

                for (int x = 0; x < texture.width; x++)
                {
                    Color32 pixelColor = texture.GetPixel(x, y);
                    if (pixelColor != Color.white)
                    {
                        Debug.Log(pixelColor);
                    }

                    if (terrainLookupTable.ContainsKey(pixelColor))
                    {
                        NodeType nodeType = terrainLookupTable[pixelColor];
                        int nodeTypeNum = (int)nodeType;
                        newLine += nodeTypeNum;
                    }
                    else
                    {
                        newLine += '0';
                    }
                }
                lines.Add(newLine);
                //Debug.Log(newLine);
            }
        }

        return lines;
    }

    // Set the width and height of the mapdata
    public void SetDimensions(List<string> textLines)
    {
        height = textLines.Count;

        foreach (string line in textLines)
        {
            if (line.Length > width)
            {
                width = line.Length;
            }
        }
    }

    // Create the map data
    public int[,] MakeMap()
    {
        List<string> lines = new List<string>();

        if (textureMap != null)
        {
            lines = GetMapFromTexture();
        }
        else
        {
            lines = GetMapFromTextFile(textAsset);
        }

        SetDimensions(lines);

        int[,] map = new int[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (lines[y].Length > x)
                {
                    map[x, y] = (int)Char.GetNumericValue(lines[y][x]);
                }
            }
        }
        return map;

    }

    // The Lookup Table to store all node type as value and the corresponding color as key in to the dictionary
    void SetupLookupTable()
    {
        terrainLookupTable.Add(openColor, NodeType.Open);
        terrainLookupTable.Add(blockedColor, NodeType.Blocked);
        terrainLookupTable.Add(lightTerrainColor, NodeType.LightTerrain);
        terrainLookupTable.Add(mediumTerrainColor, NodeType.MediumTerrain);
        terrainLookupTable.Add(heavyTerrainColor, NodeType.HeavyTerrain);

    }

    // Get the Color of Node through the Lookup Table
    public static Color GetColorFromNodeType(NodeType nodeType)
    {
        if (terrainLookupTable.ContainsValue(nodeType))
        {
            Color colorKey = terrainLookupTable.FirstOrDefault(x => x.Value == nodeType).Key;
            return colorKey;
        }

        return Color.white;
    }
}
