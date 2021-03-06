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

using System;
using Cassette.Configuration;

namespace Cassette.Scripts
{
    class ExternalScriptBundle : ScriptBundle
    {
        readonly string url;
        readonly string fallbackCondition;
        ExternalScriptBundleHtmlRenderer externalRenderer;

        public ExternalScriptBundle(string url)
            : base(url)
        {
            ValidateUrl(url);
            this.url = url;
        }

        public ExternalScriptBundle(string url, string bundlePath, string fallbackCondition = null)
            : base(bundlePath)
        {
            ValidateUrl(url);
            this.url = url;
            this.fallbackCondition = fallbackCondition;
        }

        static void ValidateUrl(string url)
        {
            if (url == null) throw new ArgumentNullException("url");
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentException("URL is required.", "url");
        }

        internal string Url
        {
            get { return url; }
        }

        internal string FallbackCondition
        {
            get { return fallbackCondition; }
        }

        internal override void Process(CassetteSettings settings)
        {
            // Any fallback assets are processed like a regular ScriptBundle.
            base.Process(settings);
            // We just need a special renderer instead.
            externalRenderer = new ExternalScriptBundleHtmlRenderer(Renderer, settings);
        }

        internal override string Render()
        {
            return externalRenderer.Render(this);
        }

        internal override bool ContainsPath(string pathToFind)
        {
            return base.ContainsPath(pathToFind) || url.Equals(pathToFind, StringComparison.OrdinalIgnoreCase);
        }
    }
}