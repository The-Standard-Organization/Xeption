// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
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
    }
}
