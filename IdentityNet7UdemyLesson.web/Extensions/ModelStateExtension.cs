using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IdentityNet7UdemyLesson.web.Extensions
{
    public static class ModelStateExtension
    {
        public static void AddModelErrorList(this ModelStateDictionary modelState,List<string> errors)
        {
            errors.ForEach(error =>
            {
                modelState.AddModelError(string.Empty, error);
            });
        }
    }
}
