//////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2004, Andrew S. Townley
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions
// are met:
// 
//     * Redistributions of source code must retain the above
//     copyright notice, this list of conditions and the following
//     disclaimer.
// 
//     * Redistributions in binary form must reproduce the above
//     copyright notice, this list of conditions and the following
//     disclaimer in the documentation and/or other materials provided
//     with the distribution.
// 
//     * Neither the names Andrew Townley and Townley Enterprises,
//     Inc. nor the names of its contributors may be used to endorse
//     or promote products derived from this software without specific
//     prior written permission.  
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
// FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
// COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED
// OF THE POSSIBILITY OF SUCH DAMAGE.
//
// File:	AppConfig.cs
// Created:	Mon Jun 14 08:45:41 IST 2004
//
//////////////////////////////////////////////////////////////////////

using System;
using System.Collections;

namespace TownleyEnterprises.Config {

//////////////////////////////////////////////////////////////////////
/// <summary>
///   This class is used to group all of the settings for a given
///   application into a common place.
/// </summary>
/// <version>$Id: AppConfig.cs,v 1.1 2004/06/21 15:54:34 atownley Exp $</version>
/// <author><a href="mailto:adz1092@netscape.net">Andrew S. Townley</a></author>
//////////////////////////////////////////////////////////////////////

public sealed class AppConfig: IConfigSupplier
{
	//////////////////////////////////////////////////////////////
	/// <summary>
	///   The constructor takes the name of the application
	///   managed by these settings.
	/// </summary>
	/// <param name="name">the application name</param>
	//////////////////////////////////////////////////////////////
	
	public AppConfig(string name)
	{
		_name = name;
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This method is used to register a configuration supplier
	///   for this application.
	/// </summary>
	//////////////////////////////////////////////////////////////

	public void RegisterConfigSupplier(IConfigSupplier supplier)
	{
		if(_name != supplier.AppName)
		{
			// don't do anything if the names don't match
			return;
		}

		// NOTE:  there's two different ways to approach this.
		// The first is to just replace the old value with the
		// new one, but then you lose any traceability in your
		// overrides.  The mechanism below is designed to
		// support this tracking, but as I'm writing this, I'm
		// not 100% sure it is necessary... (ast 21-June-04)
		
		foreach(string key in supplier.Keys)
		{
			ConfigResolver cr = new ConfigResolver(supplier);
			ConfigResolver val = (ConfigResolver)_hash[key];
			if(val != null)
			{
				val.Parent = cr;
			}
			else
			{
				_hash[key] = cr;
			}
		}

		_suppliers.Add(supplier);
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This method is used to register a configuration supplier
	///   for this application.
	/// </summary>
	//////////////////////////////////////////////////////////////

	public void UnregisterConfigSupplier(IConfigSupplier supplier)
	{
		// FIXME:  figure out how to implement this cleanly
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This property provides the name used to access the
	///   settings.  Normally, it is set when the instance is
	///   created.
	/// </summary>
	//////////////////////////////////////////////////////////////

	public string AppName
	{
		get { return _name; }
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This property returns the collection of keys in the
	///   supplier.
	/// </summary>
	//////////////////////////////////////////////////////////////

	public ICollection Keys
	{
		get { return _hash.Keys; }
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This indexer provides access to a configuration
	///   setting.  If the setting does not exist, a null is
	///   returned.
	/// </summary>
	//////////////////////////////////////////////////////////////

	public string this[string key]
	{
		get
		{
			ConfigResolver cr = (ConfigResolver)_hash[key];
			if(cr != null)
				return cr[key];
			
			return null;
		}

		set
		{
			ConfigResolver cr = (ConfigResolver)_hash[key];
			if(cr != null)
				cr[key] = value;
		}
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This method will cause the properties to be reloaded
	///   from their original source.
	/// </summary>
	//////////////////////////////////////////////////////////////
	
	public void Load()
	{
		foreach(IConfigSupplier supplier in _suppliers)
		{
			supplier.Load();
		}
	}
	
	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This method will cause the properties to be saved
	///   to their original source.
	/// </summary>
	//////////////////////////////////////////////////////////////
	
	public void Save()
	{
		foreach(IConfigSupplier supplier in _suppliers)
		{
			supplier.Save();
		}
	}

	private Hashtable	_hash = new Hashtable();
	private ArrayList	_suppliers = new ArrayList();
	private string		_name;
}

}
