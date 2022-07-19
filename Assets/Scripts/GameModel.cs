using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameModel : MonoBehaviour
{
    
    public GameObject[] cards;
    public GameObject handArea;
    public GameObject[] opponentLanes;

    private Dictionary<string, GameObject> _opponentLanesDictionary;
    private List<GameObject> _playerHand;
    private List<GameObject> _opponentHand;
    private bool _playerTurn = true; // TODO: Make property, get, set only to false outside of class

    public static GameModel Instance;
    
    void Start()
    {
        if (!Instance) Instance = this;
        
        _playerHand = new List<GameObject>(10);
        _opponentHand = new List<GameObject>(10);
        
        _opponentLanesDictionary = new Dictionary<string, GameObject>
        {
            { "Melee", opponentLanes[0] },
            { "Ranged", opponentLanes[1] },
            { "Siege", opponentLanes[2] }
        };

        StartCoroutine(DrawCards());
    }

    private IEnumerator DrawCards()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.2f);

            _opponentHand.Add(GetRandomCard());
            // _opponentHand[i] = GetRandomCard();

            GameObject playerCard = GetRandomCard();
            _playerHand.Add(playerCard);
            Instantiate(playerCard, handArea.transform, false);
        }
    }

    private GameObject GetRandomCard()
    {
        return cards[Random.Range(0, cards.Length)];
    }

    void Update()
    {
        // TODO: Play opponent card when not player's turn.
        // TODO: Turn logic
    }

    public void SetOpponentTurn()
    {
        _playerTurn = false;
    }

    public void PlayOpponentCard()
    {
        if (_opponentHand.Count >= 1)
        {
            int index = Random.Range(0, _opponentHand.Count);
            GameObject card = Instantiate(_opponentHand[index]);
            GameObject lane = _opponentLanesDictionary[LayerMask.LayerToName(card.layer)];
            card.transform.SetParent(lane.transform, false);
            _opponentHand.RemoveAt(index);
        }
    }
}
