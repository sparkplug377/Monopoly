/*
 * Author: Ramkumar Thiyagarajan
 * Description: Contains all methods for performing operation of individual cells on the board. 
 * Created on 19/10/2019
 * Updated on 21/10/2019
 */


using UnityEngine;

/// <summary> Defines the type of a property </summary>
public enum PropertyType { COLOR_STRIPE, UTILITY, RAILROAD, OTHER, GO }

/// <summary> Defines the status of a property </summary>
public enum PropertyStatus { SOLD, UNSOLD, NOT_APPLICABLE }

/// <summary> Defines the ownership of a property </summary>
public enum PropertyOwnership { PONE, PTWO, NONE, NOT_APPLICABLE }

/// <summary>
/// Property Class
/// Contains all methods for performing operation of individual cells on the board. 
/// </summary>
public class Cell
{
    ///<summary> The Name represents the property's name </summary>
    public string Name { get; private set; }

    /// <summary> The price reprents the cost of a property </summary>
    public int Price { get; private set; }

    /// <summary> The rent reprents the property's rent </summary>
    public int Rent { get; private set; }

    /// <summary> The CellTransform represents the transform of the property on board </summary>
    public Transform CellTransform { get; private set; }

    /// <summary> Represents the status of the property </summary>
    public PropertyStatus Status { get; private set; }

    /// <summary> Represents ownership of the property </summary>
    public PropertyOwnership Ownership { get; private set; }

    /// <summary> Represents the type of the property </summary>
    public PropertyType Type { get; private set; }


    /// <summary> Updates the members of the cell </summary>
    /// <param name="name"> name of the property </param>
    /// <param name="propOwnership"> ownership of the property</param>
    /// <param name="propStatus"> property status</param>
    /// <param name="rent"> rent to the property </param>
    /// <param name="price"> price to buy </param>
    /// <param name="propType"> type of property </param>
    public void SetCell(string name, PropertyOwnership propOwnership, PropertyStatus propStatus, int rent, int price, PropertyType propType)
    {
        Name = name;
        Ownership = propOwnership;
        Status = propStatus;
        Rent = rent;
        Price = price;
        Type = propType;
    }

    /// <summary> Reference to the transform of a waypoint </summary>
    /// <param name="cellT"> transform of the property on the board </param>
    public void SetTransform(Transform cellT)
    {
        CellTransform = cellT;
    }

    /// <summary>
    /// Update the status and Ownership of the property. 
    /// </summary>
    /// <param name="propStatus"> property status </param>
    /// <param name="propOwnership"> ownership of the property </param>
    public void UpdateCell(PropertyStatus propStatus, PropertyOwnership propOwnership)
    {
        Status = propStatus;
        Ownership = propOwnership;
    }
}
