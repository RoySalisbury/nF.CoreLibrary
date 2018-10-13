//
// Copyright (c) 2017 The nanoFramework project contributors
// Portions Copyright (c) Microsoft Corporation.  All rights reserved.
// See LICENSE file in the project root for full license information.

namespace System.Diagnostics
{
    /// <summary>
    /// Provides a set of methods and properties that you can use to accurately measure elapsed time.
    /// </summary>
    public class Stopwatch
    {
        private const long TicksPerMillisecond = 10000;
        private const long TicksPerSecond = TicksPerMillisecond * 1000;

        private long _elapsed;
        private long _startTimeStamp;
        private bool _isRunning;

        static Stopwatch()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Stopwatch" /> class.</summary>
        public Stopwatch()
        {
            Reset();
        }

        /// <summary>Starts, or resumes, measuring elapsed time for an interval.</summary>
        public void Start()
        {
            // Calling start on a running Stopwatch is a no-op.
            if (!_isRunning)
            {
                _startTimeStamp = GetTimestamp();
                _isRunning = true;
            }
        }

        /// <summary>Initializes a new <see cref="T:System.Diagnostics.Stopwatch" /> instance, sets the elapsed time property to zero, and starts measuring elapsed time.</summary>
        /// <returns>A <see cref="T:System.Diagnostics.Stopwatch" /> that has just begun measuring elapsed time.</returns>
        public static Stopwatch StartNew()
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            return s;
        }

        /// <summary>Stops measuring elapsed time for an interval.</summary>
        public void Stop()
        {
            // Calling stop on a stopped Stopwatch is a no-op.
            if (_isRunning)
            {
                long endTimeStamp = GetTimestamp();
                long elapsedThisPeriod = endTimeStamp - _startTimeStamp;
                _elapsed += elapsedThisPeriod;
                _isRunning = false;

                if (_elapsed < 0)
                {
                    // When measuring small time periods the StopWatch.Elapsed* 
                    // properties can return negative values.  This is due to 
                    // bugs in the basic input/output system (BIOS) or the hardware
                    // abstraction layer (HAL) on machines with variable-speed CPUs
                    // (e.g. Intel SpeedStep).

                    _elapsed = 0;
                }
            }
        }

        /// <summary>Stops time interval measurement and resets the elapsed time to zero.</summary>
        public void Reset()
        {
            _elapsed = 0;
            _isRunning = false;
            _startTimeStamp = 0;
        }

        /// <summary>Stops time interval measurement, resets the elapsed time to zero, and starts measuring elapsed time.</summary>
        public void Restart()
        {
            _elapsed = 0;
            _startTimeStamp = GetTimestamp();
            _isRunning = true;
        }

        /// <summary>Gets a value indicating whether the <see cref="T:System.Diagnostics.Stopwatch" /> timer is running.</summary>
        /// <returns>
        ///     <see langword="true" /> if the <see cref="T:System.Diagnostics.Stopwatch" /> instance is currently running and measuring elapsed time for an interval; otherwise, <see langword="false" />.</returns>
        public bool IsRunning
        {
            get { return _isRunning; }
        }

        /// <summary>Gets the total elapsed time measured by the current instance.</summary>
        /// <returns>A read-only <see cref="T:System.TimeSpan" /> representing the total elapsed time measured by the current instance.</returns>
        public TimeSpan Elapsed
        {
            get { return new TimeSpan(GetRawElapsedTicks()); }
        }

        /// <summary>Gets the total elapsed time measured by the current instance, in milliseconds.</summary>
        /// <returns>A read-only long integer representing the total number of milliseconds measured by the current instance.</returns>
        public long ElapsedMilliseconds
        {
            get { return GetRawElapsedTicks() / TicksPerMillisecond; }
        }

        /// <summary>Gets the total elapsed time measured by the current instance, in timer ticks.</summary>
        /// <returns>A read-only long integer representing the total number of timer ticks measured by the current instance.</returns>
        public long ElapsedTicks
        {
            get { return GetRawElapsedTicks(); }
        }

        /// <summary>Gets the current number of ticks in the timer mechanism.</summary>
        /// <returns>A long integer representing the tick counter value of the underlying timer mechanism.</returns>
        public static long GetTimestamp()
        {
            return System.Environment.TickCount;
        }

        // Get the elapsed ticks.        
        private long GetRawElapsedTicks()
        {
            long timeElapsed = _elapsed;

            if (_isRunning)
            {
                // If the StopWatch is running, add elapsed time since
                // the Stopwatch is started last time. 
                long currentTimeStamp = GetTimestamp();
                long elapsedUntilNow = currentTimeStamp - _startTimeStamp;
                timeElapsed += elapsedUntilNow;
            }
            return timeElapsed;
        }
    }
}