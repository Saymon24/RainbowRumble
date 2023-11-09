using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMenu : MonoBehaviour
{
    [SerializeField] private GameObject SetCloud1;
    [SerializeField] private GameObject SetCloud2;
    [SerializeField] private GameObject SetCloud3;
    [SerializeField] private GameObject SetCloud4;

    [SerializeField] private float speed1;
    [SerializeField] private float speed2;
    [SerializeField] private float speed3;
    [SerializeField] private float speed4;

    void Start()
    {
        
    }

    private void Update()
    {
        Vector3 cloud1Position = SetCloud1.transform.position;
        Vector3 cloud2Position = SetCloud2.transform.position;
        Vector3 cloud3Position = SetCloud3.transform.position;
        Vector3 cloud4Position = SetCloud4.transform.position;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 cloud1Position = SetCloud1.transform.position;
        Vector3 cloud2Position = SetCloud2.transform.position;
        Vector3 cloud3Position = SetCloud3.transform.position;
        Vector3 cloud4Position = SetCloud4.transform.position;

        cloud1Position.x += speed1 * Time.deltaTime;
        cloud2Position.x += speed2 * Time.deltaTime;
        cloud3Position.x += speed3 * Time.deltaTime;
        cloud4Position.x += speed4 * Time.deltaTime;

        SetCloud1.transform.position = cloud1Position;
        SetCloud2.transform.position = cloud2Position;
        SetCloud3.transform.position = cloud3Position;
        SetCloud4.transform.position = cloud4Position;
    }
}
