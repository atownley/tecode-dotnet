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
// File:	feather.cs
// Created:	Tue Jul 20 09:29:25 IST 2004
//
//////////////////////////////////////////////////////////////////////

using System;
using System.Text;
using TownleyEnterprises.Command;

//////////////////////////////////////////////////////////////////////
/// <summary>
///   This is an example of a hypothetical file archive program
///   roughly based on the UNIX tar command.  It is intended to
///   illustrate the proper use of the CommandParser and the Command
///   namespace.
/// </summary>
/// <version>$Id: feather3.cs,v 1.3 2004/07/23 05:51:00 atownley Exp $</version>
/// <author><a href="mailto:adz1092@netscape.net">Andrew S. Townley</a></author>
//////////////////////////////////////////////////////////////////////

public class feather
{
	static CommandOption _create = new CommandOption("create", 'c', false, null, "create a new archive");
	
	static CommandOption _extract = new CommandOption("extract", 'x', false, null, "extract files from the named archive");
	
	static CommandOption _file = new CommandOption("file", 'f', true, "ARCHIVE", "specify the name of the archive (default is stdout)");
	
	static CommandOption _verbose = new CommandOption("verbose", 'v', false, null, "print status information during execution");

	static RepeatableCommandOption _xclude = new RepeatableCommandOption("exclude", 'X', "[ FILE | DIRECTORY ]", "exclude the named file or directory from the archive");

	static PosixCommandOption _display = new PosixCommandOption("display", true, "DISPLAY", "specify the display on which the output should be written");

	static JoinedCommandOption _options = new JoinedCommandOption('D', false, "PROPERTY=VALUE[,PROPERTY=VALUE...]", "set specific run-time properties", true);

	static CommandOption[] _mainopts = { _create, _extract, _file, _verbose, _xclude };

	static CommandOption[] _examples = { _display, _options };

	public static void Main(string[] args)
	{
		new feather(args);
	}
	
	private feather(string[] args)
	{
		CommandParser parser = new CommandParser("feather", "FILE...");
		parser.SetExitOnMissingArg(true, -10);

		// this is ugly and you wouldn't do this in real code,
		// but it serves to illustrate the method call
		parser.SetExtraHelpText("This is the TE-Code feather program.  It is used to illustrate the features of the TownleyEnterprises.Command package.\n\nExamples:\n  # create archive.feather from files one, two, three and four\n  feather -cvf archive.feather one two three four\n\n  # exclude files five and six from an archive\n  feather -cvf archive.feather -X five -X six one two three\n\nAll options are not required unless otherwise stated in the description.", "This utility does not actually create an archive.\nAny bugs in the software should be reported to the te-code mailing lists.\n\nhttp://te-code.sourceforge.net");

		parser.AddCommandListener(new DefaultCommandListener("feather options", _mainopts));
		parser.AddCommandListener(new DefaultCommandListener("Example options", _examples));

		parser.Parse(args);

		// initialize the delegate
		_create.OnExecute += CreateArchive;

		parser.AddConstraint(new MutexOptionConstraint(-500, _create, _extract));
		parser.AddConstraint(new RequiresAnyOptionConstraint(-501, _file, new CommandOption[] { _create, _extract }));
		parser.AddConstraint(new RequiresAnyOptionConstraint(-502, _xclude, new CommandOption[] { _create, _extract }));

		parser.ExecuteCommands(this);
	}

	private void CreateArchive(Object sender, ExecuteEventArgs ea)
	{
		string[] largs = ea.Parser.UnhandledArguments;

		if(largs.Length == 0)
		{
			Console.WriteLine("error:  refusing to create empty archive.");
			ea.Parser.Usage();
			System.Environment.Exit(-2);
		}

		if(_verbose.Matched && _file.Matched)
		{
			Console.WriteLine("creating archive '{0}'", _file.Arg);
		}

		for(int i = 0; i < largs.Length; ++i)
		{
			if(_verbose.Matched)
				Console.WriteLine("adding {0}", largs[i]);
		}

		if(_xclude.Matched)
		{
			if(_verbose.Matched)
			{
				foreach(string s in _xclude.Args)
				{
					Console.WriteLine("excluded {0}", s);
				}
			}
		}
	}
}
