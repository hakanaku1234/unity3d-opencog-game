
/// Unity3D OpenCog World Embodiment Program
/// Copyright (C) 2013  Novamente			
///
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU Affero General Public License as
/// published by the Free Software Foundation, either version 3 of the
/// License, or (at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU Affero General Public License for more details.
///
/// You should have received a copy of the GNU Affero General Public License
/// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections;
using OpenCog.Attributes;
using OpenCog.Extensions;
using ProtoBuf;
using UnityEngine;
using OpenCog.Serialization;

namespace OpenCog.Utility
{

/// <summary>
/// The OpenCog Singleton.  Any class which inherits from this
/// </summary>
#region Class Attributes

#endregion
public class OCSingleton<T, B> : B
	where T : B // where this singleton's type T derives from base class B
{

	//---------------------------------------------------------------------------

	#region Private Member Data

	//---------------------------------------------------------------------------
		
	/// <summary>
	/// The singleton instance.
	/// </summary>
	protected static T m_Instance = null;
		
	//---------------------------------------------------------------------------

	#endregion

	//---------------------------------------------------------------------------

	#region Accessors and Mutators

	//---------------------------------------------------------------------------
		
	/// <summary>
	/// Gets the singleton instance.
	/// </summary>
	/// <value>
	/// The instance of this singleton.
	/// </value>
	public static T Instance
	{
		get
		{
			if(m_Instance == null && !Instantiate())
			{
				Debug.LogError
				( "In OCSingleton.Instance, an instance of singleton " 
				+ typeof(T) 
				+ " does not exist and could not be instantiated."
				);
			}
				
			return m_Instance;
		}
	}
			
	//---------------------------------------------------------------------------

	#endregion

	//---------------------------------------------------------------------------

	#region Public Member Functions

	//---------------------------------------------------------------------------

	//---------------------------------------------------------------------------

	#endregion

	//---------------------------------------------------------------------------

	#region Private Member Functions

	//---------------------------------------------------------------------------
			
	/// <summary>
	/// Instantiate this singleton instance.
	/// </summary>
	private static bool Instantiate()
	{
		//Assert that we're not already instantiated
		if(m_Instance != null)
		{
			throw new 
				OCException("In OCSingleton.Instantiate, we're already instantiated!");
		}
			
		//Switch based on the type of B, the base class
		OCTypeSwitch.Do<B>
		( 
			OCTypeSwitch.Case<Component>
			(
				() => 
				{
					GameObject gameObject = 
						new GameObject(typeof(T).ToString(), typeof(T));
					m_Instance = gameObject.GetComponent<T>();
				}
			)
		, OCTypeSwitch.Case<ScriptableObject>
			(
				() => ScriptableObject.CreateInstance<T>()
			)
		, OCTypeSwitch.Case<Object>
			(
				() => m_Instance = (T)FindObjectOfType(typeof(T))
			)
		, OCTypeSwitch.Default
			(
				() => {}
			)
		);
			
		return m_Instance != null;
	}
					
					
	//---------------------------------------------------------------------------

	#endregion

	//---------------------------------------------------------------------------

	#region Member Classes

	//---------------------------------------------------------------------------		

	//---------------------------------------------------------------------------

	#endregion

	//---------------------------------------------------------------------------

}// class OCSingleton

}// namespace OpenCog.Utility




