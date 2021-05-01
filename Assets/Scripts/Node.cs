using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    public Color hoverColor;
    public Color cantBuildColor;

    private Renderer rend;
    private Color startColor;

    public GameObject turret;
    public Vector3 positionOffset;

    MyBuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = MyBuildManager.instance;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if(turret != null)
        {
            Debug.Log("Can't build there");
            return;
        }

        buildManager.BuildTurretOn(this);
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
            rend.material.color = hoverColor;
        else
            rend.material.color = cantBuildColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

}
