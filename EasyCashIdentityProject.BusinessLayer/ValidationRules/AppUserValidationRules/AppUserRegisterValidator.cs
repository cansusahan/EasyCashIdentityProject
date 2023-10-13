
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.BusinessLayer.ValidationRules.AppUserValidationRules
{
    public class AppUserRegisterValidator:AbstractValidator<DtoLayer.Dtos.AppUserDtos.AppUserRegisterDto>
    {
        public AppUserRegisterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı boş gecilemez");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad alanı boş gecilemez");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Kullanıcı adı adı boş gecilemez");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email alanı boş gecilemez");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş gecilemez");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şifre tekrar alanı boş gecilemez");
            RuleFor(x => x.Name).MaximumLength(30).WithMessage("Lütfen en fazla 30 karakter giriş yapın");
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("Lütfen en az 2 karakter veri girişi yapın");
            RuleFor(x => x.ConfirmPassword).Equal(y=>y.Password).WithMessage("Parolanız eşleşmiyor");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen geçerli bir mail adresi giriniz");


        }
    }

}
