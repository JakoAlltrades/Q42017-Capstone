﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace AzureSQLConnection.Models
{
    public class PasswordHasher
    {
        private byte[] passwordSalt =
        {
            90,152,15,166,227,113,149,89,123,90,28,129,147,99,79,214,
            121,141,229,245,189,116,128,251,190,200,26,65,54,53,118,161,
            195,252,161,244,112,87,169,10,110,0,220,170,17,103,201,200,
            215,136,11,156,67,196,47,113,45,196,237,95,219,218,240,52,
            169,168,140,75,27,228,31,94,17,250,20,38,139,1,42,208,
            92,208,184,95,216,64,20,88,51,10,70,198,231,23,19,190,
            0,92,1,89,217,227,110,5,144,132,249,242,67,19,144,107,
            116,54,192,217,5,20,40,33,76,97,254,108,181,160
        };

        public byte[] Hash(string value)
        {
            return Hash(Encoding.UTF8.GetBytes(value));
        }

        public byte[] Hash(byte[] value)
        {
            byte[] saltedValue = value.Concat(passwordSalt).ToArray();

            return new SHA256Managed().ComputeHash(saltedValue);
        }

        public bool ConfirmPassword(string password, byte[] storedHash)
        {
            byte[] passwordHash = Hash(password);

            return storedHash.SequenceEqual(passwordHash);
        }

    }
}