using KnowMyneighbour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;

namespace KnowMyneighbour.Magic
{
    public class DAL
    {
        public Entities db = new Entities();
        public class Trips
        {
            
            private Entities db = new Entities();

            public IQueryable<Trip> TripIndex(string userid)
            {
                //var trips = db.Trips.Include(t => t.AspNetUser).Include(t => t.City).Include(t => t.City1);
                var trips = db.Trips.Where(t => t.AspNetUser.Id == userid).Include(t => t.City).Include(t => t.City1);
                return trips;
            }
            public IQueryable<Trip> GetAllTrips()
            {
                var trips = db.Trips.Include(t => t.AspNetUser).Include(t => t.City).Include(t => t.City1).OrderByDescending(m=>m.DateAdded);
                //var trips = db.Trips.Where(t => t.AspNetUser.Id == userid).Include(t => t.City).Include(t => t.City1);
                return trips;
            }

            internal static object GetIndex(DbSet<Trip> trips)
            {
                throw new NotImplementedException();
            }

            internal IQueryable<Trip> SearchTrips(int? startLocationId, int? destinationId)
            {
                var trips = db.Trips.Where(t => t.StartLocationId == startLocationId).Where(t => t.DestinationId == destinationId);

                return trips;
            }

            internal Trip GetTripsById(int? id)
            {
               var trip = db.Trips.Find(id);
                return trip;
            }

            internal void CreateTrip(Trip trip)
            {
                trip.DateAdded = DateTime.Now;
                trip.DateModified = DateTime.Now.AddYears(-100);
                trip.ActiveStatus = true;         
                db.Trips.Add(trip);
                db.SaveChanges();
            }

            public void EditTripById(Trip trip)
            {
                //trip.TripAdmin = User.Identity.GetUserId();
                trip.DateModified = DateTime.Now;
                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();
            }

            internal void DeleteTripsById(int id)
            {
                Trip trip = GetTripsById(id);
                db.Trips.Remove(trip);
                db.SaveChanges();
            }
        }
        public class Vehicles
        {
            private Entities db = new Entities();
            public IQueryable<VehicleDetail> VehicleIndex(string userid)
            {
                //var trips = db.Trips.Include(t => t.AspNetUser).Include(t => t.City).Include(t => t.City1);
                var vehicle = db.VehicleDetails.Where(t => t.AspNetUser.Id == userid);
                return vehicle;
            }
        }

        public class UserDetail
        {
            private Entities db = new Entities();

            public IQueryable<AspNetUser> GetUserById(string userid)
            {
                var user = db.AspNetUsers.Where(t => t.Id == userid);
                return user;
            }

           
        }

        public class Addresszz
        {
            private Entities db = new Entities();

            public IQueryable<Address> AddressIndex(string userid)
            {
                var add = db.Addresses.Where(a => a.AspNetUser.Id==userid).Include(a => a.City).Include(a => a.State).Include(a => a.AspNetUser);

                return add;
            }

            public Address GetAddressByID(int? id)
            {
                Address address = db.Addresses.Find(id);
                return address;
            }

            public int CreateNewAddress(Address addres)
            {
                int id = 0;

                try
                {

                    db.Addresses.Add(addres);
                    db.SaveChanges();
                    id = addres.Id;//return the row to check if insert was successful or not
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Log.Error(string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Log.Error(string.Format("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    
                }
                catch (Exception e)
                {
                    Log.Error("error saving new address. reason:<br/>" + e);
                }
                return id;
            }
        }

        public class TripNetwk
        {
            private Entities db = new Entities();

            public IQueryable<TripNetwork> CreateTripNetwork(TripNetwork nt)
            {
                int d = 0;
                nt.DateAdded = DateTime.Now;
                nt.ActiveStatus = true;
                nt.DateModified = DateTime.Now.AddYears(-100);
                db.TripNetworks.Add(nt);
                db.SaveChanges();
                d = nt.Id;
                var newTripNetwk = db.TripNetworks.Where(s => s.Id == d);
                return newTripNetwk;
                //try
                //{
                //    nt.DateAdded = DateTime.Now;
                //    nt.ActiveStatus = true;
                //    nt.DateModified = DateTime.Now.AddYears(-100);
                //    db.TripNetworks.Add(nt);
                //    db.SaveChanges();
                //    d = nt.Id;
                //    var newTripNetwk = db.TripNetworks.Where(s => s.Id == d);
                //    return newTripNetwk;

                //}
                //catch (Exception ex)
                //{
                //    Log.Error(ex.ToString());
                //    IQueryable<TripNetwork> newTripNetwk = new List<TripNetwork>()  ;
                //    return newTripNetwk;
                //}

            }

            public void EditTripNetwork(TripNetwork tripnetwork )
            {
                tripnetwork.DateModified = DateTime.Now;
                db.Entry(tripnetwork).State = EntityState.Modified;
                db.SaveChanges();
            }

            public TripNetwork Details(int? id)
            {
                TripNetwork tripNetwork = db.TripNetworks.Find(id);
                return tripNetwork;
            }

        }

        public class GeneralLogic
        {
            private Entities db = new Entities();
            internal bool CheckIfUserHasPaid(string user, int? id)
            {
               var answer = db.TripNetworks.Where(m => m.AspNetUser.Id == user).Where(m => m.Paid == true).Where(m=>m.TripsId==id).FirstOrDefault();
                if (answer==null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            internal void JoinUserToNetwork(int? id)
            {
                throw new NotImplementedException();
            }

            internal void RejectUserFromNetwork(int? id)
            {
                throw new NotImplementedException();
            }
        }
    }
}