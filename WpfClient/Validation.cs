using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfClient
{
    public class ValidNumber : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                TextBox textBox = (TextBox)value;
                int num=int.Parse(textBox.Text);
                if (num > 100)
                {
                    return new ValidationResult(false, "number too high");
                }
            }
            catch
            {
                return new ValidationResult(false, "must enter numbers only");
            }
            return ValidationResult.ValidResult;
        }
    }
    public class ValidBirthday : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                DateTime date=DateTime.Parse(value.ToString());
                if (date < DateTime.Today.AddYears(-120))
                {
                    return new ValidationResult(false, "Too Old");
                }
                if (date > DateTime.Today.AddYears(-13))
                {
                    return new ValidationResult(false, "You have to be at least 13 yo");
                }
            }
            catch (Exception ex)
            {

                return new ValidationResult(false, "trash data :*|" + ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }

    public class ValidFirstName : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string firstname = value.ToString();
                if (firstname.Length < 2)
                {
                    return new ValidationResult(false, "names are longer than a letter");
                }
                if (firstname.Length > 20)
                {
                    return new ValidationResult(false, "get a shorter name");
                }
                for (int i = 0; i < firstname.Length; i++)
                {
                    if (!(Char.IsLetter(firstname[i])) || Char.IsWhiteSpace(firstname[i]))
                    {
                        return new ValidationResult(false, "names contain letters");
                    }

                }
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "name not valid->" + ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }

    public class ValidLastName : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string lastname = value.ToString();
                if (lastname.Length < 2)
                {
                    return new ValidationResult(false, "names are longer than a letter");
                }
                if (lastname.Length > 20)
                {
                    return new ValidationResult(false, "get a shorter name");
                }
                for (int i = 0; i < lastname.Length; i++)
                {
                    if (!(Char.IsLetter(lastname[i])) || Char.IsWhiteSpace(lastname[i]))
                    {
                        return new ValidationResult(false, "names contain letters");
                    }

                }
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "name not valid->" + ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }

    public class ValidEmail : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string Username = value.ToString();
                if (Username.IndexOf("@gmail.com") == -1)
                {
                    return new ValidationResult(false, "email must end with @gmail.com");
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Username is illegal" + ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }

    public class ValidPassword : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string Password = value.ToString();
                string symbols = "!@#$%^&*()`~;'?><._-+=[]{}|-";
                bool big = false, small = false, digit = false, sym = false;
                if (Password.Length > 69)
                {
                    return new ValidationResult(false, "Password is too long");
                }
                if (Password.Length < 8)
                {
                    return new ValidationResult(false, " password must be at least 8 characters");
                }
                for (int i = 0; i < Password.Length; i++)
                {
                    if (!Char.IsLetterOrDigit(Password[i]) && symbols.IndexOf(Password[i]) == -1)
                    {
                        return new ValidationResult(false, "password must contain letters and digits");
                    }
                    if (Char.IsUpper(Password[i]))
                    {
                        big = true;
                    }
                    if (Char.IsLower(Password[i]))
                    {
                        small = true;
                    }
                    if (Char.IsDigit(Password[i]))
                    {
                        digit = true;
                    }
                    if (symbols.IndexOf(Password[i]) != -1)
                    {
                        sym = true;
                    }
                }
                if (!(big && small && digit && sym))
                {
                    throw new Exception("password must contain upper and lower letters, symbols and numbers");
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Illegal password\n" + ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }
}

