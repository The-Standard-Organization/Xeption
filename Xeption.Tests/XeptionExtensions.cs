// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Tynamix.ObjectFiller;

namespace Xeption.Tests
{
    public partial class XeptionExtensions
    {
        private static string GetRandomString() =>
            new MnemonicString().GetValue();
    }
}
