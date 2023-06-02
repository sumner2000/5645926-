using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern06 : MonoBehaviour
{
    [SerializeField]
    private GameObject[] warningImages;     // ��� �̹���
    [SerializeField]
    private GameObject doctorKO;            // ��ڻ� ������Ʈ
    [SerializeField]
    private GameObject projectilePrefab;    // �߻�ü ������
    [SerializeField]
    private float spawnCycle;           // ���� �ֱ�
    [SerializeField]
    private int maxCount;           // Ƚ��

    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }

    private void OnDisable()
    {
        doctorKO.GetComponent<MovingEntity>().Reset();

        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        // ���� ���� �� ��� ����ϴ� �ð�
        yield return new WaitForSeconds(1);

        // ��� �̹����� 0.5�� ���� Ȱ��ȭ�ߴٰ� ��Ȱ��ȭ
        warningImages[0].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningImages[0].SetActive(false);

        // ��ڻ� ������Ʈ�� ������ �Ʒ��� �̵�
        yield return StartCoroutine(nameof(MoveDown));

        // C# �߻�ü ����
        yield return StartCoroutine(nameof(SpawnProjectile));

        // ��� �̹����� 0.5�� ���� Ȱ��ȭ�ߴٰ� ��Ȱ��ȭ
        warningImages[1].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningImages[1].SetActive(false);

        // ��ڻ� ������Ʈ �� or �� �̵�
        yield return StartCoroutine(nameof(MoveHorizontal));

        // ���� ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);
    }

    private IEnumerator MoveDown()
    {
        // ��ǥ ��ġ
        float destinationY = -2.7f;

        // ��ڻ� ������Ʈ Ȱ��ȭ
        doctorKO.gameObject.SetActive(true);

        // ��ڻ簡 ��ǥ��ġ���� �̵��ߴ��� �˻�
        while (true)
        {
            if (doctorKO.transform.position.y <= destinationY)
            {
                doctorKO.GetComponent<MovementTransform2D>().MoveTo(Vector3.zero);

                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator SpawnProjectile()
    {
        float minSpeed = 2;
        float maxSpeed = 10;

        int count = 0;
        while (count < maxCount)
        {
            GameObject clone = Instantiate(projectilePrefab, doctorKO.transform.position, Quaternion.identity);
            var movement2D = clone.GetComponent<MovementRigidbody2D>();

            movement2D.MoveSpeed = Random.Range(minSpeed, maxSpeed);
            movement2D.MoveTo(1 - 2 * Random.Range(0, 2));
            movement2D.IsLongJump = Random.Range(0, 2) == 0 ? false : true;
            movement2D.JumpTo();

            count++;

            yield return new WaitForSeconds(spawnCycle);
        }
    }

    private IEnumerator MoveHorizontal()
    {
        Vector3 direction = Random.Range(0, 2) == 0 ? Vector3.left : Vector3.right;
        doctorKO.GetComponent<MovementTransform2D>().MoveTo(direction);

        while (true)
        {
            if (doctorKO.transform.position.x < Constants.min.x ||
                 doctorKO.transform.position.x > Constants.max.x)
            {
                doctorKO.gameObject.SetActive(false);

                yield break;
            }

            yield return null;
        }
    }
}
