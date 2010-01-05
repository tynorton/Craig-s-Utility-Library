﻿/*
Copyright (c) 2010 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

#region Usings
using System;
using System.Security.Cryptography;
#endregion

namespace Utilities.Encryption
{
    /// <summary>
    /// Utility class to handle MD5 functions
    /// </summary>
    public static class MD5
    {
        #region Public Static Functions
        /// <summary>
        /// Computes a hash using MD5
        /// </summary>
        /// <param name="Input">Input string</param>
        /// <returns>A hash of the input string using MD5</returns>
        public static string ComputeHash(string Input)
        {
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            byte[] InputArray = System.Text.Encoding.ASCII.GetBytes(Input);
            byte[] HashedArray = MD5.ComputeHash(InputArray);
            return BitConverter.ToString(HashedArray).Replace("-","");
        }
        #endregion
    }
}
