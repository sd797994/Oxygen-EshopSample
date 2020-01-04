using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationBase
{
    public interface ICustomModelValidator
    {
        List<ValidationResult> Valid<T>(T instance);
    }
}
