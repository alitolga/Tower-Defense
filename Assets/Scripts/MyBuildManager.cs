using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBuildManager : MonoBehaviour
{
    public static MyBuildManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    //private void Start()
    //{
    //    turretToBuild = standardTurretPrefab;
    //}

    public GameObject Turret1Prefab;
    public GameObject Turret2Prefab;
    public GameObject Turret3Prefab;

    private TurretBlueprint turretToBuild;

    //public GameObject GetTurretToBuild()
    //{
    //    return turretToBuild;
    //}

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }

    public void BuildTurretOn(Node node)
    {
        if(PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money to build");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;
        GameObject turret = Instantiate<GameObject>(turretToBuild.prefab, node.transform.position + node.positionOffset, Quaternion.identity);
        node.turret = turret;


    }

}
