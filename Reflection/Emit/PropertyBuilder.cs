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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Utilities.Reflection.Emit.Interfaces;
#endregion

namespace Utilities.Reflection.Emit
{
    /// <summary>
    /// Helper class for defining a property
    /// </summary>
    public class PropertyBuilder:IPropertyBuilder
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="TypeBuilder">Type builder</param>
        /// <param name="Name">Name of the property</param>
        /// <param name="Attributes">Attributes for the property (public, private, etc.)</param>
        /// <param name="PropertyType">Property type for the property</param>
        /// <param name="Parameters">Parameter types for the property</param>
        public PropertyBuilder(TypeBuilder TypeBuilder, string Name,
            PropertyAttributes Attributes,MethodAttributes GetMethodAttributes,
            MethodAttributes SetMethodAttributes,
            Type PropertyType, List<Type> Parameters)
        {
            if (TypeBuilder==null)
                throw new ArgumentNullException("TypeBuilder");
            if (string.IsNullOrEmpty(Name))
                throw new ArgumentNullException("Name");
            this.Name = Name;
            this.Type = TypeBuilder;
            this.Attributes = Attributes;
            this.GetMethodAttributes = GetMethodAttributes;
            this.SetMethodAttributes = SetMethodAttributes;
            this.PropertyType = PropertyType;
            this.ParameterTypes = new List<System.Type>();
            if (Parameters != null)
            {
                this.ParameterTypes.AddRange(Parameters);
            }
            Setup();
        }

        #endregion

        #region Functions

        private void Setup()
        {
            if (Type == null)
                throw new NullReferenceException("No type is associated with this property");
            Builder = Type.Builder.DefineProperty(Name, Attributes,PropertyType,
                (ParameterTypes != null && ParameterTypes.Count > 0) ? ParameterTypes.ToArray() : System.Type.EmptyTypes);
            GetMethod = new MethodBuilder(Type, "get_" + Name, GetMethodAttributes, ParameterTypes, PropertyType);
            List<Type> SetParameters = new List<System.Type>();
            if (ParameterTypes != null)
            {
                SetParameters.AddRange(ParameterTypes);
            }
            SetParameters.Add(PropertyType);
            SetMethod = new MethodBuilder(Type, "set_" + Name, SetMethodAttributes, SetParameters,typeof(void));
            Builder.SetGetMethod(GetMethod.Builder);
            Builder.SetSetMethod(SetMethod.Builder);
        }

        #endregion

        #region Properties

        public string Name { get; private set; }
        public Type PropertyType { get; private set; }
        public List<Type> ParameterTypes { get; private set; }
        public System.Reflection.Emit.PropertyBuilder Builder { get; private set; }
        public System.Reflection.PropertyAttributes Attributes { get; private set; }
        public System.Reflection.MethodAttributes GetMethodAttributes { get; private set; }
        public System.Reflection.MethodAttributes SetMethodAttributes { get; private set; }
        public MethodBuilder GetMethod { get; private set; }
        public MethodBuilder SetMethod { get; private set; }

        private TypeBuilder Type { get; set; }

        #endregion
    }
}
