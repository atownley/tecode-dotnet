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
// File:	RequiresAnyOptionConstraint.cs
// Created:	Tue Jul 20 13:59:20 IST 2004
//
//////////////////////////////////////////////////////////////////////

using System.Text;

namespace TownleyEnterprises.Command {

//////////////////////////////////////////////////////////////////////
/// <summary>
///   This class provides an implementation of a dependency 
///   constraint between options.
/// </summary>
/// <version>$Id: RequiresAnyOptionConstraint.cs,v 1.1 2004/07/20 13:16:41 atownley Exp $</version>
/// <author><a href="mailto:adz1092@yahoo.com">Andrew S. Townley</a></author>
//////////////////////////////////////////////////////////////////////

public class RequiresAnyOptionConstraint: OptionConstraint
{
	//////////////////////////////////////////////////////////////
	/// <summary>
	///   The basic constructor takes the options and a
	///   status code.
	/// </summary>
	///
	/// <param name="opt">the option being constrained.</param>
	/// <param name="status">the exit status on failure</param>
	/// <param name="deps">the other options</param>
	//////////////////////////////////////////////////////////////
	
	public RequiresAnyOptionConstraint(CommandOption opt,
				int status, CommandOption[] deps)
		: base(opt, status,
			"error:  option '{0}' requires one of:  ")
	{
		_deps = deps;
		_custommsg = false;
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This version of the constructor allows the specification
	///   of a custom message.
	/// </summary>
	///
	/// <param name="opt">the option being constrained.</param>
	/// <param name="status">the exit status on failure</param>
	/// <param name="deps">the other options</param>
	/// <param name="message">a custom message</param>
	//////////////////////////////////////////////////////////////
	
	public RequiresAnyOptionConstraint(CommandOption opt, 
				int status, CommandOption[] deps,
				string message)
		: base(opt, status, message)
	{
		_deps = deps;
		_custommsg = true;
	}

	public override string Message
	{
		get
		{
			if(_custommsg)
				return base.Message;

			string s = Option.LongName;
			if(s == null)
				s = Option.ShortName.ToString();

			string msg = string.Format(base.Message, s);

			StringBuilder buf = new StringBuilder(msg);
			int i = 0;
			foreach(CommandOption o in _deps)
			{
				s = o.LongName;
				if(s == null)
					s = o.ShortName.ToString();

				buf.Append("'");
				buf.Append(s);
				buf.Append("'");

				if(++i < _deps.Length)
				{
					if(i == _deps.Length - 1)
					{
						buf.Append(" or ");
					}
					else
					{
						buf.Append(", ");
					}
				}
			}

			return buf.ToString();
		}
	}

	protected override bool Check()
	{
		if(!Option.Matched)
			return true;

		foreach(CommandOption o in _deps)
		{
			if(o.Matched)
				return true;
		}

		return false;
	}

	private readonly CommandOption[] _deps;
	private readonly bool		_custommsg;
}

}
