using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern08 : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;      // �÷��̾� Transform
    [SerializeField]
    private GameObject warningImage;            // ��� �̹���
    [SerializeField]
    private GameObject prefab;                  // ������
    [SerializeField]
    private float spawnCycle;               // ���� �ֱ�
    [SerializeField]
    private int maxCount;				// �ִ� ���� ����

    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        // ���� ���� �� ��� ����ϴ� �ð�
        yield return new WaitForSeconds(1);

        int count = 0;
        while (count < maxCount)
        {
            StartCoroutine(nameof(SpawnPrefab));

            count++;

            yield return new WaitForSeconds(spawnCycle);
        }

        // ���� ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);
    }

    private IEnumerator SpawnPrefab()
    {
        // ���� �÷��̾� ��ġ�� ��� �̹��� ���
        GameObject warningClone = Instantiate(warningImage, playerTransform.position, Quaternion.identity);
        warningClone.transform.localScale = Vector3.one;

        yield return new WaitForSeconds(0.5f);

        // ��� �̹��� ��ġ�� ������ ����
        GameObject prefabClone = Instantiate(prefab, warningClone.transform.position, Quaternion.identity);
        Destroy(warningClone);
        Destroy(prefabClone, 0.5f);
    }
}

