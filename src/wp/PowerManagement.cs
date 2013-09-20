/*
 * Copyright (C) 2013 Wolfgang Koller
 * 
 * This file is part of GOFG Sports Computer - http://www.gofg.at/.
 * 
 * GOFG Sports Computer is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * GOFG Sports Computer is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with GOFG Sports Computer.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Phone.Shell;
using WPCordovaClassLib.Cordova;
using WPCordovaClassLib.Cordova.Commands;
using WPCordovaClassLib.Cordova.JSON;

namespace Cordova.Extension.Commands
{
    class PowerManagement : BaseCommand
    {
        /// <summary>
        /// acquires the stay awake lock by disabling UserIdleDetection mode and optionally disable the ApplicationIdleDetection
        /// </summary>
        /// <param name="options">acquire options</param>
        /// exec(win, fail, 'PowerManagement', 'acquire', [runLockScreen]);
        public void acquire(string options) {
            string[] optionStrings = JsonHelper.Deserialize<string[]>(options);
            bool runLockScreen = false;
            bool.TryParse(optionStrings[0], out runLockScreen);

            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;

            // check if we should continue running within the lock screen
            if (runLockScreen)
            {
                PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;
            }
            
            DispatchCommandResult();
        }

        /// <summary>
        /// releases the stay awake lock
        /// </summary>
        /// <param name="options">release options</param>
        /// exec(win, fail, 'PowerManagement', 'release', []);
        public void release(string options)
        {
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Enabled;
            
            DispatchCommandResult();
        }

    }
}
