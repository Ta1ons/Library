using LibraryService.Data;
using LibraryService.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibraryService.Services
{
    public class DvdService
    {
        LibraryContext _context;

        public DvdService()
        {
            var dbAccessor = new DbAccessor();
            _context = dbAccessor.DataContext;
        }

        public void SaveDvd(Dvd dvdToSave)
        {

            if (dvdToSave.DvdID > 0)
            {
                var dvd = _context.Dvd.FirstOrDefault(x => x.DvdID == dvdToSave.DvdID);
                if (dvd != null) 
                {
                    dvd.Title = dvdToSave.Title;
                    dvd.Year = dvdToSave.Year;
                    dvd.Genre = dvdToSave.Genre;
                    dvd.Series = dvdToSave.Series;
                    dvd.SeriesNo = dvdToSave.SeriesNo;
                    _context.Update(dvd);
                }
            }
            else
            {
                Dvd dvd = new Dvd();

                dvd.Title = dvdToSave.Title;
                dvd.Year = dvdToSave.Year;
                dvd.Genre = dvdToSave.Genre;
                dvd.Series = dvdToSave.Series;
                dvd.SeriesNo = dvdToSave.SeriesNo;
                _context.Add(dvd);
            }
            _context.SaveChanges();
        }

        public List<Dvd> SearchDvds(string searchCriteria)
        {
            return _context.Dvd.Where(d => 
            d.Title.Contains(searchCriteria) || 
            d.Series.Contains(searchCriteria) || 
            d.Series.Contains(searchCriteria) ).ToList();
        }

        public void DeleteDvd(int dvdID)
        {      
            var dvd = _context.Dvd.FirstOrDefault(x => x.DvdID == dvdID);
            if (dvd != null)
            {
                _context.Remove(dvd);
                _context.SaveChanges();
            }
        }

        public void UpdateDvd(Book updatedDvd)
        {
            _context.Update(updatedDvd);
            _context.SaveChanges();
        }

        public List<Dvd> GetAllDvds()
        {
            return _context.Dvd.Take(200).ToList();
        }
    }
}

