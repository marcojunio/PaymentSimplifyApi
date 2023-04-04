using System.Text;

namespace PaymentSimplify.Application.Common.Interfaces;

public interface ICsvBuilderFile
{
    byte[] GenerateCsv(IEnumerable<object> data,Encoding encoding);
}