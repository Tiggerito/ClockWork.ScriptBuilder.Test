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
using ClockWork.ScriptBuilder.XmlScript;
using System.Xml;

namespace ClockWork.ScriptBuilder.Test
{
	[TestFixture]
	public class XmlTests
	{

		public void ToConsoleViaXmlDocument(XsElement rootElement)
		{
			XmlDocument doc = new XmlDocument();

			rootElement.Render(doc);

			XmlTextWriter tw = new XmlTextWriter(Console.Out);
			tw.Formatting = Formatting.Indented;

			doc.WriteTo(tw);
		}


		[Test]
		public void Types()
		{
			XsElement element = Xs.Element(ScriptLayout.Block, "Types");

			ScriptTests.AddTypes(element);

			string result = Xs.Render(element);

			Console.WriteLine(result);

			ToConsoleViaXmlDocument(element);
		}

		[Test]
		public void Test1()
		{

			XsElement rootElement =
				Xs.Element("Root",
					Xs.Element("Name", "Tony", Xs.Attribute("cool", true)),
					Xs.Element("Age", 5),
					Xs.Element("Deep",
						Xs.Element("Deeper",
							Xs.Element("Name", "Tony", Xs.Attribute("cool", true)),
							Xs.Element("Age", 5),
							Xs.Element("Price", 5.6),
							Xs.Element("Today", DateTime.Now),
							Xs.Element("Soon", new TimeSpan(1, 34, 56))
						)
					),
					"This is free text so should be encoded <&\"'>",
					"This is free text so should be encoded <&\"'>",
					Xs.Element("Stuff", Xs.CData("dont need to encode  <&\"'>   r43t35h47o9877654h55v$%^&*()*0779869^(*^^(?./,,{P")),
					Xs.Element("More", Xs.CData("try and confuse the cdata ]]> with that")),
					Xs.Element("Sex", true),
					"This is free text so should be encoded <&\"'>"
				);


			string result = Xs.Render(rootElement);

			Console.WriteLine(result);

			ToConsoleViaXmlDocument(rootElement);


		}

		[Test]
		public void MultiLineCData()
		{

			XsElement rootElement =
				Xs.Element("Root",
					Xs.CData(
						Sb.Script(
							"1234567890",
							"1234567890",
							"1234567890",
							"1234567890",
							"1234567890",
							"1234567890",
							"1234567890",
							"1234567890"
						)
					)

				);
			string result = Xs.Render(rootElement);

			Console.WriteLine(result);

			ToConsoleViaXmlDocument(rootElement);


		}


		[Test]
		public void JavascriptCData()
		{
			Script script = Sb.Script();

			JavaScriptTests.AddJavaScript(script);

			XsElement rootElement =
				Xs.Element("Root",
					Xs.CData(
						script
					)

				);
			string result = Xs.Render(rootElement);

			Console.WriteLine(result);

			ToConsoleViaXmlDocument(rootElement);


		}

		[Test]
		public void JavascriptText()
		{
			Script script = Sb.Script();

			JavaScriptTests.AddJavaScript(script);

			XsElement rootElement =
				Xs.Element("Root",
					script
				);
			string result = Xs.Render(rootElement);

			Console.WriteLine(result);

			ToConsoleViaXmlDocument(rootElement);


		}

	}
}
