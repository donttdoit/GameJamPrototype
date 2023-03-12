using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Game : MonoBehaviour
{
    private Unit _unit;
    private GameObject _unitObject;
    private GameObject _movePointObject;
    private MovePoint _movePoint;

    private Color _movePointColor;
    private Sprite _movePointSprite;

    // Кастомные настройки под отдельные фонари
    [SerializeField] private GameObject[] _gameObjectsTodestroy;
    
    //[SerializeField] private Light2D[] _lightTodestroy;


    // Настройки при захвате точки игроком
    private float _lightIntensity = 4.12f;
    private float _innerRadius = 6.04f;
    private float _outerRadius = 16.08f;
    private Color _spriteColor = new Color(35, 50, 156, 255);

    private bool CheckArrived()
    {
        if (ObjectDetection.s_gameObjects.Count == 2 &&
            ObjectDetection.s_gameObjects[0].transform.position == (ObjectDetection.s_gameObjects[1].transform.position - new Vector3(ObjectDetection.s_offsetX, 0, 0)))
        {
            _unit = ObjectDetection.s_gameObjects[0].GetComponent<Unit>();
            _unitObject = ObjectDetection.s_gameObjects[0];

            _movePoint = ObjectDetection.s_gameObjects[1].GetComponent<MovePoint>();
            _movePointObject = ObjectDetection.s_gameObjects[1];

            _movePointColor = _movePoint.GetMovePointColor();
            _movePointSprite = _movePoint.GetMovePointSprite();

            ObjectDetection.s_gameObjects.Clear();
            return true;

        }


        return false;
    }

    IEnumerator waiter()
    {
        //Debug.Log("Test");
        //_movePoint.SetMovePointType(MovePoint.MovePointType.Flashlight);
        //_movePoint.SetMovePointOwner(MovePoint.MovePointOwner.Player_A);
        //_movePoint.SetMovePointColor(_movePointColor);
        //_movePoint.SetMovePointSprite(_movePointSprite);
        //_movePoint.SetSetSpriteColor(_spriteColor);
        //_movePoint.SetMovePointLightIntensity(_lightIntensity);
        _movePoint.SetMovePointLightInnerRadius(20f);
        _movePoint.SetMovePointLightOuterRadius(48f);
        yield return new WaitForSeconds(2);
        Destroy(_movePointObject);
        foreach (GameObject obj in _gameObjectsTodestroy)
        {
            Destroy(obj);
        }

    }

    private void Update()
    {
        if (CheckArrived())
        {
            // Тип точки
            switch (_movePoint.GetMovePointType())
            {
                // если фонарь
                case MovePoint.MovePointType.Flashlight:
                    {
                        // Чья точка
                        switch (_movePoint.GetMovePointOwner())
                        {
                            // Ничейная точка
                            case MovePoint.MovePointOwner.Nothing:
                                // пришла фея
                                if (_unit.GetUnitType() == Unit.UnitType.Fairy)
                                {
                                    _movePoint.SetMovePointType(MovePoint.MovePointType.Flashlight);
                                    _movePoint.SetMovePointOwner(MovePoint.MovePointOwner.Player_A);
                                    _movePoint.SetMovePointColor(_movePointColor);
                                    _movePoint.SetMovePointSprite(_movePointSprite);
                                    _movePoint.SetSetSpriteColor(_spriteColor);
                                    _movePoint.SetMovePointLightIntensity(_lightIntensity);
                                    _movePoint.SetMovePointLightInnerRadius(_innerRadius);
                                    _movePoint.SetMovePointLightOuterRadius(_outerRadius);
                                    
                                }
                                // пришел гоблин
                                else if (_unit.GetUnitType() == Unit.UnitType.Goblin)
                                {
                                    //

                                }
                                // пришел гном
                                else if (_unit.GetUnitType() == Unit.UnitType.Gnome)
                                {
                                    //
                                }
                                break;

                            // Занята игроком B
                            case MovePoint.MovePointOwner.Player_B:
                                //  пришла фея
                                if (_unit.GetUnitType() == Unit.UnitType.Fairy)
                                {
                                    //
                                }
                                // пришел гоблин
                                else if (_unit.GetUnitType() == Unit.UnitType.Goblin)
                                {
                                    StartCoroutine(waiter());
                                    
                                }

                                // пришел гном
                                else if (_unit.GetUnitType() == Unit.UnitType.Gnome)
                                {
                                    //
                                }
                                break;
                        }
                        break;
                    }
            }
        }
    }
}
