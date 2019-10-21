/*
 * Author: Ramkumar Thiyagarajan
 * Description: Collection of cells to create a game board 
 * Created on 19/10/2019
 * Updated on 21/10/2019
 */

using System;
using System.Xml;
using UnityEngine;

/// <summary>
/// Board class
/// Collection of cells to create a game board
/// </summary>
public class Board : MonoBehaviour
{
    private const int NUMCELLS = 40;            // Number of cells on the board
    
    public Cell[] cells;                        // list of all the properties on the board


    // Start is called before the first frame update
    private void Start()
    {
        cells = new Cell[NUMCELLS];             // initalize the size of cells with null references

        LoadXML();                              // Loading the properties of each cell on the board from loadcells.xml.

        int index = 0;
        foreach (Transform t in transform)      // Populate the waypoints with cell's position on board.
        {
            cells[index].SetTransform(t);
            index++;
        }
    }

    /// <summary>
    /// Reads from the XML file
    /// </summary>
    private void LoadXML()
    {
        
        TextAsset xmlAsset = Resources.Load("XML/loadcells") as TextAsset;  // import the xml file as an asset.

        XmlDocument doc = new XmlDocument();                                // Initializing a new xml document.        

        doc.LoadXml(xmlAsset.text);                                         // load the text contents of the xml file as a string.

        XmlNodeList cellnodes = doc.GetElementsByTagName("Cell");           // returns all the nodes that goes by the name cell.

        int index = 0;

        foreach (XmlNode cell in cellnodes)                                 // grabs the sub nodes under the Cell node
        {
            Cell c = new Cell();
            c.SetCell(  cell.ChildNodes[0].InnerText.ToString(),
                        (PropertyOwnership)Enum.Parse(typeof(PropertyOwnership), cell.ChildNodes[1].InnerText),
                        (PropertyStatus)Enum.Parse(typeof(PropertyStatus), cell.ChildNodes[2].InnerText),
                        int.Parse(cell.ChildNodes[3].InnerText),
                        int.Parse(cell.ChildNodes[4].InnerText),
                        (PropertyType)Enum.Parse(typeof(PropertyType), cell.ChildNodes[5].InnerText)
                    );

            cells[index] = c;                                               // Reference to values of the cell from the XML file
            index++;
        }
    }
}
