using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public Transform player;
    public Roads[] roadPrefabs;
    public Roads firstRoad;
    public List<Roads> spawnedRoads = new List<Roads>();
    int roadCount = 3;
    void Start()
    {
        Roads startRoad = Instantiate(firstRoad);
        spawnedRoads.Add(startRoad); //��������� ��������� ������ ������ � �����  ������
        for (int i = 0; i < roadCount; i++)
        {
            SpawnRoads(); // ��������� ������ ������ �� �����, ���� ��������� ������ � �������
        }
    }
    void Update()
    {
        if (player.position.z > (spawnedRoads[0].end.position.z + 10f)) // ���� ����� ����������� ������ �������� ����� ������� ������ - ������� ���
        {
            SpawnRoads();
            DestroyRoads();
        }
    }
    private void SpawnRoads()
    {
        Roads newRoad = Instantiate(roadPrefabs[Random.Range(0, roadPrefabs.Length)]);
        newRoad.transform.position = spawnedRoads[spawnedRoads.Count - 1].end.position + new Vector3(0, 0, 25);
        spawnedRoads.Add(newRoad);
    }
    private void DestroyRoads()
    {
        if (spawnedRoads.Count >= 4)
        {
            Destroy(spawnedRoads[0].gameObject);
            spawnedRoads.RemoveAt(0);
        }

    }
}
