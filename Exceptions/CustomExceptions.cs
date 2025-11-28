namespace CarePlusApi.Exceptions
{
    // Exceção base para erros de negócio
    public abstract class CustomException : Exception
    {
        public int StatusCode { get; protected set; }

        protected CustomException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }

    // 404 Not Found
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message) : base(message, 404) { }
    }

    // 409 Conflict (Ex: Usuário já completou o desafio, ou já resgatou o benefício)
    public class ConflictException : CustomException
    {
        public ConflictException(string message) : base(message, 409) { }
    }

    // 422 Unprocessable Entity (Ex: Pontuação insuficiente, regra de negócio violada)
    public class BusinessRuleException : CustomException
    {
        public BusinessRuleException(string message) : base(message, 422) { }
    }
}
