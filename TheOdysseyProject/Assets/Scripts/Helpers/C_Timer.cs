using UnityEngine;
using System.Collections;

public class C_Timer
{

	#region Variables

	public E_TimerType timerType		= E_TimerType.Stopwatch;

	// minutes and seconds are the updating variables
	// for Stopwatch, they increase on every update
	// for Countdown, they decrease on every update
	[Range(0,59)]
	public float minutes				= 0;
	[Range(0,59)]
	public float seconds				= 2;

	public bool countdownEnd			= false;
	public bool pauseStopwatch			= false;
	
	private float _minutes_resetValue	= 0;
	private float _seconds_resetValue	= 2;

	#endregion

	// Constructor
	// Only need to set minutes and seconds if timerType is Countdown
	public C_Timer(E_TimerType timerType = E_TimerType.Stopwatch, float minutes = 0, float seconds = 0)
	{
		this.timerType = timerType;
		this.minutes = minutes;
		this.seconds = seconds;
		_minutes_resetValue = minutes;
		_seconds_resetValue = seconds;
	}

	// Resets the Timer
	// hardReset is to reinitialise everything
	// !hardReset is to just reset the updating variables ( in this case, the minutes and seconds )
	public void Reset(bool hardReset = false, E_TimerType timerType = E_TimerType.Stopwatch, float minutes = 0, float seconds = 0)
	{
		if (hardReset) {
			this.timerType = timerType;
			this.minutes = minutes;
			this.seconds = seconds;
			_minutes_resetValue = minutes;
			_seconds_resetValue = seconds;
		}
		else
		{
			this.minutes = _minutes_resetValue;
			this.seconds = _seconds_resetValue;
		}
	}
	
	public bool UpdateTimer()
	{
		// if countdown timer
		if (timerType == E_TimerType.Countdown)
		{
			if (seconds <= 0)
			{
				if (minutes > 0)
				{
					minutes--;
					seconds = 60;
				}
				else
				{
					minutes = 0;
					seconds = 0;
					countdownEnd = true;
					return true;
				}
			}
			
			seconds -= Time.deltaTime;

		}
		// if stopwatch timer
		else
		{
			if(!pauseStopwatch)
			{
				if (seconds >= 60)
				{
					minutes++;
					seconds = 0;
				}
				
				seconds += Time.deltaTime;
			}

		}
		return false;
	}

	public float inSeconds()
	{
		return (minutes*60) + seconds;
	}

	override public string ToString()
	{
		return "Minutes: " + minutes + ", Seconds: " + seconds;
	}

}

