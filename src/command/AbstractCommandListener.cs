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
// File:	AbstractCommandListener.cs
// Created:	Tue May 18 11:48:17 IST 2004
//
//////////////////////////////////////////////////////////////////////

namespace TownleyEnterprises.Command {

//////////////////////////////////////////////////////////////////////
/// <summary>
///   This abstract class is intended to easily support the
///   implementation of command-line argument handler classes
///   by providing an empty optionMatched method.
/// </summary>
/// <version>$Id: AbstractCommandListener.cs,v 1.3 2004/07/20 10:22:08 atownley Exp $</version>
/// <author><a href="mailto:adz1092@yahoo.com">Andrew S. Townley</a></author>
//////////////////////////////////////////////////////////////////////

public abstract class AbstractCommandListener: ICommandListener
{
	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This property is used to retrieve the
	///   description of the command listener's options
	///   when printing the help message.
	/// </summary>
	//////////////////////////////////////////////////////////////
	
	public abstract string Description
	{
		get;
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This property is used to retrieve the arguments
	///   to be handled by the listener.
	/// </summary>
	///
	/// <returns>an array of CommandOption
	/// arguments</returns>
	//////////////////////////////////////////////////////////////

	public abstract CommandOption[] Options
	{
		get;
	}

	//////////////////////////////////////////////////////////////
	/// <summary>
	///   This method is called whenever the argument
	///   registered the parser is detected.  It is not
	///   normally used except in "classic" mode.
	/// </summary>
	///
	/// <param name="opt">the option matched by the
	/// parser</param>
	/// <param name="arg">the option argument (if
	/// any)</param>
	//////////////////////////////////////////////////////////////

	public virtual void OptionMatched(CommandOption opt, string arg)
	{
	}
}
}
