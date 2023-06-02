using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern03 : MonoBehaviour
{
    [SerializeField]
    private GameObject warningImage;    // ��� �̹���
    [SerializeField]
    private Transform boom;         // ��ź
    [SerializeField]
    private GameObject boomEffect;     // ��ź ����Ʈ

    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }

    private void OnDisable()
    {
        boom.GetComponent<MovingEntity>().Reset();

        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        // ���� ���� �� ��� ����ϴ� �ð�
        yield return new WaitForSeconds(1);

        // ��� �̹��� Ȱ��/��Ȱ��
        warningImage.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningImage.SetActive(false);

        // ��ź ������Ʈ Ȱ��ȭ & �̵�
        yield return StartCoroutine(nameof(MoveUp));

        // ��ź ����Ʈ Ȱ��/��Ȱ��
        boomEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        boomEffect.SetActive(false);

        // ���� ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);
    }

    private IEnumerator MoveUp()
    {
        // ��ǥ ��ġ
        float boomDestinationY = 0;

        // ��ź ������Ʈ�� Ȱ��ȭ
        boom.gameObject.SetActive(true);

        // ��ź�� ��ǥ��ġ���� �̵��ߴ��� �˻�
        while (true)
        {
            if (boom.transform.position.y >= boomDestinationY)
            {
                // ��ź ������Ʈ ��Ȱ��ȭ
                boom.gameObject.SetActive(false);

                yield break;
            }

            yield return null;
        }
    }
}
