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
// File:	IniFileProcessor.java
// Created:	Tue Jun 15 17:54:26 IST 2004
//
//////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using TownleyEnterprises.Common;

namespace TownleyEnterprises.IO {

//////////////////////////////////////////////////////////////////////
/// <summary>
///   This class provides an extension of the TextFileProcessor class
///   which is useful in parsing Windows Profiles (INI files).  Once
///   the file has been parsed, the sections can be retrieved for
///   further manipulation.
/// </summary>
/// <version>$Id: IniFileProcessor.cs,v 1.1 2004/06/15 17:21:15 atownley Exp $</version>
/// <author><a href="mailto:adz1092@netscape.net">Andrew S. Townley</a></author>
//////////////////////////////////////////////////////////////////////

public class IniFileProcessor: TextFileProcessor
{
	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This private class extends the AbstractLineProcessor to
	///   handle the parsing of the INI file.
	/// </summary>
	//////////////////////////////////////////////////////////////
	
	class IniReader: AbstractLineProcessor
	{
		public override void ProcessLine(string line)
		{
			base.ProcessLine(line);

			// ignore comments
			if(line.Length == 0 || line.StartsWith("#")
					|| line.StartsWith("'"))
			{
				return;
			}
			
			if(line.Trim().IndexOf("[") != 0)
			{
				ParseValue(line);
			}
			else
			{
				_currentSection = ParseSection(line);
				_sections.Add(_currentSection);
			}
		}

		public ICollection Sections
		{
			get { return _sections; }
		}

		private IniSection ParseSection(string line)
		{
			IniSection section = null;
			int idx = line.IndexOf("[");
			int idx2 = line.LastIndexOf("]");

			if(idx != -1 && idx2 != -1 && idx2 > idx)
			{
				section = new IniSection(line.Substring(idx + 1, idx2 - idx - 1));
			}

			return section;
		}

		private void ParseValue(string line)
		{
			int idx = line.IndexOf("=");
			if(idx != -1)
			{
				_currentSection[line.Substring(0, idx)] =
					line.Substring(idx + 1);
			}
		}

		private ArrayList	_sections = new ArrayList();
		private IniSection	_currentSection = null;
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   The constructor initializes the processor, but does not
	///   actually process the file.
	/// </summary>
	/// <param name="name">the file to process</param>
	//////////////////////////////////////////////////////////////

	public IniFileProcessor(string name) : base(name)
	{
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This method is called to actually process the file.
	///   Once processed, the sections can be retrieved via the
	///   Sections property.
	/// </summary>
	//////////////////////////////////////////////////////////////

	public void ProcessFile()
	{
		ProcessFile(_ir);
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This method clears the contents of the file which has
	///   been processed.
	/// </summary>
	//////////////////////////////////////////////////////////////
	
	public void Reset()
	{
		base.Reset();
		_ir = new IniReader();
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This property is used to retrieve the parsed section
	///   objects.
	/// </summary>
	//////////////////////////////////////////////////////////////

	public ICollection Sections
	{
		get { return (_ir == null) ? null : _ir.Sections; }
	}
	
	private IniReader	_ir = new IniReader();
}
