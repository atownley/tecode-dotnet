######################################################################
##
## Copyright (c) 2004, Andrew S. Townley
## All rights reserved.
## 
## Redistribution and use in source and binary forms, with or without
## modification, are permitted provided that the following conditions
## are met:
## 
##     * Redistributions of source code must retain the above
##     copyright notice, this list of conditions and the following
##     disclaimer.
## 
##     * Redistributions in binary form must reproduce the above
##     copyright notice, this list of conditions and the following
##     disclaimer in the documentation and#or other materials provided
##     with the distribution.
## 
##     * Neither the names Andrew Townley and Townley Enterprises,
##     Inc. nor the names of its contributors may be used to endorse
##     or promote products derived from this software without specific
##     prior written permission.  
## 
## THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
## "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
## LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
## FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
## COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
## INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
## (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
## SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
## HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
## STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
## ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED
## OF THE POSSIBILITY OF SUCH DAMAGE.
##
## File:	Makefile
## Created:	Tue May 18 14:24:27 IST 2004
## Version:	$Id: Makefile,v 1.1 2004/05/19 10:43:38 atownley Exp $
##
## This is a *very* simple makefile for building TE-Common.NET using
## the command line compiler.  For Windows, the following command
## should be used:
##
## c:\te-common\cs> nmake csc
##
## To delete the files, use the following:
##
## c:\te-common\cs> nmake DEL=del clean
##
## or simply delete the te-common.dll.
##
## For the Mono environment under Linux, no special targets are needed.
##
######################################################################

# some defintions for Mono (http://mono.ximian.com)
CSC		= mcs
DEL		= rm
SEP		= /

MAINLIB		= te-common.dll

$(MAINLIB): common/AssemblyInfo.cs \
		command$(SEP)AbstractCommandListener.cs \
		command$(SEP)CommandOption.cs \
		command$(SEP)CommandParser.cs \
		command$(SEP)ICommandListener.cs \
		command$(SEP)DelimitedCommandOption.cs \
		command$(SEP)JoinedCommandOption.cs \
		command$(SEP)PosixCommandOption.cs \
		command$(SEP)RepeatableCommandOption.cs 
	$(CSC) $(CSCFLAGS) -target:library -out:$@ $?

clean:
	- $(DEL) $(MAINLIB)

######################################################################
# some makefile games for Windows
######################################################################

csc:
	$(MAKE) CSC="csc" CSCFLAGS="-nologo" SEP="\\"
