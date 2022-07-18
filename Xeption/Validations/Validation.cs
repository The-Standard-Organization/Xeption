// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;

namespace Xeptions.Validations
{
    public static class Validation
    {
        public static void Validate<T>(params (dynamic Rule, string Parameter)[] validations)
            where T : Xeption
        {
            var invalidException = (T)Activator.CreateInstance(typeof(T), new object[] { });

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidException.ThrowIfContainsErrors();
        }

        public static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        public static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        public static dynamic IsNotSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not the same as {secondDateName}"
            };

        public static dynamic IsSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate == secondDate,
                Message = $"Date is the same as {secondDateName}"
            };

        public static dynamic IsNotRecent(
            DateTimeOffset inputDateTime,
            DateTimeOffset currentDateTime,
            double withinMinutes = 1) => new
            {
                Condition = IsDateNotRecent(inputDateTime, currentDateTime, withinMinutes),
                Message = "Date is not recent"
            };

        public static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static bool IsDateNotRecent(
            DateTimeOffset inputDateTime,
            DateTimeOffset currentDateTime,
            double withinMinutes = 1)
        {
            TimeSpan timeDifference = currentDateTime.Subtract(inputDateTime);
            TimeSpan oneMinute = TimeSpan.FromMinutes(1);

            return timeDifference.Duration() > oneMinute;
        }
    }
}
