﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18YearIfAMember: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer =(Customer) validationContext.ObjectInstance;

            if (customer.MembershipTypeId == 0||customer.MembershipTypeId == 1)
            {
                return ValidationResult.Success;
            }
            if (customer.BirthDate==null)
            {
                return new ValidationResult("Please enter birthdate");
            }
            var age = DateTime.Today.Year - customer.BirthDate.Year;

            return (age >= 18) ?
                ValidationResult.Success : 
                new ValidationResult("Customer should be 18 years old");
        }
    }
}