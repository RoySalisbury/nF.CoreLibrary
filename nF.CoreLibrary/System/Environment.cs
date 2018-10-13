//
// Copyright (c) 2017 The nanoFramework project contributors
// Portions Copyright (c) Microsoft Corporation.  All rights reserved.
// See LICENSE file in the project root for full license information.

namespace System
{
    /// <summary>Provides information about the current environment and platform.</summary>
    public static class Environment
    {
        /// <summary>Gets the number of processors on the current machine.</summary>
        /// <returns>The 32-bit signed integer that specifies the number of processors on the current machine. There is no default. If the current machine contains multiple processor groups, this property returns the number of logical processors that are available for use by the common language runtime (CLR).</returns>
        //public static uint ProcessorCount
        //{
        //    get
        //    {
        //        try
        //        {
        //            return NativeProcessorCount();
        //        }
        //        catch
        //        {
        //            // Not implemented? We know we have at least 1.
        //            return 1;
        //        }
        //    }
        //}

        /// <summary>Gets the number of milliseconds elapsed since the system started.</summary>
        /// <returns>A 64-bit signed integer containing the amount of time in milliseconds that has passed since the last time the computer was started. </returns>
        public static long TickCount
        {
            get
            {
                try
                {
                    // To be used accross all targets, we would need a common method that could get the number of ticks since the target started.
                    // The ESP32 gets the number of microseconds since boot. We convert that to ticks for this call.
                     var microseconds = nanoFramework.Hardware.Esp32.HighResTimer.GetCurrent();
                    return (long)microseconds * 10;
                }
                catch
                {
                    // Not implemented? Just return 0.
                    return 0;
                }
            }
        }

        /// <summary>Gets a <see cref="T:System.Version" /> object that describes the major, minor, build, and revision numbers of the common language runtime.</summary>
        /// <returns>An object that displays the version of the common language runtime.</returns>
        //public static Version Version => new Version(1, 0);
    }
}