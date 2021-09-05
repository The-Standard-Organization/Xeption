// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Tynamix.ObjectFiller;

namespace Xeption.Tests
{
    public partial class XeptionTests
    {
        private static string GetRandomMessage() =>
            new MnemonicString().GetValue();

        private static Dictionary<string, List<object>> CreateRandomDictionary()
        {
            var filler = new Filler<Dictionary<string, List<object>>>();

            filler.Setup()
                .OnType<object>().Use(GetRandomObject());

            return filler.Create();
        }

        private static object GetRandomObject()
        {
            int randomValue = new IntRange(min: 0, max: 4).GetValue();

            return randomValue switch
            {
                0 => new MnemonicString().GetValue(),
                1 => new DateTimeRange(earliestDate: new DateTime()).GetValue(),
                2 => new EmailAddresses().GetValue(),
                3 => new IntRange().GetValue(),
                _ => Randomizer<bool>.Create()
            };
        }
    }
}