using System.Collections;
using UnityEngine;

public class Pattern01 : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab; // �� ������
    [SerializeField]
    private int maxEnemyCount;		// �� ���� ����
    [SerializeField]
    private float spawnCycle;       // ���� �ֱ�

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(SpawnEnemys));
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(SpawnEnemys));
    }

    private IEnumerator SpawnEnemys()
    {
        // ���� ���� �� ��� ����ϴ� �ð�
        float waitTime = 1f;
        yield return new WaitForSeconds(waitTime);

        int count = 0;
        //while ( true )
        while (count < maxEnemyCount)
        {
            // ���� ����� ����� ����Ǹ� �ٽ� ���
            /*if (audioSource.isPlaying == false)
            {
                audioSource.Play();
            }*/

            Vector3 position = new Vector3(Random.Range(Constants.min.x, Constants.max.x), Constants.max.y, 0);
            Instantiate(enemyPrefab, position, Quaternion.identity);

            yield return new WaitForSeconds(spawnCycle);
            
            count++;
        }
        // ���� ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}
