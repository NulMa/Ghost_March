using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //������ ���� ����
    public GameObject[] prefabs;

    //Ǯ ��� ����Ʈ
    List<GameObject>[] pools;

    private void Awake() {
        pools = new List<GameObject>[prefabs.Length];

        for(int index = 0; index < pools.Length; index++) {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index) {
        GameObject select = null;
        //���� Ǯ�� ���� ������Ʈ�� ����
    
        //����Ʈ ��ȸ
        foreach (GameObject item in pools[index]) {
            if (!item.activeSelf) {
                // ���� �� select ������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }

        //���� ���н�
        
        if (!select) {
            //���� ���� �� select�� �Ҵ�
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
