using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MapBuilder : MonoBehaviour
{
    private GameObject _tileObject;
    private Vector3 _mousePossition;
    private GameObject[,] _objectsOnMap;
    private int _currentX;
    private int _currentY;


    /// <summary>
    /// Данный метод вызывается автоматически при клике на кнопки с изображениями тайлов.
    /// В качестве параметра передается префаб тайла, изображенный на кнопке.
    /// Вы можете использовать префаб tilePrefab внутри данного метода.
    /// </summary>
    public void StartPlacingTile(GameObject tilePrefab)
    {
        _tileObject = Instantiate(tilePrefab); //записал ссылку на созданый объект
    }

    private void Awake()
    {
        _objectsOnMap = new GameObject[10, 10];
    }

    private void Update()
    {
        if (_tileObject != null) //проверяем есть ли доступный тайл для передвижения и сета
        {
            PrefabMoover(); //начинаем двигать
        }
    }

    private void PrefabMoover()
    {
        GetMousePossition(); //получаем позицию мыши
        _mousePossition.y = 0; //сбрасываем высоту

        GetCurrentIndex(_mousePossition); //получаем индекс относительно поля

        if (CanWeMove()) //проверяем не вышел ли индекс за пределы поля
        {
            _tileObject.transform.position = _mousePossition; //передвигаем объект

            if (IsAwailablePlace()) //проверяем можно ли сетить в данную точку префаб
            {
                _tileObject.GetComponent<IHighliteble>().SetColor();

                if (Input.GetMouseButton(0) && _tileObject != null) //если нажали кнопку то сетим
                {
                    SetPrefab();
                }
            }
        }
    }

    private void GetMousePossition() //получаем текущую позицию мыши в пространстве
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var point))
        {
            _mousePossition = point.transform.position;
        }
    }

    private void GetCurrentIndex(Vector3 possition) //получаем индекс относительно поля
    {
        Vector3 prefix = new Vector3(-4.5f, 0f, -4.5f);

        var result = possition - prefix;

        _currentX = (int)result.x;
        _currentY = (int)result.z;
    }

    private void SetPrefab() //при установке префаба на поле
    {
        _tileObject.GetComponent<IHighliteble>().ResetColor(); //сбрасываем цвет
        _objectsOnMap[_currentX, _currentY] = _tileObject;//сохраняем ссылку на объект в массив по координатам
        _tileObject = null;//удаляем временную ссылку для передвижения префаба
    }


    private bool IsAwailablePlace()//просто проверяем не занята ли клетка
    {
        return _objectsOnMap[_currentX, _currentY] == null;
    }
    private bool CanWeMove()//проверяем не вышла ли мышка за пределы поля
    {
        if (_currentX > 9 || _currentX < 0)
        {
            return false;
        }

        if (_currentY > 9 || _currentY < 0)
        {
            return false;
        }

        return true;
    }
}