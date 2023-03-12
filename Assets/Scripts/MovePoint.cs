using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

public class MovePoint : MonoBehaviour
{
    [SerializeField] private MovePointType _movePointType;
    [SerializeField] private MovePointOwner _movePointOwner;
    [SerializeField] private Color _changedColor;
    [SerializeField] private Sprite _changedSprite;

    private Light2D _light;
    private SpriteRenderer _sprite;

    public MovePointType GetMovePointType() => _movePointType;

    public void SetMovePointType(MovePointType movePointType) => _movePointType = movePointType;
    public MovePointOwner GetMovePointOwner() => _movePointOwner;

    public void SetMovePointOwner(MovePointOwner movePointOwner) => _movePointOwner = movePointOwner;

    public Color GetMovePointColor() => _changedColor;

    public void SetMovePointColor(Color color) => _light.color = color;

    public Sprite GetMovePointSprite() => _changedSprite;

    public void SetMovePointSprite(Sprite sprite) => _sprite.sprite = sprite;

    public void SetMovePointLightInnerRadius(float innerRadius) => _light.pointLightInnerRadius = innerRadius;

    public void SetMovePointLightOuterRadius(float outerRadius) => _light.pointLightOuterRadius = outerRadius;

    public void SetMovePointLightIntensity(float intensity) => _light.intensity = intensity;

    public void SetSetSpriteColor(Color color) => _sprite.color = color;


    private void Start()
    {
        _light = transform.GetComponentInChildren<Light2D>();
        _sprite = transform.GetComponent<SpriteRenderer>();
    }


    public enum MovePointOwner
    {
        Nothing,
        Player_A,
        Player_B
    }

    public enum MovePointType
    {
        Nothing,
        Flashlight,
        Gnome,
        Fairy,
        Goblin
    }
}





