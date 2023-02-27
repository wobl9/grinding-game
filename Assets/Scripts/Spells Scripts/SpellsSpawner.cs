using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Utils;

public class SpellsSpawner : MonoBehaviour
{
    [SerializeField] List<SpellObject> allSpells;
    [SerializeField] List<SpellObject> testSpells;
    private readonly Dictionary<string, SpellObject> spells = new();
    private readonly Dictionary<string, float> spellsCooldowns = new();


    private void Awake()
    {
        foreach (SpellObject spell in testSpells)
        {
            spells.Add(spell.id, spell);
            spellsCooldowns.Add(spell.id, 0.0f);
        }
    }

    private void Update()
    {
        foreach (string spellId in spells.Keys)
        {
            spellsCooldowns[spellId] -= Time.deltaTime;
            CastSpell(id: spellId, castStrategy: CastStrategy.ZERO_COOLDOWN);
        }
    }

    public void OnLevelUpSpellChosen(string spellId)
    {
        Debug.Log("spell was learned or leveled up");
    }

    public void AddSpell(SpellObject spell)
    {
        spells.Add(spell.id, spell);
        spellsCooldowns.Add(spell.id, 0.0f);
    }
    public void RemoveSpell(string id)
    {
        spells.Remove(id);
        spellsCooldowns.Remove(id);
    }
    //todo вынести куда-то
    public IEnumerable<LevelUpSpellModel> GetLearnedSpellsIds()
    {
        return spells.Values.Select(spell => CreateLevelSpellModel(spell));
    }
    //todo вынести куда-то
    public IEnumerable<LevelUpSpellModel> GetAllAvailableSpells()
    {
        return allSpells.Select(spell => CreateLevelSpellModel(spell));
    }
    //через экстеншн
    private LevelUpSpellModel CreateLevelSpellModel(SpellObject spellObject)
    {
        LevelUpSpellModel model = new();
        model.desctiption = spellObject.description;
        model.image = spellObject.prefab.GetComponent<SpriteRenderer>().sprite;
        model.level = spellObject.level;
        model.id = spellObject.id;
        return model;
    }

    //придумать фолбек, чтобе в бесконечный цикл не уйти
    public List<LevelUpSpellModel> OfferNewSpellsToPlayer()
    {
        var offeredSpells = new List<LevelUpSpellModel>();
        var learnedSpells = GetLearnedSpellsIds().ToList();
        var allAvailableSpells = GetAllAvailableSpells().ToList();
        //todo если попали в 0.7 то скил из списка, если нет то новый. если такой скил уже есть в предложенных то снова новый скилл
        while (offeredSpells.Count < 3)
        {
            if (ChanceUtils.HitChance(0.7f) && (learnedSpells.Count - 1) > 0)
            {
                int learnedSpellsIndex = Random.Range(0, allAvailableSpells.Count - 1);
                offeredSpells.Add(learnedSpells[learnedSpellsIndex]);
                learnedSpells.RemoveAt(learnedSpellsIndex);
            }
            else
            {
                int availableSpellIndex = Random.Range(0, allAvailableSpells.Count - 1);
                offeredSpells.Add(allAvailableSpells[availableSpellIndex]);
                allAvailableSpells.RemoveAt(availableSpellIndex);
            }
        }
        return offeredSpells;
    }

    public void CastSpell(string id, CastStrategy castStrategy)
    {
        if (spellsCooldowns[id] <= 0.0f)
        {
            SpellObject spell = spells[id];
            if (spell.castStrategy == CastStrategy.ANY)
            {
                Instantiate(spell.prefab, transform.position, transform.rotation);
                print("Casting spell: " + spell.id + " for " + spell.damage + " damage!");
                spellsCooldowns[id] = spell.cooldown;
            }
            else if (spell.castStrategy == castStrategy)
            {
                Instantiate(spell.prefab, transform.position, transform.rotation);
                print("Casting spell: " + spell.id + " for " + spell.damage + " damage!");
                spellsCooldowns[id] = spell.cooldown;
            }
            else
            {
                Debug.Log($"for spell {spell.id} startegy is {spell.castStrategy} and cast stategy is {castStrategy}");
            }
        }
    }
}
