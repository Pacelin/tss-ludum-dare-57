using System;
using System.Linq;
using AYellowpaper.SerializedCollections;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace LudumDare57.UI
{
    public class LanguageSelectionView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _language;
        [SerializeField] private FeedbackButton _nextLocaleButton;
        [SerializeField] private FeedbackButton _previousLocaleButton;
        [SerializeField] private SerializedDictionary<Locale, string> _languageNames;

        private Locale[] _values;
        private int _selectedIndex;
        private IDisposable _disposables;
        
        private void Awake()
        {
            _values = _languageNames.Keys.ToArray();
            _selectedIndex = PlayerPrefs.GetInt("selected_language", 0);
            LocalizationSettings.Instance.SetSelectedLocale(_values[_selectedIndex]);
            _language.text = _languageNames[_values[_selectedIndex]];
        }

        private void OnEnable()
        {
            var nextDisposable = 
                _nextLocaleButton.ObserveFeedbackStart().Subscribe(_ => OnNextLocaleClick());
            var previousDisposable =
                _previousLocaleButton.ObserveFeedbackStart().Subscribe(_ => OnPreviousLocaleClick());
            _disposables = Disposable.Combine(nextDisposable, previousDisposable);
        }

        private void OnDisable()
        {
            _disposables.Dispose();
        }

        private void OnNextLocaleClick()
        {
            _selectedIndex = (_selectedIndex + 1) % _values.Length;
            PlayerPrefs.SetInt("selected_language", _selectedIndex);
            LocalizationSettings.Instance.SetSelectedLocale(_values[_selectedIndex]);
            _language.text = _languageNames[_values[_selectedIndex]];
        }

        private void OnPreviousLocaleClick()
        {
            _selectedIndex = (_selectedIndex + _values.Length - 1) % _values.Length;
            PlayerPrefs.SetInt("selected_language", _selectedIndex);
            LocalizationSettings.Instance.SetSelectedLocale(_values[_selectedIndex]);
            _language.text = _languageNames[_values[_selectedIndex]];
        }
    }
}