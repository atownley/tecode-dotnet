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
// File:	Properties.cs
// Created:	Sun Jun 20 21:38:37 IST 2004
//
//////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Text;

namespace TownleyEnterprises.Config {

//////////////////////////////////////////////////////////////////////
/// <summary>
///   <para>
///   This class more-or-less implements the java.util.Properties API.
///   It allows .NET applications to easily work with existing
///   property files in a similar manner to a Java application.  Why
///   would you want to do this?  The primary reason is if you need to
///   have a common configuration mechanism between .NET and J2SE
///   applications.
///   </para>
///   <para>
///   To load or save properties, use the PropertiesFileProcessor and
///   CollectionWriter objects.
///   </para>
/// </summary>
/// <version>$Id: Properties.cs,v 1.1 2004/06/21 08:39:27 atownley Exp $</version>
/// <author><a href="mailto:adz1092@netscape.net">Andrew S. Townley</a></author>
//////////////////////////////////////////////////////////////////////

public class Properties
{
	//////////////////////////////////////////////////////////////
	/// <summary>
	///   Creates an empty properties set with no values.
	/// </summary>
	//////////////////////////////////////////////////////////////
	
	public Properties()
	{
		_hash = new Hashtable();
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   Creates a new properties object initialized with the
	///   values from another properties set.
	/// </summary>
	/// <param name="defaults">the default values</param>
	//////////////////////////////////////////////////////////////
	
	public Properties(Properties defaults)
	{
		_hash = new Hashtable(defaults._hash);
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   Indexer to provide values as strings.
	/// </summary>
	//////////////////////////////////////////////////////////////
	
	public string this[string key]
	{
		get { return (string)_hash[key]; }
		set { _hash[key] = value; }
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This property returns the keys of the properties stored
	///   in the instance.
	/// </summary>
	//////////////////////////////////////////////////////////////

	public ICollection Keys
	{
		get { return _hash.Keys; }
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This property returns the values of the items in the
	///   instance.
	/// </summary>
	//////////////////////////////////////////////////////////////

	public ICollection Values
	{
		get { return _hash.Values; }
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This method clears the contents of the instance.
	/// </summary>
	//////////////////////////////////////////////////////////////

	public void Clear()
	{
		_hash.Clear();
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This method is used to determine if an item exists in
	///   the properties instance.
	/// </summary>
	/// <param name="key">the key to check</param>
	/// <returns>true if the key exists; false otherwise</returns>
	//////////////////////////////////////////////////////////////

	public bool Contains(string key)
	{
		return _hash.Contains(key);
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This method is used to output the entire section as a
	///   string.
	/// </summary>
	//////////////////////////////////////////////////////////////
	
	public override string ToString()
	{
		StringBuilder buf = new StringBuilder();
		
		foreach(string key in Keys)
		{
			buf.Append(key);
			buf.Append("=");
			buf.Append(this[key]);
			buf.Append("\n");
		}

		return buf.ToString();
	}

	private Hashtable	_hash = new Hashtable();
}

}
