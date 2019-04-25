using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTowerManager : MonoBehaviour
{
    public Mesh ArrowTowerUpgradeMesh;
    public Mesh BombTowerUpgradeMesh;
    public Mesh MagicTowerUpgradeMesh;

    private GameObject tower;

    public void UpgradeTower()
    {
        tower = this.transform.parent.gameObject;
        switch(tower.gameObject.tag)
        {
            case "ArrowTower":
                tower.GetComponent<MeshFilter>().mesh = ArrowTowerUpgradeMesh;
                break;
            case "BombTower":
                tower.GetComponent<MeshFilter>().mesh = BombTowerUpgradeMesh;
                break;
            case "MagicTower":
                tower.GetComponent<MeshFilter>().mesh = MagicTowerUpgradeMesh;
                break;
            default:
                tower.GetComponent<MeshFilter>().mesh = BombTowerUpgradeMesh;
                break;
        }

    }

    public void DeleteTower()
    {
        tower = this.transform.parent.gameObject;
        Destroy(tower);
    }
}
