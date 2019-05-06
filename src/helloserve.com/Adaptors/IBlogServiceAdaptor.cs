using helloserve.com.Models;
using System.Threading.Tasks;

namespace helloserve.com.Adaptors
{
    public interface IBlogServiceAdaptor
    {
        Task<BlogView> Read(string title);
    }
}
