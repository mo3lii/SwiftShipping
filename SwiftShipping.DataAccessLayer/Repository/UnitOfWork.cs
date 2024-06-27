using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.DataAccessLayer.Repository.CustomRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.DataAccessLayer.Repository
{
    public class UnitOfWork
    {
        private ApplicationContext context;
        //Generic Repositories 
        private GenericRepository<ApplicationUser> _appUserRepository; 
        private GenericRepository<Order> _orderRipository;
        private GenericRepository<Government> _governmentRipository; 
        private GenericRepository<Region> _regionRipository; 
        private GenericRepository<Branch> _branchRipository;
        private GenericRepository<DeliveryMan> _deliveryManRipository;
        private GenericRepository<DeliveryManRegions> _deliveryManRegionsRipository;
        private GenericRepository<Employee> _employeeRipository;
        private GenericRepository<Seller> _sellerRipository;
        private GenericRepository<Admin> _adminRipository;
        //Custom Repositories 
        private WeightSettingRepository _weightSettingRepository;

        public UnitOfWork(ApplicationContext _context)
        {
            context = _context;
        }

        public GenericRepository<ApplicationUser> AppUserRepository
        {
            get
            {
                if (_appUserRepository == null)
                {
                    _appUserRepository = new GenericRepository<ApplicationUser>(context);
                }
                return _appUserRepository;
            }
        }

        public GenericRepository<Order> OrderRipository
        {
            get
            {
                if (_orderRipository == null)
                {
                    _orderRipository = new GenericRepository<Order>(context);
                }
                return _orderRipository;
            }
        }
        public GenericRepository<Government> GovernmentRipository
        {
            get
            {
                if (_governmentRipository == null)
                {
                    _governmentRipository = new GenericRepository<Government>(context);
                }
                return _governmentRipository;
            }
        }

        public GenericRepository<Region> RegionRipository
        {
            get
            {
                if (_regionRipository == null)
                {
                    _regionRipository = new GenericRepository<Region>(context);
                }
                return _regionRipository;
            }
        }

        public GenericRepository<Branch> BranchRipository
        {
            get
            {
                if (_branchRipository == null)
                {
                    _branchRipository = new GenericRepository<Branch>(context);
                }
                return _branchRipository;
            }
        }

        public GenericRepository<DeliveryMan> DeliveryManRipository
        {
            get
            {
                if (_deliveryManRipository == null)
                {
                    _deliveryManRipository = new GenericRepository<DeliveryMan>(context);
                }
                return _deliveryManRipository;
            }
        }

        public GenericRepository<DeliveryManRegions> DeliveryManRegionsRipository
        {
            get
            {
                if (_deliveryManRegionsRipository == null)
                {
                    _deliveryManRegionsRipository = new GenericRepository<DeliveryManRegions>(context);
                }
                return _deliveryManRegionsRipository;
            }
        }

        public GenericRepository<Employee> EmployeeRipository
        {
            get
            {
                if (_employeeRipository == null)
                {
                    _employeeRipository = new GenericRepository<Employee>(context);
                }
                return _employeeRipository;
            }
        }

        public GenericRepository<Seller> SellerRipository
        {
            get
            {
                if (_sellerRipository == null)
                {
                    _sellerRipository = new GenericRepository<Seller>(context);
                }
                return _sellerRipository;
            }
        }

        public GenericRepository<Admin> AdminRipository
        {
            get
            {
                if (_adminRipository == null)
                {
                    _adminRipository = new GenericRepository<Admin>(context);
                }
                return _adminRipository;
            }
        }


        // custom repos 
        public WeightSettingRepository WeightSettingRepository
        {
            get
            {
                if (_weightSettingRepository == null)
                {
                    _weightSettingRepository = new WeightSettingRepository(context);
                }
                return _weightSettingRepository;
            }
        }

        // Saving
        public void SaveChanges()
        {
            context.SaveChanges();
        }

    }
}
