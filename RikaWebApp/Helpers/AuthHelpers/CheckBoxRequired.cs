using System.ComponentModel.DataAnnotations;

namespace RikaWebApp.Helpers.AuthHelpers;
public class CheckBoxRequired : ValidationAttribute
{
    public override bool IsValid(object? value) => value is bool b && b;
}
