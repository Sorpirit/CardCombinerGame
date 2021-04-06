using System.Linq;
using Core;
using Models;
using UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameMaster _master;
    [SerializeField] private UIController ui;
    [SerializeField] private GamePreset preset;

    private void Awake()
    {
        _master = new GameMaster(preset);
        ui.Init(preset.TabelSize,preset.HandSize);
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartGame();
        }
            
    }

    private void StartGame()
    {
        _master.StartGame();
        
        ui.InitCombinationManager(
            _master.GetComboInfo()
                .Select(combo => combo.comboination
                    .Select(convert => convert.Parent.ExportUI()).ToArray()).ToList());
        
        Debug.Log("game started");
        
        UpdateHandVisuals();
        UpdateTabelVisuals();
        UpdateScore();
        
        
    }

    private void Pick(int i)
    {
        bool success = _master.PickCard(i);
        if (success)
        {
            UpdateHandVisuals();
            UpdateTabelVisuals();
            NextTurn();   
        }
            
    }
    private void Drop(int i)
    {
        bool success = _master.DropCard(i);
        if (success)
        {
            UpdateTabelVisuals();
            NextTurn();
        }
    }

    private void NextTurn()
    {
        if(!_master.isTurnFinished)
            return;

        bool containsCombos = _master.LookForCombinations(out int[] cardCombinationIndexes);
        if (containsCombos)
        {
            ShowCardCombo(cardCombinationIndexes);
        }
        UpdateScore();
        UpdateHandVisuals();
        if (_master.NextTurn())
        {
            UpdateTabelVisuals();
        }
        else if(_master.isGameEnded)
        {
            Debug.Log("Game finished");
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

    private void ShowCardCombo(int[] intCardIndexes)
    {
        ui.VisualiseCombo(intCardIndexes);
    }
}
