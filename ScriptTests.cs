/*
 * Copyright (c) 2008, Anthony James McCreath
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     1 Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     2 Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     3 Neither the name of the project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY Anthony James McCreath "AS IS" AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL Anthony James McCreath BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.IO;
using ClockWork.ScriptBuilder.JavaScript;
using ClockWork.ScriptBuilder.XmlScript;
using System.Xml;

namespace ClockWork.ScriptBuilder.Test
{
	[TestFixture]
	public class ScriptTests
	{
		public static void AddScript(ScriptSet scriptSet)
		{
			scriptSet.AddRange(
				"line 1",
				"line 2",
				"line 3",
				"line 4",
				"line 5",
				"line 6",
				Sb.Line("1","2","3","4","5"),
				Sb.Line("1","2","3","4","5"),
				Sb.Indent(
					"indented",
					"line 1",
					"line 2",
					"line 3",
					"line 4",
					"line 5",
					"line 6",
					Sb.Line("1","2","3","4","5"),
					Sb.Line("1","2","3","4","5")
				),
				Sb.Script(
					"script",
					"line 1",
					"line 2",
					"line 3",
					"line 4",
					"line 5"
				),
				Sb.Wrapper("(",Sb.Line("1","2","3"),")"),
				Sb.Wrapper("{", Sb.Script("1", "2", "3"), "}")
			);
		}
		public static void AddTypes(ScriptSet scriptSet)
		{
			scriptSet.AddRange(
				Sb.Line("DateTime = ", new DateTime(2008, 12, 18, 18, 34, 56)),
				Sb.Line("Boolean = ", true),
				Sb.Line("Int = ", 35345),
				Sb.Line("Float = ", 35454.677),
				Sb.Line("TimeSpan = ", new TimeSpan(1, 2, 3, 4, 5)),
				Sb.Line("String = ", "the cat sat on the mat"),
				Sb.Line("String2 = ", "some special characters \"\'&^%$#@!*`~;:<>,./?=-+_"),
				Sb.Line("String3 = ", "string on" + Environment.NewLine + "two lines"),
				Sb.Line("SecureString = ", Encryption.CreateSecureString("password"))

			);
		}

		[Test]
		public void Types()
		{
			Script script = Sb.Script();

			ScriptTests.AddTypes(script);

			string result = Sb.Render(script);

			Console.WriteLine(result);
		}

		[Test]
		public void QuickWrite()
		{
			Script script = Sb.Script();

			AddScript(script);

			string result = Sb.Render(script);

			Console.WriteLine(result);
		}

		[Test]
		public void FullWrite()
		{
			Script script = new Script();

			AddScript(script);

			TextWriter stringWriter = new StringWriter();

			ScriptWriter scriptWriter = new ScriptWriter(stringWriter);

			script.Render(scriptWriter);

			string result = stringWriter.ToString();

			Console.WriteLine(result);
		}

		[Test]
		public void Article1()
		{
			Script script = Sb.Script(
				"line 1;",
				"line 2;",
				"line 3;"
			);

			string result = Sb.Render(script);

			Console.WriteLine(result);
		}
		[Test]
		public void Article2()
		{
			Script script = Sb.Script(
				Sb.Line("1", "2", "3", "4", "5", ";"),
				Sb.Line("1", "2", "3", "4", "5", ";"),
				Sb.Indent(
					Sb.Line("indented ", "1", "2", "3", "4", "5", ";"),
					Sb.Line("indented ", "1", "2", "3", "4", "5", ";"),
					Sb.Script(
						Sb.Line("Some objects"),
						Sb.Line("Number = ", 47),
						Sb.Line("True = ", true),
						Sb.Line("Now is = ", DateTime.Now)
					)
				)
			);

			string result = Sb.Render(script);

			Console.WriteLine(result);
		}


	}
}
