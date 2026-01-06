namespace Blog.Extensions
    {
    public static class ModelStateExtesions
        {
        public static List<string> GetErrors(this Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)
            {
            var errors = new List<string>();
            foreach (var state in modelState)
                {
                foreach (var error in state.Value.Errors)
                    {
                    errors.Add(error.ErrorMessage);
                    }
                }
            return errors;
            //return modelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            }
        }
    }
