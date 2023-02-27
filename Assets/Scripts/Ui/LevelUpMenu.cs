using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.Ui
{
    public class LevelUpMenu : MonoBehaviour
    {

        public event EventHandler<string> OnButtonClicked;

        [SerializeField] private SpellsSpawner spellSpawner;
        [SerializeField] private RectTransform content;
        [SerializeField] private RectTransform spellObjectPrefab;

        public void Init()
        {
            var offeredSpells = spellSpawner.OfferNewSpellsToPlayer();
            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }
            foreach (LevelUpSpellModel model in offeredSpells)
            {
                var instance = Instantiate(spellObjectPrefab, content);
                NewSpellView view = new(instance);
                view.textView.text = model.desctiption;
                view.image.sprite = model.image;
                view.button.onClick.AddListener(() => OnBUttonClicked(model.id));
            }
        }

        private void OnBUttonClicked(string spellId)
        {
            spellSpawner.OnLevelUpSpellChosen(spellId);
            OnButtonClicked?.Invoke(this, spellId);
            Debug.Log($"Button with id {spellId} was clicked");
        }

        private class NewSpellView
        {
            public Button button;
            public TextMeshProUGUI textView;
            public Image image;

            public NewSpellView(Transform rootView)
            {
                button = rootView.GetComponent<Button>();
                textView = rootView.Find("Spell Description").GetComponent<TextMeshProUGUI>();
                image = rootView.Find("Spell Image").GetComponent<Image>();
            }
        }
    }
}


