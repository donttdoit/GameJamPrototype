using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private UnitType _unitType;
    [SerializeField] private Owner _owner;

    [HideInInspector] public bool IsArrived = false;


    public UnitType GetUnitType() => _unitType;
    public Owner GetOwner() => _owner;

    public float GetSpeed() => _speed;

    public enum UnitType
    {
        Gnome,
        Fairy,
        Goblin
    }

    public enum Owner
    {
        Player_A,
        Player_B
    }
}
