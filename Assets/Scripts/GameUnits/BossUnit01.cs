using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveSpeedComponent))]
[RequireComponent(typeof(RotateSpeedComponent))]
[RequireComponent(typeof(Rigidbody))]
public class BossUnit01 : GameUnit {
    
    public struct ScreenBounds
    {
        public float left, right, top, bottom;

        public bool Contains(float leftRight, float upDown)
        {
            return (leftRight > left && leftRight < right)
                        && (upDown > bottom && upDown < top);
        }
    }

    // **********************************
    // 
    //          Variables
    //
    // **********************************

    public GameUnit target;

    private bool isAttackingTarget;
    private float elapsedTime;
    private ScreenBounds screenBounds;
    private Bounds shipBounds;

    private MoveSpeedComponent moveSpeedComponent;
    private RotateSpeedComponent rotateSpeedComponent;

    // **********************************
    // 
    //          Properties
    //
    // **********************************

    public float moveSpeed
    {
        get { return moveSpeedComponent.speed; }
        set { moveSpeedComponent.speed = value; }
    }

    public float rotateSpeed

    {
        get { return rotateSpeedComponent.speed; }
        set { rotateSpeedComponent.speed = value; }
    }

    // **********************************
    // 
    //          Unity Methods
    //
    // **********************************

    private void Awake()
    {
        isAttackingTarget = true;
        shipBounds = GetShipBounds();
        screenBounds = GetScreenBounds();

        moveSpeedComponent = GetComponent<MoveSpeedComponent>();
        rotateSpeedComponent = GetComponent<RotateSpeedComponent>();

        if (moveSpeedComponent == null)
            moveSpeedComponent = gameObject.AddComponent<MoveSpeedComponent>();

        if (rotateSpeedComponent == null)
            rotateSpeedComponent = gameObject.AddComponent<RotateSpeedComponent>();
    }

    private void Start()
    {
        StartCoroutine(ManageBossUnit01());
    }


    // **********************************
    // 
    //      Screen Related Methods
    //
    // **********************************

    //Note: Taken from Invincibility Sphere - Find way to make this reusable.
    /// <summary>Get the approximate ship sizes of by using renderers.</summary>
    /// <returns><see cref="Bounds"/></returns>
    private Bounds GetShipBounds()
    {
        // Set up Invincibility Effect object to encompass this entire unit.
        Renderer[] renderers = transform.root.GetComponentsInChildren<Renderer>();
        Bounds bounds = new Bounds();

        foreach (Renderer x in renderers)
            if (x.CompareTag(transform.root.tag))
                bounds.Encapsulate(x.bounds);

        return bounds;
    }

    // Note: Reused from Projectile. Should find a way make it reusable.
    /// <summary>Get the approximate screen bounds regardless of set resolution.</summary>
    /// <returns><see cref="screenBounds"/>. At least you can fly.</returns>
    private ScreenBounds GetScreenBounds()
    {
        ScreenBounds bounds = new ScreenBounds();

        float dist = (transform.position.y - Camera.main.transform.position.y);

        // Literal World coordinates based on Camera.main position.
        float rightLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        float leftLimitation = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        
        float upLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).z;
        float downLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).z;

        

        bounds.left = leftLimitation;
        bounds.right = rightLimitation;
        bounds.top = upLimitation;
        bounds.bottom = downLimitation;
        
        return bounds;
    }
    
    /// <summary>
    /// Bind given <see cref="Vector3"/> to coordinates that are within ScreenBounds.
    /// Returns the new set of Vector3 coordinates that are bounded by the <see cref="ScreenBounds"/>.
    /// NOTE that the returned values uses the X and Z axis.
    /// </summary>
    /// <param name="position"><see cref="Vector3"/> to bind in its X and Z axes.</param>
    /// <param name="bounds">Check the X and Z bounds of the screen.</param>sa
    /// <returns><see cref="Vector3"/> with the X and Z axis bounded.</returns>
    private Vector3 ClampToScreenBounds(Vector3 position, ScreenBounds bounds)
    {
        // If clamping is required then do it, but return true too.
        if (!bounds.Contains(position.x, position.z))
        {
            position.x = Mathf.Clamp(position.x, bounds.left, bounds.right);
            position.z = Mathf.Clamp(position.z, bounds.bottom, bounds.top);
        }
        
        return position;
    }


    // **********************************
    // 
    //      Movement Related Methods
    //
    // **********************************

    // Note: Reused from ProjectileMissile. Should find a way make it reusable.
    private void RotateToward(GameUnit target)
    {
        RotateToward(target.transform.position);
    }
    
    private void RotateToward(Vector3 position)
    {
        Quaternion direction = Quaternion.LookRotation(position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, rotateSpeed * Time.deltaTime);
    }

    // Note: Reused from Projectile. Should find a way make it reusable.
    public void MoveFoward()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (rigidbody != null)
            rigidbody.velocity = transform.forward * moveSpeed * Time.deltaTime;
        else
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);

    }

    private void Update()
    {
        if (target == null)
            target = GameController.currentPlayerUnit;
    }

    // **********************************
    // 
    //      BossUnit01 Specific Methods
    //
    // **********************************

    private IEnumerator ManageBossUnit01()
    {
        // Step1: Move to top center of screen;
        yield return StartCoroutine(MoveToTopOfScreen());

        // Repeat these steps
        // Step2: Continuously Aim at Player
        StartCoroutine(AimAtTarget(target));

        // Step3: Manage weapons.
        StartCoroutine(MoveLeftRight(target));
    }

    private IEnumerator MoveToTopOfScreen()
    {
        bool isAtTopOfScreen = false;

        // Determine top center of screen and offset.
        float centerx = (screenBounds.left + screenBounds.right) / 2.0f;
        float centerz = screenBounds.top - (shipBounds.extents.z + ((screenBounds.top + screenBounds.bottom)*0.05f));
        Vector3 destination = new Vector3(centerx, 0.0f, centerz);
        

        while (!isAtTopOfScreen)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * moveSpeed);
            RotateToward(destination);

            yield return null;

            if ((transform.position.x == destination.x) && (transform.position.y == destination.y) && (transform.position.z == destination.z))
                isAtTopOfScreen = true;
        }
        
    }

    private IEnumerator AimAtTarget(GameUnit target)
    {
        while (isAttackingTarget)
        {
            if (target != null)
            {
                RotateToward(target);
            }
            else
            {
                target = GameController.currentPlayerUnit;
            }

            yield return null;
        }
    }

    private IEnumerator MoveLeftRight(GameUnit target)
    {
        // The left and right boundaries of how far the boss traverses.
        float percentDecrease = 0.2f;    // Subtract 20% of the range for more centered view
        float screenWidth = screenBounds.right - screenBounds.left;
        screenWidth -= (screenWidth * percentDecrease); 
        screenWidth = Mathf.Abs(screenWidth);
        

        while (isAttackingTarget)
        {
            // Gets this unit to travel left and right across the screen width.
            float pingpong = Mathf.PingPong(Time.time * moveSpeed, screenWidth) - (screenWidth / 2.0f);

            Vector3 moveLeftRight = new Vector3(pingpong,  0.0f, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, moveLeftRight, Time.deltaTime * moveSpeed);
            

            yield return null;
        }
    }

}
