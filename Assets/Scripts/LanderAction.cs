/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

using System;

/// <summary>
/// Lunar lander actions. Note the use of enum flags, so we can have several
/// actions simultaneously.
/// </summary>
[Flags]
public enum LanderAction
{
    None      = 0,
    Thrusters = 1,
    Right     = 2,
    Left      = 4
}
