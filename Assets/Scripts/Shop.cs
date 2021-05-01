using UnityEngine;

public class Shop : MonoBehaviour
{

    MyBuildManager buildManager;

    public TurretBlueprint machineGun;
    public TurretBlueprint missileLauncher;

    private void Start()
    {
        buildManager = MyBuildManager.instance;
    }

    public void SelectMachineGun()
    {
        Debug.Log("MachineGun Selected");
        buildManager.SelectTurretToBuild(machineGun);
    }
    
    public void SelectMissileLauncher()
    {
        Debug.Log("MissileLauncher selected");
        buildManager.SelectTurretToBuild(missileLauncher);
    }
    
    public void SelectLaser()
    {
        Debug.Log("Laser Selected");
        //buildManager.SelectTurretToBuild(buildManager.Turret3Prefab);
    }
}
