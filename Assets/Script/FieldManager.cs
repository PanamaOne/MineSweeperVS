using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public enum BlockType
    {
        BlockType_Normal = 0,   // ボム無しの普通のマス
        BlockType_Bomb,         // ボムありマス
        BlockType_Wall,         // 外壁

        BlockType_Num,
    }

    BlockController blockController;

    // 外壁抜きのサイズ
    const int FIELD_WIDTH = 8;      
    const int FIELD_HEIGHT = 8;

    // 外壁ありのサイズ
    const int ALL_FIELD_WIDTH = FIELD_WIDTH + 2;
    const int ALL_FIELD_HEIGHT = FIELD_HEIGHT + 2;

    [SerializeField] int BombNum = 10;

    public GameObject[,] objArray;
    
    // Start is called before the first frame update
    void Start()
    {
        objArray = new GameObject[ALL_FIELD_HEIGHT, ALL_FIELD_WIDTH];
        if (BombNum >= FIELD_HEIGHT * FIELD_WIDTH - 1)
        {
            BombNum = FIELD_HEIGHT * FIELD_WIDTH / 2;
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

        GameObject grayPanel = (GameObject)Resources.Load("GrayPanel");
        GameObject whiteWall = (GameObject)Resources.Load("WhitePanel");
        int IDCounter = 0;

        for(int height = 0; height < ALL_FIELD_HEIGHT; ++height)
        {
            for(int width = 0; width < ALL_FIELD_WIDTH; ++width)
            {
                GameObject inst;
                if(height == 0 || height == ALL_FIELD_HEIGHT - 1 || width == 0 || width == ALL_FIELD_WIDTH - 1)
                {
                    inst = (GameObject)Instantiate(whiteWall, new Vector3(height - ALL_FIELD_HEIGHT / 2, width - ALL_FIELD_WIDTH / 2, 0), Quaternion.identity);
                    blockController = inst.GetComponent<BlockController>();
                    blockController.SetID(-1);
                    blockController.SetBlockType(BlockType.BlockType_Wall);
                    blockController.SetIndex(width, height);
                }
                else
                {
                    inst = (GameObject)Instantiate(grayPanel, new Vector3(height - ALL_FIELD_HEIGHT / 2, width - ALL_FIELD_WIDTH / 2, 0), Quaternion.identity);
                    blockController = inst.GetComponent<BlockController>();
                    blockController.SetID(IDCounter);
                    blockController.SetBlockType(BlockType.BlockType_Normal);
                    blockController.SetIndex(width, height);

                    // 爆弾IDリストと比較して一致すれば爆弾フラグオン
                    foreach (var bombID in bombIDList)
                    {
                        if (IDCounter == bombID)
                        {
                            blockController.SetBomb(true);
                            break;
                        }
                    }
                    ++IDCounter;
                }
                objArray[height, width] = inst;
            }
        }

        // 爆弾をカウント
        for(int height = 0;height < ALL_FIELD_HEIGHT; ++height)
        {
            for (int width = 0; width < ALL_FIELD_WIDTH; ++width)
            {
                int bombNum = CountBomb(height, width);
                objArray[height, width].GetComponent<BlockController>().SetBombCount(bombNum);
            }
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void SwitchPushedColor(int height, int width, int bombNum)
    {
        Vector3 pos = objArray[height, width].transform.position;

        GameObject pushedPrefab = (GameObject)Resources.Load("GrayPanelPushed");
        GameObject pushedInst = (GameObject)Instantiate(pushedPrefab, pos, Quaternion.identity);

        Destroy(objArray[height, width]);
        objArray[height, width] = pushedInst;

        Sprite[] sprites = Resources.LoadAll<Sprite>("MineSweeper");
        
        switch (bombNum)
        {
            case 0:
                break;
            case 1:
                pushedInst.transform.Find("image").GetComponent<SpriteRenderer>().sprite = sprites[5];
                break;
            case 2:
                pushedInst.transform.Find("image").GetComponent<SpriteRenderer>().sprite = sprites[6];
                break;
            case 3:
                pushedInst.transform.Find("image").GetComponent<SpriteRenderer>().sprite = sprites[7];
                break;
            case 4:
                pushedInst.transform.Find("image").GetComponent<SpriteRenderer>().sprite = sprites[8];
                break;
            case 5:
                pushedInst.transform.Find("image").GetComponent<SpriteRenderer>().sprite = sprites[9];
                break;
            case 6:
                pushedInst.transform.Find("image").GetComponent<SpriteRenderer>().sprite = sprites[10];
                break;
            case 7:
                pushedInst.transform.Find("image").GetComponent<SpriteRenderer>().sprite = sprites[11];
                break;
            case 8:
                pushedInst.transform.Find("image").GetComponent<SpriteRenderer>().sprite = sprites[12];
                break;
        }
        
        
    }

    int CountBomb(int height, int width)
    {
        if(height == 0 || height == ALL_FIELD_HEIGHT - 1 || width == 0 || width == ALL_FIELD_WIDTH - 1)
        {
            // 外壁が指定されているのでおかしい
            return -1;
        }

        int bombCounter = 0;
        if(objArray[height - 1, width -1].GetComponent<BlockController>().GetBomb())
        {
            ++bombCounter;
        }
        if (objArray[height - 1, width].GetComponent<BlockController>().GetBomb())
        {
            ++bombCounter;
        }
        if (objArray[height - 1, width + 1].GetComponent<BlockController>().GetBomb())
        {
            ++bombCounter;
        }
        if (objArray[height, width - 1].GetComponent<BlockController>().GetBomb())
        {
            ++bombCounter;
        }
        if (objArray[height, width + 1].GetComponent<BlockController>().GetBomb())
        {
            ++bombCounter;
        }
        if (objArray[height + 1, width - 1].GetComponent<BlockController>().GetBomb())
        {
            ++bombCounter;
        }
        if (objArray[height + 1, width].GetComponent<BlockController>().GetBomb())
        {
            ++bombCounter;
        }
        if (objArray[height + 1, width + 1].GetComponent<BlockController>().GetBomb())
        {
            ++bombCounter;
        }
        return bombCounter;
    }
}
