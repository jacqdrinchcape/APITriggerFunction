﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace APITriggerFunction.Model
{
    public class ValidationExtension
    {

        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
        public class AtLeastOnePropertyAttribute : ValidationAttribute
        {
            private string[] PropertyList { get; set; }

            public AtLeastOnePropertyAttribute(params string[] propertyList)
            {
                this.PropertyList = propertyList;
            }

            //See http://stackoverflow.com/a/1365669
            public override object TypeId
            {
                get
                {
                    return this;
                }
            }

            public override bool IsValid(object value)
            {
                PropertyInfo propertyInfo;
                foreach (string propertyName in PropertyList)
                {
                    propertyInfo = value.GetType().GetProperty(propertyName);

                    if (propertyInfo != null && propertyInfo.GetValue(value, null) != null)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}