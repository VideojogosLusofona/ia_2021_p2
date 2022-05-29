/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

using UnityEngine;

public class ControlLander : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Sprite to use when lander is NOT using thrusters")]
    private Sprite landerNoThrusters;

    [SerializeField]
    [Tooltip("Sprite to use when lander is using thrusters")]
    private Sprite landerWithThrusters;

    [SerializeField]
    [Tooltip("Thrust/force to apply when pressing the up arrow")]
    private float thrust = 5.0f;

    [SerializeField]
    [Tooltip("Rotation (in degrees) to apply when pressing the right or left arrows")]
    private float rotationDelta = 2f;

    // Reference to the rigid body
    private Rigidbody2D rb;

    // Reference to the sprite renderer
    private SpriteRenderer sr;

    // The last action specified by the player
    private LanderAction action;

    // Last time thrusters were activated (used to avoid sprite flickering)
    private float lastThrustTime;

    // We use this to keep the thruster flame on while the player is pressing up
    // (to avoid sprite flickering)
    private const float THRUST_TIMEOUT = 0.01f;

    // Start is called before the first frame update
    private void Start()
    {
        // Get references to the necessary components
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        action = LanderAction.None;
    }

    // Update is called once per frame
    private void Update()
    {
        // Assume there's no action by default
        action = LanderAction.None;

        // Check if the user pressed any valid key
        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                action = LanderAction.Thrusters;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                action = LanderAction.Right;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                action = LanderAction.Left;
            }
        }
    }

    // Frame-rate independent Update for physics calculations
    private void FixedUpdate()
    {
        // Was any action specified by the player during the last frame update?
        switch (action)
        {
            case LanderAction.Thrusters:
                // Apply thrust
                rb.AddForce(thrust * Deg2Vec(rb.rotation + 90));
                // Remember last time thrust was applied
                lastThrustTime = Time.fixedTime;
                break;
            case LanderAction.Right:
                // Rotate right
                rb.SetRotation(rb.rotation - rotationDelta);
                break;
            case LanderAction.Left:
                // Rotate left
                rb.SetRotation(rb.rotation + rotationDelta);
                break;
        }

        // Determine if it's necessary to swap sprites
        if (action == LanderAction.Thrusters && sr.sprite == landerNoThrusters)
        {
            sr.sprite = landerWithThrusters;
        }
        else if (sr.sprite == landerWithThrusters
            && Time.fixedTime > lastThrustTime + THRUST_TIMEOUT)
        {
            sr.sprite = landerNoThrusters;
        }
    }

    // Convert an angle in degrees to a normalized Vector2
    // See lecture 2 (about movement) to understand why
    private static Vector2 Deg2Vec(float angle)
    {
        float angleRad = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}
