using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Jimmy He
 * CSC631
 * Team12
 * Final Project
 */

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(IdentifierComponent))]
public class DamageAOEComponent : MonoBehaviour {

    // *****************
    // 
    //  Variables
    //
    // *****************
    
    [Tooltip("Affect all units within range regardless of friend or foe.")]
    public bool  isNeutral = false;
    [Tooltip("The damage will apply only once upon activation and cease.\n")]
    public bool  applyAtStartOnly = false;
    [Tooltip("Persistent damage applied to unit when they stay within AOE.")]
    public int   damage = 50;
    [Tooltip("Damage will be applied in this time increment in seconds.\n"
                + "NOTE that smaller increments means more damage dealt over one second unless damage is portioned properly.")]
    public float rateOfDamage = 0.2f;
    [Tooltip("Indicates if damage should decrease as a unit is further away from the AOE's center.\n "
                + "The damage formula is as follows: (damage - (damage/AOERadius)(unitPosition - AOECenterPositon)).")]
    public bool  useDamageRadiusFalloff = false;
    

    /*** Private Variables ***/
    private IdentifierComponent idComponent;    // For owner identification in friend or foe checking.
    private SphereCollider areaOfEffect;        // The AOE range to check for units.
    private bool applyAtStartOnlyCheck;         // Check if damage is to be applied only once at activation and cease afterward.
    private float nextDamageTime;               // Timer to apply damage to AOE units again.

    private List<GameUnit> unitsInAOEList;      // Units located in the AOE.

    /*** Properties ***/
    public string ownerTag
    {
        get
        {
            if (idComponent == null)
                idComponent = GetComponent<IdentifierComponent>();

            return idComponent.ownerTag;
        }

        set
        {
            if (idComponent == null)
                idComponent = GetComponent<IdentifierComponent>();

            idComponent.ownerTag = value;
        }
    }

    // *****************
    // 
    //  Unity Methods
    //
    // *****************

    private void Start()
    {
        idComponent = GetComponent<IdentifierComponent>();
        areaOfEffect = GetComponent<SphereCollider>();
        nextDamageTime = 0.0f;

        unitsInAOEList = new List<GameUnit>();

        // Apply damage to units within range once and cease any damages afterward.
        if (applyAtStartOnly) ApplyDamageAtStartOnly();
    }

    
    private void OnTriggerStay(Collider other)
    {
        if ((Time.time > nextDamageTime) && !applyAtStartOnly)
            AddGameUnitToList(other);
    }
    
    // NOTE: Unity Execution Order runs OnTrigger() functions first before Update().
    // NOTE: Every Collider in OnTrigger will process that code, thus damage application is waited until Update().

    private void Update()
    {
        if ((Time.time > nextDamageTime) && !applyAtStartOnly)
        {
            ProcessUnitsInAOEList();
            nextDamageTime = Time.time + rateOfDamage;  // Set next time of applied damage.
        }
    }

    
    // *****************
    // 
    //  Private Methods
    //
    // *****************
    
    /// <summary>
    /// Method to only be executed once at the <see cref="Start"/> if <see cref="applyAtStartOnly"/> is true.
    /// This method creates an <see cref="Physics.OverlapSphere(Vector3, float)"/> to detect initial units
    /// within the AOE upon instantiation.
    /// </summary>
    private void ApplyDamageAtStartOnly()
    {
        Collider[] collidedUnits = Physics.OverlapSphere(transform.position, areaOfEffect.radius);

        foreach (Collider x in collidedUnits)
            AddGameUnitToList(x);

        ProcessUnitsInAOEList();
    }

    /// <summary>
    /// Helper method to adds the given units to <see cref="unitsInAOEList"/>.
    /// It will reject null <see cref="GameUnit"/> and pre-existing units that have already been added.
    /// </summary>
    /// <param name="other">The <see cref="Collider"/> of the collided object.</param>
    private void AddGameUnitToList(Collider other)
    {
        GameUnit gameUnit = other.transform.root.GetComponentInChildren<GameUnit>();

        if (gameUnit != null)
            if (!unitsInAOEList.Contains(gameUnit))
                unitsInAOEList.Add(gameUnit);   // Add GameUnits only to AOE affected list.
    }

    /// <summary>
    /// Process and call <see cref="ApplyDamage(GameUnit, int)"/> for units in the <see cref="unitsInAOEList"/>.
    /// </summary>
    private void ProcessUnitsInAOEList()
    {
        foreach (GameUnit x in unitsInAOEList)
        {
            if (isNeutral)
                ApplyDamage(x, damage);
            else if (Tags.isEnemy(ownerTag, x.tag))
                ApplyDamage(x, damage);
        }

        unitsInAOEList.Clear();     // Clear out list for next AOE scan
    }

    /// <summary>
    /// Helper method for <see cref="AddAffectedUnitsToList(Collider, int)"/>.
    /// Determines the amount of damage to apply to give <see cref="GameUnit"/>; if <see cref="useDamageRadiusFalloff"/>
    /// is being used then the damage will vary based on the unit's distance from the epicenter. If not, then the
    /// damage will be flat damage.
    /// </summary>
    /// <param name="unit">Unit to apply damage to.</param>
    /// <param name="damage">The amount of damage to deal to unit.</param>
    private void ApplyDamage(GameUnit unit, int damage)
    {
        int damageToApply = damage;

        if (useDamageRadiusFalloff) // Decrease damage applied based on distance.
        {
            // aoeRadius = this explosion SphereCollider's radius.
            float aoeRadius = areaOfEffect.radius;

            // Distance between the target and the explosion's center.
            float unitToCenterDistance = Vector3.Distance(  unit.transform.position, 
                                                            transform.TransformPoint(areaOfEffect.center));

            // Unknown reason but sometimes the distance is larger than the radius while target is within and near its edge.
            unitToCenterDistance = Mathf.Clamp(unitToCenterDistance, 0.0f, aoeRadius);

            // Formula: (damage - (damage/AOERadius)(unitPosition - AOECenterPositon))
            float damageToDeal = damage - ((damage / aoeRadius) * (unitToCenterDistance));
            
            damageToApply = Mathf.CeilToInt(damageToDeal);
            
        }

        unit.TakeDamage(damageToApply);
    }
    

}
