using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    BlockController blockController;

    const int FIELD_WIDTH = 8;
    const int FIELD_HEIGHT = 8;

    [SerializeField] int BombNum = 10;

    GameObject[,] objArray;

    // Start is called before the first frame update
    void Start()
    {
        objArray = new GameObject[FIELD_HEIGHT, FIELD_WIDTH];
        if (BombNum >= FIELD_HEIGHT * FIELD_WIDTH - 1)
        {
            BombNum = FIELD_HEIGHT * FIELD_WIDTH - 1;
        }

        List<int> bombIDList = new List<int>();

        // 乱数の候補を全部リストに入れる
        int randomMax = FIELD_HEIGHT * FIELD_WIDTH - 1;
        List<int> randomNumList = new List<int>();
        for (int index = 0; index <= randomMax; ++index)
        {
            randomNumList.Add(index);
        }

        // 上のリストからランダムに数を抽出する
        Random.InitState(System.DateTime.Now.Millisecond);  // 乱数のシード値は日付を使用
        for (int count = 0; count < BombNum; ++count)
        {
            int index = Random.Range(0, randomNumList.Count);
            int bombID = randomNumList[index];
            randomNumList.RemoveAt(index);
            Debug.Log(bombID);
            bombIDList.Add(bombID);
        }

        GameObject obj = (GameObject)Resources.Load("GrayPanel");

        for(int height = 0; height < FIELD_HEIGHT; ++height)
        {
            for(int width = 0; width < FIELD_WIDTH; ++width)
            {
                GameObject instance = (GameObject)Instantiate(obj, new Vector3(height - FIELD_HEIGHT / 2, width - FIELD_WIDTH / 2, 0), Quaternion.identity);

                blockController = instance.GetComponent<BlockController>();

                blockController.SetID(height * FIELD_WIDTH + width);
                blockController.SetIndex(width, height);
                // 爆弾IDリストと比較して一致すれば爆弾フラグオン
                foreach(var bombID in bombIDList)
                {
                    if((height * FIELD_WIDTH + width) == bombID)
                    {
                        blockController.SetBomb(true);
                        break;
                    }
                }

                objArray[height, width] = instance;
            }
        }




    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void SwitchPushedColor(int height, int width)
    {
        Vector3 pos = objArray[height, width].transform.position;

        GameObject pushedPrefab = (GameObject)Resources.Load("GrayPanelPushed");
        GameObject pushedInst = (GameObject)Instantiate(pushedPrefab, pos, Quaternion.identity);

        Destroy(objArray[height, width]);
        objArray[height, width] = pushedInst;
    }
}
