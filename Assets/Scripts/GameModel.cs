using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameModel : MonoBehaviour
{
    
    public GameObject[] cards;
    public GameObject handArea;
    public GameObject[] opponentLanes;
    public GameObject turnTextObject;

    private Dictionary<string, GameObject> _opponentLanesDictionary;
    private List<GameObject> _playerHand;
    private List<GameObject> _opponentHand;
    private GameObject _playedPlayerCard;
    private bool _playerTurn = true;
    public bool PlayerTurn
    {
        get => _playerTurn;
        set
        {
            if (value) throw new ArgumentException("Cannot set to true");
            _playerTurn = false;
        } 
        
    }

    private bool _opponentTurn = false;
    private TextMeshProUGUI _turnText;
    
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

        _turnText = turnTextObject.GetComponent<TextMeshProUGUI>();

        StartCoroutine(DrawCards());
    }

    private IEnumerator DrawCards()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.2f);

            _opponentHand.Add(GetRandomCard());

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
        if (PlayerTurn)
        {
            _turnText.text = "Player's turn";
        }
        else if (!_opponentTurn)
        {
            _opponentTurn = true;
            _turnText.text = "Opponent's turn";
            StartCoroutine(PlayOpponentCard());
        }
    }

    public bool RemovePlayedCard(GameObject cardToRemove)
    {
        CardAttributes toRemoveAttributes = cardToRemove.GetComponent<CardAttributes>();

        foreach (GameObject card in _playerHand)
        {
            CardAttributes attributes = card.GetComponent<CardAttributes>();
            if (toRemoveAttributes.Equals(attributes))
            {
                return _playerHand.Remove(card);
            }
        }

        return false;
    }

    public IEnumerator PlayOpponentCard()
    {
        yield return new WaitForSeconds(1.5f);

        if (_opponentHand.Count >= 1)
        {
            int index = Random.Range(0, _opponentHand.Count);
            GameObject card = Instantiate(_opponentHand[index]);
            GameObject lane = _opponentLanesDictionary[LayerMask.LayerToName(card.layer)];
            card.transform.SetParent(lane.transform, false);
            _opponentHand.RemoveAt(index);
        }
        _playerTurn = true;
        _opponentTurn = false;
    }
}
