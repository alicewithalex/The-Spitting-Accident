using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject defaultCarPrefab;
    
    public Vector2 durationRange = new Vector2(1f,5f);
    public int poolSize = 25;

    private List<Car> pool;
    
    void Start()
    {
        pool = new List<Car>();
    }

    private IEnumerator GameLoop()
    {
        
        while (GameSingleton.instance.IsGameEnded == false)
        {
            SpawnCar();
            yield return new WaitForSeconds(Random.Range(durationRange.x,durationRange.y));
        }
    }

    public void StartGameLoop()
    {
        StartCoroutine(GameLoop());
    }

    private void SpawnCar()
    {
        List<Car> poolInActive = pool.FindAll(x => x.gameObject.activeSelf == false);

        if (poolInActive.Count > 0)
        {
            int index = Random.Range(0, poolInActive.Count);

            poolInActive[index].ToggleCar(true);
            poolInActive[index].gameObject.transform.position = transform.position;
            poolInActive[index].gameObject.transform.forward = transform.forward;
            poolInActive[index].gameObject.SetActive(true);
        }
        else
        {
            Car car = Instantiate(defaultCarPrefab, null).GetComponent<Car>();
            car.transform.position = transform.position;
            car.transform.forward = transform.forward;
            
            pool.Add(car);
        }
    }
}
