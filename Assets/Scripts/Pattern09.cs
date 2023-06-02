using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern09 : MonoBehaviour
{
    [SerializeField]
    private GameObject warningObject;   // ��� ������Ʈ
    [SerializeField]
    private MovementTransform2D unityLogo;      // ����Ƽ �ΰ� ������Ʈ
    [SerializeField]
    private float moveTime;     // �̵� �ð�
    [SerializeField]
    private float minX = -2.7f; // ���� x ��ġ
    [SerializeField]
    private float maxX = 2.7f;	// ������ x ��ġ

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

        // ��� ������Ʈ Ȱ��-��Ȱ��
        warningObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningObject.SetActive(false);

        //  ������Ʈ Ȱ��ȭ
        unityLogo.gameObject.SetActive(true);

        // ó�� �̵� ������ ���������� ����
        unityLogo.MoveTo(Vector3.right);

        //  ������Ʈ ��/�� �̵�
        float time = 0;
        while (time < moveTime)
        {
            time += Time.deltaTime;

            // ������Ʈ�� ��ġ�� ���� �ּ� ������ �Ѿ�� �̵� ������ ���������� ����
            if (unityLogo.transform.position.x <= minX)
            {
                unityLogo.MoveTo(Vector3.right);
            }
            // ������Ʈ�� ��ġ�� ������ �ִ� ������ �Ѿ�� �̵� ������ �������� ����
            else if (unityLogo.transform.position.x >= maxX)
            {
                unityLogo.MoveTo(Vector3.left);
            }

            yield return null;
        }

        //  ������Ʈ ��Ȱ��ȭ
        unityLogo.gameObject.SetActive(false);

        // ���� ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}
