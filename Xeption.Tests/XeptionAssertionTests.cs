﻿// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using Tynamix.ObjectFiller;

namespace Xeptions.Tests
{
    public partial class XeptionAssertionTests
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
