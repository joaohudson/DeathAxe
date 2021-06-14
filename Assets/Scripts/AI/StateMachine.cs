using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    /// <summary>
    /// Chamado ao entrar neste estado.
    /// </summary>
    void OnEnter();
    /// <summary>
    /// Chamado a cada quadro.
    /// </summary>
    void OnUpdate();
    /// <summary>
    /// Chamado ao sair deste estado.
    /// </summary>
    void OnExit();
}

public abstract class StateMachine : MonoBehaviour
{
    private IState currentState = null;

    private void Update()
    {
        currentState?.OnUpdate();
        OnUpdate();
    }

    /// <summary>
    /// Chamado a cada frame.
    /// </summary>
    protected abstract void OnUpdate();

    /// <summary>
    /// O estado atual. Pode ser definido com um novo
    /// estado para trocar de comportamento.
    /// </summary>
    protected IState CurrentState {
        get => currentState;
        set
        {
            if (currentState == value)
                return;

            currentState?.OnExit();
            currentState = value;
            currentState?.OnEnter();
        }
    }
}
