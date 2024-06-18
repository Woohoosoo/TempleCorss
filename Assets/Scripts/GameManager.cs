 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*2�� �õ�
public class GameManager : MonoBehaviour
{
    public GameObject grid; // Grid ������Ʈ
    public GameObject player; // Player ������Ʈ
    public Dictionary<string, List<GameObject>> prefixParentObjects; // �����Ƚ��� �׿� �ش��ϴ� �θ� ������Ʈ ��
    public float displayTime = 5f; // ������Ʈ�� Ȱ��ȭ�� �ð�
    private float currentYPosition = 4.0f; // ���� ��ġ
    private const int NumberOfMaps = 5; // ������ ���� ����
    private List<GameObject> activeMaps = new List<GameObject>(); // Ȱ��ȭ�� �� ����Ʈ
    private float mapHeight = 10.0f; // �� ���� ����

    private bool playerMoving = false; // �÷��̾ �����̰� �ִ��� ����

    private void Start()
    {
        // �����Ƚ��� �׿� �ش��ϴ� �θ� ������Ʈ�� �ʱ�ȭ�մϴ�.
        InitializePrefixParentObjects();

        // �ʱ� ���� �����մϴ�.
        StartCoroutine(GenerateMaps());
    }

    private void Update()
    {
        // �÷��̾ �����̸� ���� ��ũ���մϴ�.
        if (playerMoving)
        {
            ScrollMaps();
        }
    }

    private void InitializePrefixParentObjects()
    {
        prefixParentObjects = new Dictionary<string, List<GameObject>>();

        // Prefabs ���� ������ �� �����Ƚ��� �ش��ϴ� �������� �ҷ��ɴϴ�.
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

        yield return null; // ��� ���� ������ �Ŀ� �ڷ�ƾ�� �����մϴ�.
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
                // �÷��̾��� ���� ��ġ�� �����ɴϴ�.
                Vector3 playerPosition = player.transform.position;

                // �÷��̾ ���� �������� �� ���� �Ʒ��� ��ũ���մϴ�.
                if (playerPosition.y > currentYPosition)
                {
                    foreach (GameObject map in activeMaps)
                    {
                        map.transform.position -= Vector3.up * Time.deltaTime;
                    }

                    // ���� �� �Ʒ����� Ư�� ��ġ�� ����� ���� �����ϰ� ���ο� ���� �����մϴ�.
                    if (activeMaps.Count > 0 && activeMaps[0].transform.position.y < -6.0f)
                    {
                        Destroy(activeMaps[0]);
                        activeMaps.RemoveAt(0);
                        GenerateSingleMap();
                    }
                }

                // �÷��̾��� ���� ��ġ�� ����մϴ�.
                currentYPosition = playerPosition.y;
            }

            yield return null;
        }
    }



}
*/









/*
 * 1���õ�
public class GameManager : MonoBehaviour
{
    public GameObject grid; // Grid ������Ʈ
    public Dictionary<string, List<GameObject>> prefixParentObjects; // �����Ƚ��� �׿� �ش��ϴ� �θ� ������Ʈ ��
    public float displayTime = 5f; // ������Ʈ�� Ȱ��ȭ�� �ð�
    private float currentYPosition = -9.0f; // ���� ��ġ

    // Start �޼ҵ忡�� prefixParentObjects�� �ʱ�ȭ�ϰ� GenerateMap �ڷ�ƾ�� �����մϴ�.
    private void Start()
    {
        // �����Ƚ��� �׿� �ش��ϴ� �θ� ������Ʈ�� �ʱ�ȭ�մϴ�.
        InitializePrefixParentObjects();

        // StartCoroutine�� ���� �ڷ�ƾ�� �����մϴ�.
        StartCoroutine(GenerateMap());
    }

    // �����Ƚ��� �׿� �ش��ϴ� �θ� ������Ʈ�� �ʱ�ȭ�ϴ� �޼ҵ�
    private void InitializePrefixParentObjects()
    {
        prefixParentObjects = new Dictionary<string, List<GameObject>>();

        // Prefabs ���� ������ �� �����Ƚ��� �ش��ϴ� �������� �ҷ��ɴϴ�.
        LoadPrefabsForPrefix("Grass");
        LoadPrefabsForPrefix("Ground");
        LoadPrefabsForPrefix("Water");
        LoadPrefabsForPrefix("River");
        LoadPrefabsForPrefix("Stone");
    }

    // Ư�� ���λ翡 ���� �����յ��� �ҷ����� �޼ҵ�
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

    // ���� �����ϴ� �ڷ�ƾ
    private IEnumerator GenerateMap()
    {
        while (currentYPosition <= 15.0f)
        {
            // ���� �����Ƚ��� �����մϴ�.
            List<string> prefixes = new List<string>(prefixParentObjects.Keys);
            string prefix = prefixes[Random.Range(0, prefixes.Count)];
            List<GameObject> prefabs = prefixParentObjects[prefix];

            // �����յ��� ������ ���� �ݺ����� �Ѿ�ϴ�.
            if (prefabs == null || prefabs.Count < 3)
            {
                continue;
            }

            // top ������ ���� �� ��ġ ����
            GameObject topInstance = Instantiate(prefabs[0], grid.transform);
            SetPosition(topInstance, currentYPosition); // ��ġ�� ���� (���)
            currentYPosition -= topInstance.transform.localScale.y; // ���� ������Ʈ�� ���� ��ġ ������Ʈ

            // middle ������ ���� �� ��ġ ����
            GameObject middleInstance = Instantiate(prefabs[2], grid.transform);
            SetPosition(middleInstance, currentYPosition); // ��ġ ���� (�ߴ�)
            currentYPosition -= middleInstance.transform.localScale.y; // ���� ������Ʈ�� ���� ��ġ ������Ʈ

            // bottom ������ ���� �� ��ġ ����
            GameObject bottomInstance = Instantiate(prefabs[1], grid.transform);
            SetPosition(bottomInstance, currentYPosition); // ��ġ ���� (�ϴ�)
            currentYPosition -= bottomInstance.transform.localScale.y; // ���� ������Ʈ�� ���� ��ġ ������Ʈ

            // ���� �ð� �� ������Ʈ�� ��Ȱ��ȭ�մϴ�.
            yield return new WaitForSeconds(displayTime);

            topInstance.SetActive(false);
            middleInstance.SetActive(false);
            bottomInstance.SetActive(false);
        }
    }

    // ������Ʈ�� ��ġ�� �����ϴ� �Լ�
    private void SetPosition(GameObject instance, float yPosition)
    {
        instance.transform.localPosition = new Vector3(0, yPosition, 0);
    }
}
*/