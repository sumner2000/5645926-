using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern02 : MonoBehaviour
{
    [SerializeField]
    private GameObject[] warningImages;     // ��� �̹���
    [SerializeField]
    private GameObject[] playerObjects;     // �÷��̾� ������Ʈ
    [SerializeField]
    private float spawnCycle = 1;     // ���� �ֱ�

    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }

    private void OnDisable()
    {
        // ���� ����� ���� �÷��̾� ������Ʈ�� ���¸� �ʱ�ȭ
        for (int i = 0; i < playerObjects.Length; ++i)
        {
            playerObjects[i].SetActive(false);
            playerObjects[i].GetComponent<MovingEntity>().Reset();
        }

        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        // ���� ���� �� ��� ����ϴ� �ð�
        yield return new WaitForSeconds(1);

        // �÷��̾��� ���� ��ġ�� ��ġ�� �ʴ� ������ ��ġ�� ����
        int[] numbers = Utils.RandomNumbers(3, 3);

        int index = 0;
        while (index < numbers.Length)
        {
            StartCoroutine(nameof(SpawnPlayer), numbers[index]);

            index++;

            yield return new WaitForSeconds(spawnCycle);
        }

        // ���� ���� �� ��� ����ϴ� �ð�
        yield return new WaitForSeconds(1);

        // ���� ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);
    }

    private IEnumerator SpawnPlayer(int index)
    {
        // ��� �̹��� Ȱ��/��Ȱ��
        warningImages[index].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningImages[index].SetActive(false);

        // �÷��̾� ������Ʈ Ȱ��ȭ
        playerObjects[index].SetActive(true);
    }
}
