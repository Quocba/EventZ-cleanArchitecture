using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Wrappers;

namespace Event.Application.Interface
{
    public interface IEventPackageRegistrationQuery
    {
        Task<PagedResponse<List<GetEventPackageRegistrationDTO>>> GetAllEventPackageRegistration(PagedRequest request,int? status);
        Task<GetEventPackageRegistrationDTO> GetEventPackageRegistrationDetail(Guid eventPackageRegistrationID);
        Task<PagedResponse<List<GetEventPackageRegistrationDTO>>> GetRegistrationByEventPackage(PagedRequest request,Guid eventPackageRegistrationID);

        Task<PagedResponse<List<GetEventPackageRegistrationDTO>>> GetEventPackageRegistrationToDay(PagedRequest request);
    }
}
