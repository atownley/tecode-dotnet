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
// File:	ConfigResolver.cs
// Created:	Mon Jun 21 15:08:33 IST 2004
//
//////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Text;

namespace TownleyEnterprises.Config {

//////////////////////////////////////////////////////////////////////
/// <summary>
///   This class is responsible for managing parent-child override
///   relationships between the registered config supplier.  It is a
///   decorator for a single reference to an IConfigSupplier instance
///   delegate.
/// </summary>
/// <version>$Id: ConfigResolver.cs,v 1.1 2004/06/21 15:55:46 atownley Exp $</version>
/// <author><a href="mailto:adz1092@netscape.net">Andrew S. Townley</a></author>
//////////////////////////////////////////////////////////////////////

public sealed class ConfigResolver: IConfigSupplier
{
	//////////////////////////////////////////////////////////////
	/// <summary>
	///   The constructor takes a reference to config supplier
	///   instance to decorate.
	/// </summary>
	/// <param name="config">the supplier with the actual
	/// data</param>
	//////////////////////////////////////////////////////////////

	public ConfigResolver(IConfigSupplier config)
	{
		_config = config;
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   Property which allows manipulating the parent resolver.
	/// </summary>
	/// <param name="parent">the parent resovler</param>
	//////////////////////////////////////////////////////////////

	public ConfigResolver Parent
	{
		get { return _parent; }
		set { _parent = value; }
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
		get { return _config.AppName; }
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This property returns the collection of keys in the
	///   supplier.
	/// </summary>
	//////////////////////////////////////////////////////////////

	public ICollection Keys
	{
		get { return _config.Keys; }
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
			if(_parent != null)
			{
//Console.WriteLine("settings in {0} overridden", _config.GetHashCode());
				return _parent[key];
			}

//Console.WriteLine("return value from {0}", _config.GetHashCode());
			return _config[key];
		}

		set { _config[key] = value; }
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This method will cause the properties to be reloaded
	///   from their original source.
	/// </summary>
	//////////////////////////////////////////////////////////////
	
	public void Load()
	{
		_config.Load();
	}
	
	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This method will cause the properties to be saved
	///   to their original source.
	/// </summary>
	//////////////////////////////////////////////////////////////
	
	public void Save()
	{
		_config.Save();
	}

	private readonly IConfigSupplier	_config;
	private ConfigResolver			_parent = null;
}

}
