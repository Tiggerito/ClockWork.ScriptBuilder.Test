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
using ClockWork.ScriptBuilder.JavaScript;
using ClockWork.ScriptBuilder.XmlScript;
using System.IO;

namespace ClockWork.ScriptBuilder.Test
{
	[TestFixture]
	public class JavaScriptTests
	{
		public static void AddJavaScript(Script script)
		{
			script.AddRange(
				"function helloWord() {",
				"	alert('Hello World');",
				"}",
				"",
				"helloWorld();",
				"",
				Js.Function("helloWord",
					Js.Statement(Js.Call("alert", Js.Q("Hello World"))),
					"line 2",
					Js.Block(
						"This should be blocked",
						"This should be blocked",
						Sb.Line("This should be blocked"),
						"This should be blocked",
						Sb.Indent(
							Sb.Line("This should be indented"),
							"This should be indented"
						),
						"This should be blocked",
						"This should be blocked",
						Sb.Wrapper(
							"/*",
							Sb.Script(ScriptLayout.InlineBlock,
								"comment",
								"comment",
								"comment",
								"comment",
								"comment",
								"comment"
							),
							"*/"
						),
						Sb.Line("1", "2", "3", "4"),
						Sb.ScriptIf(true, "This is true", "should not show"),
						Sb.ScriptIf(false, "should not show", "This is false"),
						Sb.ScriptIf(true, "This is true"),
						Sb.ScriptIf(false, "should not show")

					)
				),
				Js.Function("withParams",
					Js.Parameters("aaa", "ddd", "fff"),
					Js.Statement(Js.Call("alert", Js.Q("Hello World"))),
					"line 2"
				),
				Js.Function("withBigParams",
					Js.Parameters(ScriptLayout.Block,
						"aaa",
						"ddd",
						"fff"
					),
					Js.Statement(Js.Call("alert", Js.Q("Hello World"))),
					"line 2"
				),
				Sb.Line(),
				Js.Statement(Js.Call("helloWorld")),
				Sb.Line(),
				Js.Statement("var testBool = ", true),
				Js.Statement("var testDate = ", Js.Q(new DateTime(2008, 12, 18, 14, 30, 45))),
				Js.Statement("var testSecureString = ", Js.QQ(Encryption.CreateSecureString("pass'wo\"rd"))),
				Js.Statement("var testQuotes = ", Js.Q("dfgdfg'gdfgdfg'gdfsg\"dsfg")),
				Js.Statement("var testList = ", Js.List("1", "2", "3")),
				Js.Statement("var testArray = ", Js.Array("1", "2", "3")),
				Js.Statement("var testNew = ", Js.New("className", "a", "b")),
				Js.Statement("var testXml = ", Js.Q(
						Xs.Element("name",
							"tony"
						)
					)
				),
				Js.Statement("var testObject = ",
					Js.Object(
						Js.Property("name1", "value1"),
						Js.Property("name2", "value2"),
						Js.Property("name3", "value3"),
						Js.Property("name4", "value4")
					)
				),
				Js.Statement("var testScript = ",
					Sb.Script(
						Js.Property("name1", "value1"),
						Js.Property("name2", "value2"),
						Js.Property("name3", "value3"),
						Js.Property("name4", "value4")
					)
				),
				Js.Statement("var testObject = ",
					Js.Object(ScriptLayout.InlineBlock,
						Js.Property("name1", "value1"),
						Js.Property("name2", "value2"),
						Js.Property("name3", "value3"),
						Js.Property("name4", "value4"),
						Js.Property("name5",
							Js.Function(
								Js.Statement("statement1"),
								Js.Statement("statement2"),
								Js.Statement("statement3"),
								Js.Statement("statement4")
							)
						),
						Js.Property("name6",
							Js.Function(
								Js.Parameters("a", "b"),
								Js.Statement("statement1"),
								Js.Statement("statement2"),
								Js.Statement(Js.Call("test", "a", "b")),
								Js.Statement("statement4")
							)
						),
						Js.Property("shortfunction",
							Js.Function(ScriptLayout.Inline,
								Js.Parameters("a", "b"),
								Js.Statement("statement1")
							)
						)
					)
				),
				"END"

			);
		}

		[Test]
		public void Types()
		{
			Script script = Sb.Script();

			ScriptTests.AddTypes(script);

			string result = Js.Render(script);

			Console.WriteLine(result);
		}

		[Test]
		public void QuickWriteJs()
		{
			Script script = Sb.Script();

			AddJavaScript(script);

			string result = Js.Render(script);

			Console.WriteLine(result);
		}

		[Test]
		public void Xml()
		{
			Script script = Sb.Script();

			script.AddRange(
				Js.Statement("var xml = ", 
					Js.Q(
						Xs.Element("name",
							"tony"
						)
					)
				)
			);

			string result = Js.Render(script);

			Console.WriteLine(result);
		}

		[Test]
		public void MultiLineQuote()
		{
			Script script = Sb.Script();

			script.AddRange(
				Js.Statement("var superquote = ",
					Js.Q(
						Sb.Script(
							"Line 1",
							"Line 2",
							"Line 3"
						)
					)
				)
			);

			string result = Js.Render(script);

			Console.WriteLine(result);
		}


		[Test]
		public void Compressed()
		{
			Script script = Sb.Script();

			AddJavaScript(script);

			StringWriter sw = new StringWriter();
			ScriptWriter writer = new ScriptWriter(sw, JsFormatProvider.Instance);

			writer.IndentText = String.Empty;
			writer.NewLine = String.Empty;

			script.Render(writer);

			Console.WriteLine(sw.ToString());
		}
	}
}
