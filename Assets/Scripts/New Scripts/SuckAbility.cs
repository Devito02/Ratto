using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;

public class SuckAbility : CharacterAbility
{
    /// This method is only used to display a helpbox text
    /// at the beginning of the ability's inspector
    public override string HelpBoxText() { return "TODO_HELPBOX_TEXT"; }

    [Header("TODO_HEADER")]
    /// declare your parameters here
    public float MaxProjectiles;
    public Seed_Holder seed_Holder;
    protected Collider[] _targetsWithinDistance;
    public LayerMask TargetMask;
    public float maxDistance;
    public Transform _RotatingTransfrom;


    [Tooltip("wheter is snapping on proj or sucking continuosly while pressing the input")]
    public bool ContinuosSucking;

    public virtual Vector3 Center { get { return this.transform.position; } }

    protected const string _yourAbilityAnimationParameterName = "YourAnimationParameterName";
    protected int _yourAbilityAnimationParameter;
    protected Color _gizmoColor = Color.yellow;
    protected bool _init = false;
    protected Vector3 _raycastOrigin;
    public float Sucking_Angle;

    [Tooltip("Single Sucking feedback")]
    public MMFeedbacks SingleSuck_Feedback;
    /// the feedback to play when the jump stops
    [Tooltip("Continous Sucking feedback")]
    public MMFeedbacks ContinousSuck_Feedback;


    /// <summary>
    /// Here you should initialize our parameters
    /// </summary>
    protected override void Initialization()
    {
        base.Initialization();
        seed_Holder = GetComponent<Seed_Holder>();
        _gizmoColor.a = 0.25f;
        SingleSuck_Feedback?.Initialization(this.gameObject);
        ContinousSuck_Feedback?.Initialization(this.gameObject);

        _init = true;
    }

    /// <summary>
    /// Every frame, we check if we're crouched and if we still should be
    /// </summary>
    public override void ProcessAbility()
    {
        base.ProcessAbility();
    }

    /// <summary>
    /// Called at the start of the ability's cycle, this is where you'll check for input
    /// </summary>
    protected override void HandleInput()
    {
        // here as an example we check if we're pressing down
        // on our main stick/direction pad/keyboard
        if (_inputManager.SuckButton.State.CurrentState == MMInput.ButtonStates.ButtonDown && !ContinuosSucking)
        {
            SingleSuck_Feedback?.PlayFeedbacks(this.transform.position);
            Suck();
        }
        else if (_inputManager.SuckButton.State.CurrentState == MMInput.ButtonStates.ButtonPressed && ContinuosSucking)

        {
            ContinousSuck_Feedback?.PlayFeedbacks(this.transform.position);
            Suck();
        }
    }

    /// <summary>
    /// If we're pressing down, we check for a few conditions to see if we can perform our action
    /// </summary>
    protected virtual void Suck()
    {
        

        // if the ability is not permitted
        if (!AbilityPermitted
            // or if we're not in our normal stance
            || (_condition.CurrentState != CharacterStates.CharacterConditions.Normal)
            // or if we're grounded
            || (!_controller.Grounded))
        {
            // we do nothing and exit
            return;
        }
        Debug.Log("Suckign");

        PlayAbilityStartSfx();
        PlayAbilityUsedSfx();
        PlayAbilityStartFeedbacks();

        //Else we do the ability
        _targetsWithinDistance = Physics.OverlapSphere(Center, maxDistance, TargetMask,QueryTriggerInteraction.Collide);
        foreach (Collider collider in _targetsWithinDistance)
        {
            Debug.Log(collider.gameObject.name);    
            Projectile _proj = collider.GetComponent<Projectile>();
            if (_proj != null)
            {
                Vector3 toProjectile = _proj.transform.position - transform.position;
                toProjectile.y = 0; // Ignore height difference
                toProjectile.Normalize();

                // Get forward direction (normalized)
                Vector3 characterForward = new Vector3(_RotatingTransfrom.forward.x, 0, _RotatingTransfrom.forward.z).normalized;

                // Calculate angle between vectors
                float angle = Vector3.Angle(characterForward, toProjectile);

                // Check if projectile is within sucking angle
                if (angle <= Sucking_Angle / 2)
                {
                    //if the projectile was sent by the same character who is trying to suck it reject it
                    if (_proj.GetOwner() == _character.gameObject) return;
                    Seed _seed = _proj.GetComponent<Seed>();
                    if (!_seed) return;

                    seed_Holder.Ingest(_seed);
                    _proj.Destroy();
                }

                
            }
        }
    }

    protected virtual void OnDrawGizmosSelected()
    {
        _raycastOrigin = Center;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_raycastOrigin, maxDistance);
        if (_init)
        {
            Vector3 _minangle = Quaternion.AngleAxis(-Sucking_Angle/2, Vector3.up) * _RotatingTransfrom.forward.normalized;
            Vector3 _maxangle = Quaternion.AngleAxis(Sucking_Angle / 2, Vector3.up) * _RotatingTransfrom.forward.normalized;

            Gizmos.DrawLine(Center, maxDistance * _minangle + Center);
            Gizmos.DrawLine(Center, maxDistance * _maxangle + Center);

            Gizmos.color = _gizmoColor;
            Gizmos.DrawSphere(_raycastOrigin, maxDistance);
        }
    }

}
