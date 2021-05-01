using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Models;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GameMaster _master;
    [SerializeField] private UIController ui;
    [SerializeField] private GamePreset preset;

    public event Action<CardModel> OnCardDroped;
    public event Action<CardModel> OnCardPicked;
    public event Action<CardModel[]> OnCombinationCrieted;
    public event Action<int> OnGameEnded;
    public event Action OnSessionsGameStarted;
    public event Action OnStartGame;
    public event Action OnNextTurn;
    
    private void Awake()
    {
        _master = new GameMaster(preset);
        ui.Init(preset.TabelSize,preset.HandSize);
        OnSessionsGameStarted?.Invoke();
    }

    private void OnEnable()
    {
        ui.OnCardDroped += Drop;
        ui.OnAddCardToHand += Pick;
    }
    private void OnDisable()
    {
        ui.OnCardDroped -= Drop;
        ui.OnAddCardToHand -= Pick;
    }

    private void Start()
    {
        Invoke(nameof(StartGame),.1f);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartGame();
        }
            
    }

    private void StartGame()
    {
        OnStartGame?.Invoke();
        _master.StartGame();
        
        ui.InitCombinationManager(
            _master.GetComboInfo()
                .Select(combo => combo.comboination
                    .Select(convert => convert.Parent.ExportUI()).ToArray()).ToList());
        
        Debug.Log("game started");
        
        UpdateHandVisuals();
        UpdateTabelVisuals();
        UpdateScore();
        UpdateCardCount();
        
    }

    private void Pick(int i)
    {
        CardModel savedMode = _master.Table[i];
        bool success = _master.PickCard(i);
        if (success)
        {
            UpdateHandVisuals();
            UpdateTabelVisuals();
            NextTurn();   
            OnCardPicked?.Invoke(savedMode);
        }
            
    }
    private void Drop(int i)
    {
        CardModel savedMode = _master.Table[i];
        bool success = _master.DropCard(i);
        if (success)
        {
            UpdateTabelVisuals();
            NextTurn();
            OnCardDroped?.Invoke(savedMode);
        }
    }

    private void NextTurn()
    {
        if(!_master.IsTurnFinished)
            return;

        bool containsCombos = _master.LookForCombinations(out int[] cardCombinationIndexes,out List<CardModel> models);
        if (containsCombos)
        {
            OnCombinationCrieted?.Invoke(models.ToArray());
            ShowCardCombo(cardCombinationIndexes);
        }
        UpdateScore();
        UpdateHandVisuals();
        if (_master.NextTurn())
        {
            OnNextTurn?.Invoke();
            UpdateTabelVisuals();
            UpdateCardCount();
        }
        else if(_master.IsGameEnded)
        {
            OnGameEnded?.Invoke(_master.Score);
            ui.ShowEndGameScreen(_master.Score,true);
        }
    }

    private void UpdateHandVisuals()
    {
        CardUIModel[] models = new CardUIModel[_master.HandSize];
        
        for (int i = 0; i < models.Length; i++)
        {
            if (i < _master.Hand.Count)
                models[i] = _master.Hand[i].Parent.ExportUI();
            else
                models[i] = CardUIModel.Empty;
        }
        
        ui.UpdateHandVisuals(models);
    }

    private void UpdateTabelVisuals()
    {
        ui.UpdateTabelVisuals(_master.Table.Select(c => c.Parent.ExportUI()).ToArray());
    }

    private void UpdateScore()
    {
        ui.UpdateScores(_master.Score);
    }

    private void UpdateCardCount()
    {
        ui.UpdateCardCount(_master.CardCount);
    }

    private void ShowCardCombo(int[] intCardIndexes)
    {
        ui.VisualiseCombo(intCardIndexes);
    }
}
