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
// File:	ExecuteEventArgs.cs
// Created:	Tue Jul 20 12:24:34 IST 2004
//
//////////////////////////////////////////////////////////////////////

using System;
using System.Text;

namespace TownleyEnterprises.Command {

//////////////////////////////////////////////////////////////////////
/// <summary>
///   This class provides information to event subscribers.
/// </summary>
/// <version>$Id: ExecuteEventArgs.cs,v 1.2 2004/07/23 05:53:44 atownley Exp $</version>
/// <author><a href="mailto:adz1092@yahoo.com">Andrew S. Townley</a></author>
//////////////////////////////////////////////////////////////////////

public sealed class ExecuteEventArgs: EventArgs
{
	//////////////////////////////////////////////////////////////
	/// <summary>
	///   The constructor takes a reference to the parser which is
	///   available to the handler.
	/// </summary>
	//////////////////////////////////////////////////////////////
	
	public ExecuteEventArgs(CommandParser parser, CommandOption opt)
	{
		_parser = parser;
		_option = opt;
	}

	public CommandOption Option
	{
		get { return _option; }
	}

	public CommandParser Parser
	{
		get { return _parser; }
	}

	public override string ToString()
	{
		StringBuilder buf = new StringBuilder("[TownleyEnterprises.Command.ExecuteEventArgs: parser = ");
		buf.Append(_parser);
		buf.Append("; option = ");
		buf.Append(_option);
		buf.Append(" ]");

		return buf.ToString();
	}

	private readonly CommandOption _option;
	private readonly CommandParser _parser;
}

}
