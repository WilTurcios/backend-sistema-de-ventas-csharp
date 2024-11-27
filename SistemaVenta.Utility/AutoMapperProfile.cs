using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            #region Role
            CreateMap<Role, RoleDTO>().ReverseMap();
            #endregion Role

            #region MenuItem
            CreateMap<MenuItem, MenuItemDTO>().ReverseMap();
            #endregion MenuItem

            #region User
            CreateMap<User, UserDTO>()
                .ForMember(
                    destination => destination.RoleDescription, 
                    opt => opt.MapFrom(origin => origin.Role.Name)
                )
                .ForMember(
                    destiny => destiny.IsActive,
                    opt => opt.MapFrom(origin => origin.IsActive == true ? 1 : 0)
                );

            CreateMap<User, SessionDTO>()
                .ForMember(
                    destination => destination.RoleDescription,
                    opt => opt.MapFrom(origin => origin.Role.Name)
                );

            CreateMap<UserDTO, User>()
                .ForMember(
                    destination => destination.Role,
                    opt => opt.Ignore()
                )
                .ForMember(
                    destination => destination.IsActive,
                    opt => opt.MapFrom(origin => origin.IsActive == 1 ? true : false)
                );
            #endregion User

            #region Category
            CreateMap<Category, CategoryDTO>().ReverseMap();
            #endregion Category

            #region Product
            CreateMap<Product, ProductDTO>()
                .ForMember(
                    destination => destination.CategoryDescription,
                    opt => opt.MapFrom(origin => origin.Category.Name)
                )
                .ForMember(
                    destination => destination.Price,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.Price.Value, new CultureInfo("es-MX")))
                )
                .ForMember(
                    destination => destination.IsActive,
                    opt => opt.MapFrom(origin => origin.IsActive == true ? 1 : 0)
                );

            CreateMap<ProductDTO, Product>()
                .ForMember(
                    destination => destination.Category,
                    opt => opt.Ignore()
                )
                .ForMember(
                    destination => destination.Price,
                    opt => opt.MapFrom(origin => Convert.ToDecimal(origin.Price, new CultureInfo("es-MX")))
                )
                .ForMember(
                    destination => destination.IsActive,
                    opt => opt.MapFrom(origin => origin.IsActive == 1 ? true : false)
                );
            #endregion Product

            #region Sale
            CreateMap<Sale, SaleDTO>()
                .ForMember(
                    destination => destination.Total,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo("es-MX")))
                )
                .ForMember(
                    destination => destination.RegisterDate,
                    opt => opt.MapFrom(origin => origin.RegisterDate.Value.ToString("dd/MM/yyyy"))
                );

            CreateMap<SaleDTO, Sale>()
                .ForMember(
                    destination => destination.Total,
                    opt => opt.MapFrom(origin => Convert.ToDecimal(origin.Total, new CultureInfo("es-MX")))
                );

            #endregion Sale

            #region SaleDetails
            CreateMap<SaleDetails, SaleDetailsDTO>()
                .ForMember(
                    destination => destination.ProductDescription,
                    opt => opt.MapFrom(origin => origin.Product.Name)
                )
                .ForMember(
                    destination => destination.Price,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.Price.Value, new CultureInfo("es-MX")))
                )
                .ForMember(
                    destination => destination.Total,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo("es-MX")))
                );

            CreateMap<SaleDetailsDTO, SaleDetails>()
                .ForMember(
                    destination => destination.Price,
                    opt => opt.MapFrom(origin => Convert.ToDecimal(origin.Price, new CultureInfo("es-MX")))
                )
                .ForMember(
                    destination => destination.Total,
                    opt => opt.MapFrom(origin => Convert.ToDecimal(origin.Total, new CultureInfo("es-MX")))
                );

            #endregion SaleDetails

            #region Report
            CreateMap<SaleDetails, ReportDTO>()
                .ForMember(
                    destination => destination.RegisterDate,
                    opt => opt.MapFrom(origin => origin.Sale.RegisterDate)
                )
                .ForMember(
                    destination => destination.DocumentNumber,
                    opt => opt.MapFrom(origin => origin.Sale.DocumentNumber)
                )
                .ForMember(
                    destination => destination.PaymentMethod,
                    opt => opt.MapFrom(origin => origin.Sale.PaymentMethod)
                )
                .ForMember(
                    destination => destination.SaleTotal,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.Sale.Total.Value, new CultureInfo("es-MX")))
                )
                .ForMember(
                    destination => destination.Price,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.Price.Value, new CultureInfo("es-MX")))
                )
                .ForMember(
                    destination => destination.Total,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.Total .Value, new CultureInfo("es-MX")))
                )
                .ForMember(
                    destination => destination.Product,
                    opt => opt.MapFrom(origin => origin.Product.Name)
                );

            #endregion Report

        }
    }
}
