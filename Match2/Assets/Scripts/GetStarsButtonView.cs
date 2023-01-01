using TMPro;

using Helpers.Config;

using UnityEngine;

public class GetStarsButtonView : MonoBehaviour
{
    [SerializeField] private TMP_Text _buttonText;

    [SerializeField] private HelpersConfig _config;

    private void Start()
    {
        _buttonText.text = $"= {_config.GetHelpersPrice()}";
    }
}
