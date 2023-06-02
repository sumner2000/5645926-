using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern07 : MonoBehaviour
{
    [SerializeField]
    private GameObject ground;                  // �߰� ����
    [SerializeField]
    private GameObject doctorKO;                // ��ڻ�
    [SerializeField]
    private GameObject laser;                   // ��ڻ� �� ��ġ�� �ִ� ������(��)
    [SerializeField]
    private Collider2D[] laserCollider2D;       // �������� Collider2D
    [SerializeField]
    private float rotateTime;               // ȸ�� �ð�
    [SerializeField]
    private int anglePerSeconds;        // �ʴ� ȸ�� ����

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

        // ����, ��ڻ�, ������ Ȱ��ȭ
        ground.SetActive(true);
        doctorKO.SetActive(true);
        laser.SetActive(true);

        // �������� �������ڸ��� �÷��̾�� �浹���� �ʵ��� Collider2D�� ��� ��Ȱ��ȭ
        for (int i = 0; i < laserCollider2D.Length; ++i)
        {
            laserCollider2D[i].enabled = false;
        }

        // �������� �����ϰ� ��� ����ϴ� �ð�
        yield return new WaitForSeconds(0.5f);

        // �������� Collider2D Ȱ��ȭ
        for (int i = 0; i < laserCollider2D.Length; ++i)
        {
            laserCollider2D[i].enabled = true;
        }

        // ������ ȸ�� (�ð�)
        float time = 0;
        while (time < rotateTime)
        {
            laser.transform.Rotate(Vector3.forward * anglePerSeconds * Time.deltaTime);

            time += Time.deltaTime;

            yield return null;
        }

        // ���� �� ������, ��ڻ�, ���� ��Ȱ��ȭ
        ground.SetActive(false);
        doctorKO.SetActive(false);
        laser.SetActive(false);

        // ���� ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}