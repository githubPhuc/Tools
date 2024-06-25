using AutoMapper;
using ToolsApp.EntityFramework;
using ToolsApp.Models;


namespace ToolsApp.App_Start
{
    public static class Mapper
    {
        private static IMapper _mapper;
        public static void RegisterMappings()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                #region User
                cfg.CreateMap<User, QL_UserViewModel>()
                                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
                                .ForMember(dto => dto.tenTaiKhoan, opt => opt.MapFrom(src => src.tenTaiKhoan))
                                .ForMember(dto => dto.hoVaTen, opt => opt.MapFrom(src => src.hoVaTen))
                                .ForMember(dto => dto.email, opt => opt.MapFrom(src => src.email))
                                .ForMember(dto => dto.anhDaiDien, opt => opt.MapFrom(src => src.anhDaiDien))
                                .ForMember(dto => dto.dcTamTru, opt => opt.MapFrom(src => src.dcTamTru))
                                .ForMember(dto => dto.dcThuongTru, opt => opt.MapFrom(src => src.dcThuongTru))
                                .ForMember(dto => dto.soTaiKhoan, opt => opt.MapFrom(src => src.soTaiKhoan))
                                .ForMember(dto => dto.tenTaiKhoan, opt => opt.MapFrom(src => src.tenTaiKhoan))
                                .ForMember(dto => dto.codeNganHang, opt => opt.MapFrom(src => src.codeNganHang))
                                .ForMember(dto => dto.soDienThoai, opt => opt.MapFrom(src => src.soDienThoai));

                cfg.CreateMap<QL_UserViewModel, User>()
                                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
                                .ForMember(dto => dto.tenTaiKhoan, opt => opt.MapFrom(src => src.tenTaiKhoan))
                                .ForMember(dto => dto.hoVaTen, opt => opt.MapFrom(src => src.hoVaTen))
                                .ForMember(dto => dto.email, opt => opt.MapFrom(src => src.email))
                                .ForMember(dto => dto.anhDaiDien, opt => opt.MapFrom(src => src.anhDaiDien))
                                .ForMember(dto => dto.dcTamTru, opt => opt.MapFrom(src => src.dcTamTru))
                                .ForMember(dto => dto.dcThuongTru, opt => opt.MapFrom(src => src.dcThuongTru))
                                .ForMember(dto => dto.soTaiKhoan, opt => opt.MapFrom(src => src.soTaiKhoan))
                                .ForMember(dto => dto.tenTaiKhoan, opt => opt.MapFrom(src => src.tenTaiKhoan))
                                .ForMember(dto => dto.codeNganHang, opt => opt.MapFrom(src => src.codeNganHang))
                                .ForMember(dto => dto.soDienThoai, opt => opt.MapFrom(src => src.soDienThoai));
                #endregion

                #region Page
                cfg.CreateMap<Page, QLKTPPageViewModel>()
                                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
                                .ForMember(dto => dto.controllerName, opt => opt.MapFrom(src => src.controllerName))
                                .ForMember(dto => dto.actionName, opt => opt.MapFrom(src => src.actionName))
                                .ForMember(dto => dto.Code, opt => opt.MapFrom(src => src.Code))
                                .ForMember(dto => dto.Info, opt => opt.MapFrom(src => src.Info))
                                .ForMember(dto => dto.ngayCapNhat, opt => opt.MapFrom(src => src.ngayCapNhat))
                                .ForMember(dto => dto.nguoiCapNhat, opt => opt.MapFrom(src => src.nguoiCapNhat))
                                .ForMember(dto => dto.nguoiTao, opt => opt.MapFrom(src => src.nguoiTao))
                                .ForMember(dto => dto.ngayTao, opt => opt.MapFrom(src => src.ngayTao))
                                .ForMember(dto => dto.xacNhanXoa, opt => opt.MapFrom(src => src.xacNhanXoa))
                                .ForMember(dto => dto.nguoiXoa, opt => opt.MapFrom(src => src.nguoiXoa))
                                .ForMember(dto => dto.ngayXoa, opt => opt.MapFrom(src => src.ngayXoa));

                cfg.CreateMap<QLKTPPageViewModel, Page>()
                                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
                                .ForMember(dto => dto.controllerName, opt => opt.MapFrom(src => src.controllerName))
                                .ForMember(dto => dto.actionName, opt => opt.MapFrom(src => src.actionName))
                                .ForMember(dto => dto.Code, opt => opt.MapFrom(src => src.Code))
                                .ForMember(dto => dto.Info, opt => opt.MapFrom(src => src.Info))
                                .ForMember(dto => dto.ngayCapNhat, opt => opt.MapFrom(src => src.ngayCapNhat))
                                .ForMember(dto => dto.nguoiCapNhat, opt => opt.MapFrom(src => src.nguoiCapNhat))
                                .ForMember(dto => dto.nguoiTao, opt => opt.MapFrom(src => src.nguoiTao))
                                .ForMember(dto => dto.ngayTao, opt => opt.MapFrom(src => src.ngayTao))
                                .ForMember(dto => dto.xacNhanXoa, opt => opt.MapFrom(src => src.xacNhanXoa))
                                .ForMember(dto => dto.nguoiXoa, opt => opt.MapFrom(src => src.nguoiXoa))
                                .ForMember(dto => dto.ngayXoa, opt => opt.MapFrom(src => src.ngayXoa));
                #endregion

