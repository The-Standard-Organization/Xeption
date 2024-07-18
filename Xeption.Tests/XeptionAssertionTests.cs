// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Tynamix.ObjectFiller;

namespace Xeptions.Tests
{
    public partial class XeptionAssertionTests
    {
        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static KeyValuePair<string, List<string>> GenerateKeyValuePair(int count, string keyName = "")
        {
            if (string.IsNullOrWhiteSpace(keyName))
            {
                keyName = GetRandomString();
            }

            List<string> values = Enumerable.Range(start: 0, count: count)
                .Select(_ => GetRandomString())
                .ToList();

            var keyValuePair = new KeyValuePair<string, List<string>>(
                key: keyName,
                value: values);

            return keyValuePair;
        }

        internal class OtherTarget
        {
            public static void ThrowingExceptionMethod() =>
                throw new Exception();
        }
    }
}
