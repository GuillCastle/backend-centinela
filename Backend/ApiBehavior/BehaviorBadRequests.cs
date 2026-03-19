using Microsoft.AspNetCore.Mvc;

namespace Backend.ApiBehavior
{
    public static class BehaviorBadRequests
    {
        public static void Parsear(ApiBehaviorOptions option)
        {
            option.InvalidModelStateResponseFactory = ActionContext =>
            {
                var respuesta = new List<string>();
                foreach (var llave in ActionContext.ModelState.Keys)
                {
                    foreach (var error in ActionContext.ModelState[llave].Errors)
                    {
                        respuesta.Add($"{llave}: {error.ErrorMessage}");
                    }
                }
                return new BadRequestObjectResult(respuesta);
            };
        }
    }
}