                #region Menu
                cfg.CreateMap<Menu, QLKTPMenuViewModel>()
                                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
                                .ForMember(dto => dto.tenMenu, opt => opt.MapFrom(src => src.tenMenu))
                                .ForMember(dto => dto.parentId, opt => opt.MapFrom(src => src.parentId))
                                .ForMember(dto => dto.idPage, opt => opt.MapFrom(src => src.idPage))
                                .ForMember(dto => dto.iconMenu, opt => opt.MapFrom(src => src.iconMenu))
                                .ForMember(dto => dto.sortOrder, opt => opt.MapFrom(src => src.sortOrder))
                                             .ForMember(dto => dto.ngayCapNhat, opt => opt.MapFrom(src => src.ngayCapNhat))
                                .ForMember(dto => dto.nguoiCapNhat, opt => opt.MapFrom(src => src.nguoiCapNhat))
                                .ForMember(dto => dto.nguoiTao, opt => opt.MapFrom(src => src.nguoiTao))
                                .ForMember(dto => dto.ngayTao, opt => opt.MapFrom(src => src.ngayTao))
                                .ForMember(dto => dto.xacNhanXoa, opt => opt.MapFrom(src => src.xacNhanXoa))
                                .ForMember(dto => dto.nguoiXoa, opt => opt.MapFrom(src => src.nguoiXoa))
                                .ForMember(dto => dto.ngayXoa, opt => opt.MapFrom(src => src.ngayXoa));

                cfg.CreateMap<QLKTPMenuViewModel, Menu>()
                               .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
                                .ForMember(dto => dto.tenMenu, opt => opt.MapFrom(src => src.tenMenu))
                                .ForMember(dto => dto.parentId, opt => opt.MapFrom(src => src.parentId))
                                .ForMember(dto => dto.idPage, opt => opt.MapFrom(src => src.idPage))
                                .ForMember(dto => dto.iconMenu, opt => opt.MapFrom(src => src.iconMenu))
                                .ForMember(dto => dto.sortOrder, opt => opt.MapFrom(src => src.sortOrder))
                                             .ForMember(dto => dto.ngayCapNhat, opt => opt.MapFrom(src => src.ngayCapNhat))
                                .ForMember(dto => dto.nguoiCapNhat, opt => opt.MapFrom(src => src.nguoiCapNhat))
                                .ForMember(dto => dto.nguoiTao, opt => opt.MapFrom(src => src.nguoiTao))
                                .ForMember(dto => dto.ngayTao, opt => opt.MapFrom(src => src.ngayTao))
                                .ForMember(dto => dto.xacNhanXoa, opt => opt.MapFrom(src => src.xacNhanXoa))
                                .ForMember(dto => dto.nguoiXoa, opt => opt.MapFrom(src => src.nguoiXoa))
                                .ForMember(dto => dto.ngayXoa, opt => opt.MapFrom(src => src.ngayXoa));
                #endregion






            })
            {

            };
            _mapper = mapperConfiguration.CreateMapper();
        }

        //internal static object MapFrom(Qr_Event model)
        //{
        //    throw new NotImplementedException();
        //}

        #region User
        public static QL_UserViewModel MapFrom(User data)
        {
            return _mapper.Map<User, QL_UserViewModel>(data);
        }
        public static User MapFrom(QL_UserViewModel data)
        {
            return _mapper.Map<QL_UserViewModel, User>(data);
        }
        public static User MapFrom(QL_UserViewModel data_view, User data_entity)
        {
            return _mapper.Map(data_view, data_entity);
        }
        #endregion

        #region Page
        public static QLKTPPageViewModel MapFrom(Page data)
        {
            return _mapper.Map<Page, QLKTPPageViewModel>(data);
        }
        public static Page MapFrom(QLKTPPageViewModel data)
        {
            return _mapper.Map<QLKTPPageViewModel, Page>(data);
        }
        public static Page MapFrom(QLKTPPageViewModel data_view, Page data_entity)
        {
            return _mapper.Map(data_view, data_entity);
        }
        #endregion


        #region Menu
        public static QLKTPMenuViewModel MapFrom(Menu data)
        {
            return _mapper.Map<Menu, QLKTPMenuViewModel>(data);
        }
        public static Menu MapFrom(QLKTPMenuViewModel data)
        {
            return _mapper.Map<QLKTPMenuViewModel, Menu>(data);
        }
        public static Menu MapFrom(QLKTPMenuViewModel data_view, Menu data_entity)
        {
            return _mapper.Map(data_view, data_entity);
        }
        #endregion


    }
}