﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KnowMyneighbour.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<MoreUserDetail> MoreUserDetails { get; set; }
        public virtual DbSet<VehicleDetail> VehicleDetails { get; set; }
        public virtual DbSet<VehicleRoute> VehicleRoutes { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<CarMake> CarMakes { get; set; }
        public virtual DbSet<CarModel> CarModels { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<TripNetwork> TripNetworks { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<AddressType> AddressTypes { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<SocialMediaRequest> SocialMediaRequests { get; set; }
    }
}
