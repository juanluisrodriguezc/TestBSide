using System;
using System.Collections.Generic;
using System.Text;

namespace eShopOnContainers.Core.Validations
{
    public class IsDecimalRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;

            return decimal.TryParse(str, out decimal number);
        }
    }
}
