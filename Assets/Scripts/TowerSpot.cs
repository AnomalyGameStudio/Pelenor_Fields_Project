using UnityEngine;
using System.Collections;

public class TowerSpot : MonoBehaviour
{
    void OnMouseUp()
    {
        Debug.Log("TowerSpot clicked;");

        BuildingManager buildingManager = BuildingManager.Instance;

        if(buildingManager.selectedTower != null)
        {
            ScoreManager scoreManager = ScoreManager.Instance;

            if (scoreManager.Money < buildingManager.selectedTower.GetComponent<Tower>().cost)
            {
                Debug.Log("Not Enough money!");
                return;
            }

            scoreManager.addMoney ( - buildingManager.selectedTower.GetComponent<Tower>().cost);

            // FIXME: Right we assume that we're an object nested in a parent.
            Instantiate(buildingManager.selectedTower, transform.parent.position, transform.parent.rotation);

            Destroy(transform.parent.gameObject);
        }
    }
}
