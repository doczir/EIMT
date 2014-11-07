using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using EIMT.Models;

namespace EIMT.Managers
{
    public class ServiceProviderManager
    {
        private readonly ApplicationDbContext _context;

        public ServiceProviderManager(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registers a service provider.
        /// </summary>
        /// <param name="name">The name of the service provider.</param>
        /// <param name="pass">The unencrypted password.</param>
        /// <param name="accountNumber">The number of the service providers bank account.</param>
        /// <returns>A proxy object representing the service provider.</returns>
        public ServiceProviderIdentity RegisterServiceProvider(string name, string pass, string accountNumber)
        {
            var sp = new ServiceProvider {Name = name, Password = Encrypt(pass.Trim()), AccountNumber = accountNumber};

            _context.ServiceProviders.Add(sp);

            return new ServiceProviderIdentity(sp);
        }

        /// <summary>
        /// Authenticates a service provider.
        /// </summary>
        /// <param name="name">The name of the service provider.</param>
        /// <param name="pass">The unencrypted password.</param>
        /// <returns>A proxy object representing the service provider, null if authentication failed.</returns>
        public ServiceProviderIdentity Authenticate(string name, string pass)
        {
            var ep = Encrypt(pass);

            var query = (from s in _context.ServiceProviders
                where s.Name == name && s.Password == ep
                select s).FirstOrDefault();

            return query == null ? null : new ServiceProviderIdentity(query);
        }

        /// <summary>
        /// Queries a service provider using its id.
        /// </summary>
        /// <param name="id">The id of the service provider.</param>
        /// <returns>A proxy object representing the service provider, null if not found.</returns>
        public ServiceProviderIdentity GetById(string id)
        {
            var query = (from s in _context.ServiceProviders
                where s.Id == id
                select s).FirstOrDefault();

            return query == null ? null : new ServiceProviderIdentity(query);
        }

        /// <summary>
        /// Queries a service provider using its name.
        /// </summary>
        /// <param name="name">The name of the service provider.</param>
        /// <returns>A proxy object representing the service provider, null if not found.</returns>
        public ServiceProviderIdentity GetByName(string name)
        {
            var query = (from s in _context.ServiceProviders
                where s.Name == name
                select s).FirstOrDefault();

            return query == null ? null : new ServiceProviderIdentity(query);
        }

        /// <summary>
        /// Modifies a service provider.
        /// </summary>
        /// <param name="spi">The proxy containing the new information about the service provider.</param>
        /// <returns>True if succesfull, false otherwise.</returns>
        public bool Modify(ServiceProviderIdentity spi)
        {
            if (GetByName(spi.Name) != null) return false;

            var query = (from s in _context.ServiceProviders
                where s.Id == spi.Id
                select s).FirstOrDefault();

            if (query == null) return false;
            query.Name = spi.Name;
            query.Id = spi.Id;
            query.AccountNumber = spi.AccountNumber;

            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Modifies the password of a service provider.
        /// </summary>
        /// <param name="name">The name of the service provider.</param>
        /// <param name="newPass">The new password of the service provider.</param>
        /// <returns>True if succesfull, false otherwise.</returns>
        public bool ModifyPassword(string name, string newPass)
        {
            var query = (from s in _context.ServiceProviders
                         where s.Name == name
                         select s).FirstOrDefault();

            if (query == null) return false;
            query.Password = Encrypt(newPass);

            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Deletes a service provider.
        /// </summary>
        /// <param name="name">The name of the service provider.</param>
        /// <returns>True if succesfull, false otherwise.</returns>
        public bool Delete(string name)
        {
            var query = (from s in _context.ServiceProviders
                         where s.Id == name
                         select s).FirstOrDefault();
            
            if (query == null) return false;
            
            _context.ServiceProviders.Remove(query);
            
            _context.SaveChanges();
            return true;
        }

        private static string Encrypt(string text)
        {
            const string encryptionKey = "MAKV2SPBNI99212";
            var clearBytes = Encoding.Unicode.GetBytes(text);

            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(encryptionKey,
                    new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);


                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                    text = Convert.ToBase64String(ms.ToArray());
                }
            }
            return text;
        }
    }
}