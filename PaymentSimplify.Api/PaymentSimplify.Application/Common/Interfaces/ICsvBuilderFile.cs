using System.Text;

namespace PaymentSimplify.Application.Common.Interfaces;

public interface ICsvBuilderFile
{
    byte[] GenerateCsv<T>(IEnumerable<T> data,Encoding encoding) where T : ICsvExport;
}

public interface ICsvExport
{
    
}