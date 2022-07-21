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
    public GameObject[] scoreText;

    private Dictionary<string, GameObject> _scoreTextDictionary;
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

        _scoreTextDictionary = new Dictionary<string, GameObject> // TODO: Find good way to organize score texts and corresponding lanes. Perhaps dict with string and pair of lane and score text.
        {
            { "PlayerSiege", scoreText[0] }
        };

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

    void Update() // TODO: Add round logic
    { // TODO: Add ability to pass a round
        
        // TODO: Get all lanes in a list to calculate score in loop
        GameObject lane = opponentLanes[0];
        CardAttributes[] cardAttributesArray = lane.GetComponentsInChildren<CardAttributes>();
        int score = 0;
        foreach (CardAttributes attributes in cardAttributesArray)
        {
            score += attributes.power;
        }

        Debug.Log(score);
        
        

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

    private IEnumerator PlayOpponentCard()
    {
        yield return new WaitForSeconds(1.5f);

        if (_opponentHand.Count >= 1)
        {
            int index = Random.Range(0, _opponentHand.Count);
            GameObject card = Instantiate(_opponentHand[index]);
            GameObject lane = _opponentLanesDictionary[LayerMask.LayerToName(card.layer)]; // TODO: Lookup "Opponent" + card.layer
            card.transform.SetParent(lane.transform, false);
            _opponentHand.RemoveAt(index);
        }
        _playerTurn = true;
        _opponentTurn = false;
    }
}
