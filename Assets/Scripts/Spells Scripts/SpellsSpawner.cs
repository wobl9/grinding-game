using System.Collections.Generic;
using UnityEngine;

public class SpellsSpawner : MonoBehaviour
{
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
