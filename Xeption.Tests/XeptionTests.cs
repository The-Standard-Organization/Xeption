// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Tynamix.ObjectFiller;

namespace Xeptions.Tests
{
    public partial class XeptionTests
    {
        private static string GetRandomMessage() =>
            new MnemonicString().GetValue();

        private static Dictionary<string, List<string>> CreateRandomDictionary()
        {
            var filler = new Filler<Dictionary<string, List<string>>>();

            return filler.Create();
        }
    }
}