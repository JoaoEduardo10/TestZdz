﻿using System.Runtime.Serialization;

namespace Core.Exceptions
{
    public class OrderException : ApplicationException
    {
        public string Code { get; set; }

        public OrderException(string message, string code) : base(message)
        {
            Code = code;
        }


    }
}
