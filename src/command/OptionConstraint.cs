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
// File:	OptionConstraint.cs
// Created:	Tue Jul 20 13:54:03 IST 2004
//
//////////////////////////////////////////////////////////////////////

namespace TownleyEnterprises.Command {

//////////////////////////////////////////////////////////////////////
/// <summary>
///   This is the base class for all the option constraints.
/// </summary>
/// <version>$Id: OptionConstraint.cs,v 1.1 2004/07/20 13:16:41 atownley Exp $</version>
/// <author><a href="mailto:adz1092@yahoo.com">Andrew S. Townley</a></author>
//////////////////////////////////////////////////////////////////////

public abstract class OptionConstraint
{
	//////////////////////////////////////////////////////////////
	/// <summary>
	///   The constructor initializes the option being
	///   constrained, an exit status and message to be used
	///   during failure.
	/// </summary>
	///
	/// <param name="opt">the option being constrained.</param>
	/// <param name="status">the exit status on failure</param>
	/// <param name="msg">the message to report</param>
	//////////////////////////////////////////////////////////////

	protected OptionConstraint(CommandOption option,
				int status, string msg)
	{
		_option = option;
		_message = msg;
		_status = status;
	}

	public int ExitStatus
	{
		get { return _status; }
	}

	public CommandOption Option
	{
		get { return _option; }
	}

	public bool OK
	{
		get { return Check(); }
	}

	public virtual string Message
	{
		get { return _message; }
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This method should be returned by derived classes to
	///   implement the specific constraint check.
	/// </summary>
	/// <returns>true if the constraint is valid; false
	/// otherwise</returns>
	//////////////////////////////////////////////////////////////
	
	protected abstract bool Check();

	private readonly CommandOption	_option;
	private readonly string		_message;
	private readonly int		_status;
}

}
