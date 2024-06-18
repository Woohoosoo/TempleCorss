 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*2차 시도
public class GameManager : MonoBehaviour
{
    public GameObject grid; // Grid 오브젝트
    public GameObject player; // Player 오브젝트
    public Dictionary<string, List<GameObject>> prefixParentObjects; // 프리픽스와 그에 해당하는 부모 오브젝트 맵
    public float displayTime = 5f; // 오브젝트가 활성화될 시간
    private float currentYPosition = 4.0f; // 시작 위치
    private const int NumberOfMaps = 5; // 생성할 맵의 개수
    private List<GameObject> activeMaps = new List<GameObject>(); // 활성화된 맵 리스트
    private float mapHeight = 10.0f; // 각 맵의 높이

    private bool playerMoving = false; // 플레이어가 움직이고 있는지 여부

    private void Start()
    {
        // 프리픽스와 그에 해당하는 부모 오브젝트를 초기화합니다.
        InitializePrefixParentObjects();

        // 초기 맵을 생성합니다.
        StartCoroutine(GenerateMaps());
    }

    private void Update()
    {
        // 플레이어가 움직이면 맵을 스크롤합니다.
        if (playerMoving)
        {
            ScrollMaps();
        }
    }

    private void InitializePrefixParentObjects()
    {
        prefixParentObjects = new Dictionary<string, List<GameObject>>();

        // Prefabs 폴더 내에서 각 프리픽스에 해당하는 프리팹을 불러옵니다.
        LoadPrefabsForPrefix("Grass");
        LoadPrefabsForPrefix("Ground");
        //LoadPrefabsForPrefix("Water");
        //LoadPrefabsForPrefix("River");
        LoadPrefabsForPrefix("Stone");
    }

    private void LoadPrefabsForPrefix(string prefix)
    {
        List<GameObject> prefabs = new List<GameObject>
        {
            Resources.Load<GameObject>($"Prefabs/{prefix}Top"),
            Resources.Load<GameObject>($"Prefabs/{prefix}Bottom"),
            Resources.Load<GameObject>($"Prefabs/{prefix}Middle")
        };

        prefixParentObjects[prefix] = prefabs;
    }

    private IEnumerator GenerateMaps()
    {
        for (int i = 0; i < NumberOfMaps; i++)
        {
            GenerateSingleMap();
        }

        yield return null; // 모든 맵이 생성된 후에 코루틴을 종료합니다.
    }

    private void GenerateSingleMap()
    {
        List<string> prefixes = new List<string>(prefixParentObjects.Keys);
        string prefix = prefixes[Random.Range(0, prefixes.Count)];
        List<GameObject> prefabs = prefixParentObjects[prefix];

        if (prefabs == null || prefabs.Count < 3)
        {
            Debug.LogError($"Prefabs for prefix '{prefix}' are missing or incomplete.");
            return;
        }

        GameObject topPrefab = prefabs[0];
        if (topPrefab != null)
        {
            GameObject topInstance = Instantiate(topPrefab, grid.transform);
            SetPosition(topInstance, currentYPosition);
            currentYPosition -= topInstance.transform.localScale.y;
            activeMaps.Add(topInstance);
        }
        else
        {
            Debug.LogError($"Top prefab for prefix '{prefix}' is null.");
            return;
        }

        GameObject middlePrefab = prefabs[2];
        if (middlePrefab != null)
        {
            GameObject middleInstance = Instantiate(middlePrefab, grid.transform);
            SetPosition(middleInstance, currentYPosition);
            currentYPosition -= middleInstance.transform.localScale.y;
            activeMaps.Add(middleInstance);
        }
        else
        {
            Debug.LogError($"Middle prefab for prefix '{prefix}' is null.");
            return;
        }

        GameObject bottomPrefab = prefabs[1];
        if (bottomPrefab != null)
        {
            GameObject bottomInstance = Instantiate(bottomPrefab, grid.transform);
            SetPosition(bottomInstance, currentYPosition);
            currentYPosition -= bottomInstance.transform.localScale.y;
            activeMaps.Add(bottomInstance);
        }
        else
        {
            Debug.LogError($"Bottom prefab for prefix '{prefix}' is null.");
            return;
        }
    }

    private void SetPosition(GameObject instance, float yPosition)
    {
        instance.transform.localPosition = new Vector3(0, yPosition, 0);
    }

    public void StartScrolling()
    {
        playerMoving = true;
    }

