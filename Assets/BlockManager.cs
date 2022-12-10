using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    int blockCount = 0;
    Block[] childrens;
    private void OnEnable()
    {
        childrens = GetComponentsInChildren<Block>(true);
        foreach(Block child in childrens)
        {
            AddBlock(child);
        }

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
        if (blockCount == 0) GamePlayManager.instance.GamePlayState = GamePlayState.Won;
    }

    public void OnGamePlayState(GamePlayState gamePlayState)
    {
        if (gamePlayState == GamePlayState.Ready)
        {
            blockCount = 0;
            foreach (Block block in childrens)
                block.gameObject.SetActive(true);
        }

    }
}
