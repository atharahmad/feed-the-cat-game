using System;
using UnityEngine;
using UnityEngine.UI;

public class SuperPowersHandler : MonoBehaviour
{
    public static Action PawsClicked;
    public static Action ElixirClicked;
    public static Action BiscuitsClicked;
    public static Action ExtraLivesClicked;

    [SerializeField] private Button pawsButton;
    [SerializeField] private Button elixirButton;
    [SerializeField] private Button extraLivesButton;
    [SerializeField] private Button biscuitsButton;

    private void OnEnable()
    {
        pawsButton.onClick.AddListener(UsePaws);
        elixirButton.onClick.AddListener(UseElixir);
        biscuitsButton.onClick.AddListener(UseBiscuits);
        extraLivesButton.onClick.AddListener(UseExtraLives);
    }

    private void OnDisable()
    {
        pawsButton.onClick.RemoveListener(UsePaws);
        elixirButton.onClick.RemoveListener(UseElixir);
        biscuitsButton.onClick.RemoveListener(UseBiscuits);
        extraLivesButton.onClick.RemoveListener(UseExtraLives);
    }

    private void UsePaws()
    {
        PawsClicked?.Invoke();
    }

    private void UseElixir()
    {
        ElixirClicked?.Invoke();
    }

    private void UseBiscuits()
    {
        BiscuitsClicked?.Invoke();
    }

    private void UseExtraLives()
    {
        ExtraLivesClicked?.Invoke();
    }
}
