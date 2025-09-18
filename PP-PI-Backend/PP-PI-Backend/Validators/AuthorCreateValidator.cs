//using FluentValidation;
//using PP_PI_Backend.Data;
//using PP_PI_Backend.DTO.Author;
//namespace PP_PI_Backend.Validators
//{
//    public class AuthorCreateValidator : AbstractValidator<AuthorCreateDTO>
//{
//        public AuthorCreateValidator(LibraryDb db)
//        {
//            ClassLevelCascadeMode = CascadeMode.Stop;

//            RuleFor(x => x.FirstName)
//                .NotEmpty().WithMessage("FirstName is required.")
//                .MaximumLength(100);

//            RuleFor(x => x.LastName)
//                .NotEmpty().WithMessage("LastName is required.")
//                .MaximumLength(100);

//            RuleFor(x => x.Nationality)
//                .MaximumLength(100).When(x => !string.IsNullOrWhiteSpace(x.Nationality));

//            RuleFor(x => x.Birthdate)
//                .Must(d => d is null || d <= DateTime.UtcNow.Date)
//                .WithMessage("Birthdate cannot be in the future.");

//            // Regla de unicidad por Nombre+Apellido (ajústala si querés incluir Birthdate)
//            RuleFor(x => new { x.FirstName, x.LastName })
//                .MustAsync(async (n, ct) =>
//                    !await db.Authors.AnyAsync(a =>
//                        a.FirstName == n.FirstName && a.LastName == n.LastName, ct))
//                .WithMessage("An author with the same first and last name already exists.");
//        }
//    }
//    }