    private IEnumerator ScrollMaps()
    {
        while (true)
        {
            if (player != null)
            {
                // 플레이어의 현재 위치를 가져옵니다.
                Vector3 playerPosition = player.transform.position;

                // 플레이어가 위로 움직였을 때 맵을 아래로 스크롤합니다.
                if (playerPosition.y > currentYPosition)
                {
                    foreach (GameObject map in activeMaps)
                    {
                        map.transform.position -= Vector3.up * Time.deltaTime;
                    }

                    // 맵의 맨 아래쪽이 특정 위치를 벗어나면 맵을 제거하고 새로운 맵을 생성합니다.
                    if (activeMaps.Count > 0 && activeMaps[0].transform.position.y < -6.0f)
                    {
                        Destroy(activeMaps[0]);
                        activeMaps.RemoveAt(0);
                        GenerateSingleMap();
                    }
                }

                // 플레이어의 현재 위치를 기록합니다.
                currentYPosition = playerPosition.y;
            }

            yield return null;
        }
    }



}
*/









/*
 * 1차시도
public class GameManager : MonoBehaviour
{
    public GameObject grid; // Grid 오브젝트
    public Dictionary<string, List<GameObject>> prefixParentObjects; // 프리픽스와 그에 해당하는 부모 오브젝트 맵
    public float displayTime = 5f; // 오브젝트가 활성화될 시간
    private float currentYPosition = -9.0f; // 시작 위치

    // Start 메소드에서 prefixParentObjects을 초기화하고 GenerateMap 코루틴을 시작합니다.
    private void Start()
    {
        // 프리픽스와 그에 해당하는 부모 오브젝트를 초기화합니다.
        InitializePrefixParentObjects();

        // StartCoroutine을 통해 코루틴을 시작합니다.
        StartCoroutine(GenerateMap());
    }

    // 프리픽스와 그에 해당하는 부모 오브젝트를 초기화하는 메소드
    private void InitializePrefixParentObjects()
    {
        prefixParentObjects = new Dictionary<string, List<GameObject>>();

        // Prefabs 폴더 내에서 각 프리픽스에 해당하는 프리팹을 불러옵니다.
        LoadPrefabsForPrefix("Grass");
        LoadPrefabsForPrefix("Ground");
        LoadPrefabsForPrefix("Water");
        LoadPrefabsForPrefix("River");
        LoadPrefabsForPrefix("Stone");
    }

    // 특정 접두사에 대한 프리팹들을 불러오는 메소드
    private void LoadPrefabsForPrefix(string prefix)
    {
        List<GameObject> prefabs = new List<GameObject>
        {
            Resources.Load<GameObject>($"Prefabs/{prefix}Top"),
            Resources.Load<GameObject>($"Prefabs/{prefix}Bottom"),
            Resources.Load<GameObject>($"Prefabs/{prefix}Middle")
        };

        prefixParentObjects[prefix] = prefabs;
    }

    // 맵을 생성하는 코루틴
    private IEnumerator GenerateMap()
    {
        while (currentYPosition <= 15.0f)
        {
            // 랜덤 프리픽스를 선택합니다.
            List<string> prefixes = new List<string>(prefixParentObjects.Keys);
            string prefix = prefixes[Random.Range(0, prefixes.Count)];
            List<GameObject> prefabs = prefixParentObjects[prefix];

            // 프리팹들이 없으면 다음 반복으로 넘어갑니다.
            if (prefabs == null || prefabs.Count < 3)
            {
                continue;
            }

            // top 프리팹 생성 및 위치 설정
            GameObject topInstance = Instantiate(prefabs[0], grid.transform);
            SetPosition(topInstance, currentYPosition); // 위치를 설정 (상단)
            currentYPosition -= topInstance.transform.localScale.y; // 다음 오브젝트의 시작 위치 업데이트

            // middle 프리팹 생성 및 위치 설정
            GameObject middleInstance = Instantiate(prefabs[2], grid.transform);
            SetPosition(middleInstance, currentYPosition); // 위치 설정 (중단)
            currentYPosition -= middleInstance.transform.localScale.y; // 다음 오브젝트의 시작 위치 업데이트

            // bottom 프리팹 생성 및 위치 설정
            GameObject bottomInstance = Instantiate(prefabs[1], grid.transform);
            SetPosition(bottomInstance, currentYPosition); // 위치 설정 (하단)
            currentYPosition -= bottomInstance.transform.localScale.y; // 다음 오브젝트의 시작 위치 업데이트

            // 일정 시간 후 오브젝트를 비활성화합니다.
            yield return new WaitForSeconds(displayTime);

            topInstance.SetActive(false);
            middleInstance.SetActive(false);
            bottomInstance.SetActive(false);
        }
    }

    // 오브젝트의 위치를 설정하는 함수
    private void SetPosition(GameObject instance, float yPosition)
    {
        instance.transform.localPosition = new Vector3(0, yPosition, 0);
    }
}
*/