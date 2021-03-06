﻿#region License
/*
Copyright 2011 Andrew Davey

This file is part of Cassette.

Cassette is free software: you can redistribute it and/or modify it under the 
terms of the GNU General Public License as published by the Free Software 
Foundation, either version 3 of the License, or (at your option) any later 
version.

Cassette is distributed in the hope that it will be useful, but WITHOUT ANY 
WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS 
FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with 
Cassette. If not, see http://www.gnu.org/licenses/.
*/
#endregion

using Moq;
using Should;
using Xunit;

namespace Cassette.Scripts
{
    public class ScriptBundleHtmlRenderer_Tests
    {
        [Fact]
        public void GivenRendererWithUrlGenerator_WhenRenderBundle_ThenScriptHtmlIsReturned()
        {
            var urlGenerator = new Mock<IUrlGenerator>();
            var renderer = new ScriptBundleHtmlRenderer(urlGenerator.Object);
            var bundle = new ScriptBundle("~/test");
            urlGenerator.Setup(g => g.CreateBundleUrl(bundle))
                        .Returns("URL");

            var html = renderer.Render(bundle);

            html.ShouldEqual("<script src=\"URL\" type=\"text/javascript\"></script>");
        }
    }
}

