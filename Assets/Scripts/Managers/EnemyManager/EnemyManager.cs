using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int MaxEnemyNumber = 200;

    private int EnemyNumber;

    public bool CanSpawnEnemy()
    {
        if (EnemyNumber >= MaxEnemyNumber)
            return false;
        return true;
    }

    public void AddEnemy()
    {
        EnemyNumber++;
    }

    public void RemoveEnemy()
    {
        EnemyNumber--;
    }

    public int GetEnemyNumber()
    {
        return EnemyNumber;
    }

}
