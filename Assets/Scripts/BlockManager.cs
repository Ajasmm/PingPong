using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    int blockCount = 0;
    Block[] childrens;
    private void OnEnable()
    {
        childrens = new Block[0];
        OnGamePlayState(GamePlayState.Ready);
        GamePlayManager.instance.OnGamePlayState += OnGamePlayState;
    }

    private void OnDisable() => GamePlayManager.instance.OnGamePlayState -= OnGamePlayState;

    public void AddBlock(Block block)
    {
        blockCount++;
        block.OnDestroy += RemoveBlock;
    }

    public void RemoveBlock()
    {
        blockCount--;
        if (blockCount == 0 && GamePlayManager.instance.GamePlayState == GamePlayState.Playing) GamePlayManager.instance.GamePlayState = GamePlayState.Won;
    }

    public void OnGamePlayState(GamePlayState gamePlayState)
    {
        if (gamePlayState == GamePlayState.Ready)
        {
            foreach (Block block in childrens)
            {
                block.gameObject.SetActive(false);
                block.gameObject.SetActive(true);
            }
            blockCount = 0;
            childrens = GetComponentsInChildren<Block>(true);
            foreach (Block child in childrens) AddBlock(child);
        }

    }
}
