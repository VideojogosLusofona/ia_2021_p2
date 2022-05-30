/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

/// <summary>
/// Extension methods for the LanderAction enum.
/// </summary>
public static class LanderActionExtensions
{
    /// <summary>
    /// Does the current action include a "thrusters" action?
    /// </summary>
    /// <param name="action">The lunar lander action.</param>
    /// <returns>
    /// `true` if the action contains a "thrusters" action, `false` otherwise.
    /// </returns>
    public static bool HasThrusters(this LanderAction action) =>
        (action & LanderAction.Thrusters) == LanderAction.Thrusters;

    /// <summary>
    /// Does the current action include a "rotate right" action?
    /// </summary>
    /// <param name="action">The lunar lander action.</param>
    /// <returns>
    /// `true` if the action contains a "rotate right" action, `false` otherwise.
    /// </returns>
    public static bool HasRight(this LanderAction action) =>
        (action & LanderAction.Right) == LanderAction.Right;

    /// <summary>
    /// Does the current action include a "rotate left" action?
    /// </summary>
    /// <param name="action">The lunar lander action.</param>
    /// <returns>
    /// `true` if the action contains a "rotate left" action, `false` otherwise.
    /// </returns>
    public static bool HasLeft(this LanderAction action) =>
        (action & LanderAction.Left) == LanderAction.Left;
}
