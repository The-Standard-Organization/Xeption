// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
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