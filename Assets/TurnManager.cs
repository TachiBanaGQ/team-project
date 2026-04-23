using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    // ── Singleton ────────────────────────────────────────────
    public static TurnManager Instance { get; private set; }

    // ── Turn States ──────────────────────────────────────────
    public enum TurnState { PlayerTurn, EnemyTurn, GameOver }

    // ── Settings ─────────────────────────────────────────────
    [Header("Settings")]
    [SerializeField] private float m_enemyTurnDelay = 1.0f;

    // ── Events ────────────────────────────────────────────────
    [Header("Events")]
    public UnityEvent<TurnState> OnTurnChanged;

    // ── State ─────────────────────────────────────────────────
    public TurnState CurrentTurn { get; private set; }

    // ── Unity Lifecycle ───────────────────────────────────────
    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        TransitionTo(TurnState.PlayerTurn);
    }

    // ── Public API ────────────────────────────────────────────

    // Call this when the player finishes their action
    public void PlayerEndTurn()
    {
        if (CurrentTurn != TurnState.PlayerTurn) return;
        TransitionTo(TurnState.EnemyTurn);
    }

    // Call this to end the game from anywhere
    public void EndGame()
    {
        TransitionTo(TurnState.GameOver);
    }

    // ── Turn Logic (fill these in) ────────────────────────────

    private void OnPlayerTurn()
    {
        Debug.Log("Player turn started.");
        // TODO: enable player input, highlight selectable units, etc.
    }

    private void OnEnemyTurn()
    {
        Debug.Log("Enemy turn started.");
        // TODO: run your enemy AI here
        // When enemy is done, call PlayerEndTurn equivalent:
        Invoke(nameof(EnemyEndTurn), m_enemyTurnDelay);
    }

    private void EnemyEndTurn()
    {
        if (CurrentTurn != TurnState.EnemyTurn) return;
        TransitionTo(TurnState.PlayerTurn);
    }

    private void OnGameOver()
    {
        Debug.Log("Game over.");
        // TODO: show game over screen, stop input, etc.
    }

    // ── State Machine ─────────────────────────────────────────
    private void TransitionTo(TurnState next)
    {
        CurrentTurn = next;
        OnTurnChanged?.Invoke(next);

        switch (next)
        {
            case TurnState.PlayerTurn: OnPlayerTurn(); break;
            case TurnState.EnemyTurn:  OnEnemyTurn();  break;
            case TurnState.GameOver:   OnGameOver();   break;
        }
    }
}
