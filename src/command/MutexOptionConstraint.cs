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
// File:	MutexOptionConstraint.cs
// Created:	Tue Jul 20 13:59:20 IST 2004
//
//////////////////////////////////////////////////////////////////////

namespace TownleyEnterprises.Command {

//////////////////////////////////////////////////////////////////////
/// <summary>
///   This class provides an implementation of a mutual exclusion
///   constraint for two options.
/// </summary>
/// <version>$Id: MutexOptionConstraint.cs,v 1.2 2004/07/23 05:54:11 atownley Exp $</version>
/// <author><a href="mailto:adz1092@yahoo.com">Andrew S. Townley</a></author>
//////////////////////////////////////////////////////////////////////

public class MutexOptionConstraint: OptionConstraint
{
	//////////////////////////////////////////////////////////////
	/// <summary>
	///   The basic constructor takes both the options and a
	///   status code.
	/// </summary>
	///
	/// <param name="status">the exit status on failure</param>
	/// <param name="opt">the option being constrained.</param>
	/// <param name="opt2">the other option</param>
	//////////////////////////////////////////////////////////////
	
	public MutexOptionConstraint(int status, CommandOption opt,
				CommandOption opt2)
		: base(status, opt,
			"error:  cannot specify both '{0}' and '{1}'")
	{
		_exoption = opt2;
		_custommsg = false;
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This version of the constructor allows the specification
	///   of a custom message.
	/// </summary>
	///
	/// <param name="status">the exit status on failure</param>
	/// <param name="opt">the option being constrained.</param>
	/// <param name="opt2">the other option</param>
	/// <param name="message">a custom message</param>
	//////////////////////////////////////////////////////////////
	
	public MutexOptionConstraint(int status, CommandOption opt,
				CommandOption opt2, string message)
		: base(status, opt, message)
	{
		_custommsg = true;
	}

	public override string Message
	{
		get
		{
			if(_custommsg)
				return base.Message;

			string s1, s2;

			s1 = Option.LongName;
			if(s1 == null)
				s1 = Option.ShortName.ToString();

			s2 = _exoption.LongName;
			if(s2 == null)
				s2 = _exoption.ShortName.ToString();
			return string.Format(base.Message, s1, s2);
		}
	}

	protected override bool Check()
	{
		return !(Option.Matched && _exoption.Matched);
	}

	private readonly bool		_custommsg;
	private readonly CommandOption	_exoption;
}

}
