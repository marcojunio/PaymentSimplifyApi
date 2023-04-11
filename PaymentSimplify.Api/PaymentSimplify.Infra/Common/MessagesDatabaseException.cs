namespace PaymentSimplify.Infra.Common;

public class MessagesDatabaseException
{
    private readonly Exception _dbUpdateException;

    public MessagesDatabaseException(Exception dbUpdateException)
    {
        _dbUpdateException = dbUpdateException;
    }

    public string GeneratedDetailsError()
    {
        if (_dbUpdateException.InnerException != null && _dbUpdateException.InnerException.Message.Contains("Duplicate"))
        {
            var constraintName = GetConstraintNameFromException(_dbUpdateException);
            return RegisterDuplicatedKeys()[constraintName];
        }
        
        return "";
    }

    private Dictionary<string, string> RegisterForeignKeysDelete()
    {
        return new Dictionary<string, string>()
        {
            
        };
    }
    
    private Dictionary<string,string> RegisterDuplicatedKeys()
    {
        
        return new Dictionary<string, string>()
        {
            {"IX_CUSTOMER_DOCUMENT","Document is already exists."},
            {"IX_AUTH_EMAIL","E-mail is already exists."},
        };
    }
    
    private string GetConstraintNameFromException(Exception exception)
    {
        var startKeyIndex = exception.InnerException?.Message.IndexOf("key '", StringComparison.Ordinal) + 5 ?? 0;
        var keyLength = exception.InnerException?.Message.Length - startKeyIndex - 1 ?? 0;

        if (startKeyIndex < 0 || keyLength <= 0)
            return null;

        var constraintName = exception.InnerException?.Message.Substring(startKeyIndex, keyLength);

        if (constraintName != null && !constraintName.Contains("."))
            return constraintName;

        var constraintNameSplitted = constraintName.Split(".");

        return constraintNameSplitted[1];
    }
}