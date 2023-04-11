// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using Tynamix.ObjectFiller;

namespace Xeptions.Tests
{
    public partial class XeptionExtensionTests
    {
        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        internal class OtherTarget
        {
            public static void ThrowingExceptionMethod() =>
                throw new Exception();
        }

        private static Dictionary<string, List<string>> CreateRandomDictionary()
        {
            var filler = new Filler<Dictionary<string, List<string>>>();

            return filler.Create();
        }
    }
}
