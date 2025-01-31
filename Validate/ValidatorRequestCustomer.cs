using System.Text.RegularExpressions;
using FluentValidation;
using PERT_2.Models.DTO;

namespace PERT_2.Validate
{
    public class ValidatorRequestCustomer : AbstractValidator<CustomerRequestDTO>
    {
        public ValidatorRequestCustomer() 
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).WithMessage("name Is Not Valid");
            RuleFor(x=>x.phoneNumber).NotEmpty().MinimumLength(9).MaximumLength(13).Must(ValidNumber);
        }
        public bool ValidNumber(string phoneNumber)
        {
            string regexNumberOnly = @"^\d+$";
            if (Regex.IsMatch(phoneNumber, regexNumberOnly))
                return true;
            else
                return false;

        }
    }
}
