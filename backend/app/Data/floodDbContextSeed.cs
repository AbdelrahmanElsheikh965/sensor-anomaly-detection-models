
using Early_warning.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

using System.Threading.Tasks;

namespace Early_warning.Data
{
    public class floodDbContextSeed
    {
        public static async Task SeedDataAsync(FloodContext _context)
        {
            if (_context.Locations.Count()==0)
            {
                //read data 
                var locationsData = File.ReadAllText(@"Data/DataSeed/locations.json");

                // 2.Convert json string to list<T>

                 var locations = JsonSerializer.Deserialize<List<Location>>(locationsData);
               // var locations = JsonConvert.DeserializeObject<List<Location>>(locationsData);


                //// 3.Seed Data Tp DB
                //if (locations is not null && locations.Count() > 0)
                //{
                //    await _context.Locations.AddRangeAsync(locations);
                //    await _context.SaveChangesAsync();
                //}
                if (locations?.Count() > 0)
                {
                    foreach (var item in locations)
                    {
                        _context.Locations.Add(item);
                    }
                    await _context.SaveChangesAsync();
                }
            }

            if (_context.SeverityLevels.Count() == 0)
            {
                //read data 
                var severityLevelsData = File.ReadAllText("Data/DataSeed/severity_levels.json");

                // 2.Convert json string to list<T>

                var severityLevels = JsonSerializer.Deserialize<List<SeverityLevel>>(severityLevelsData);

                // 3.Seed Data Tp DB
                //if (severityLevels is not null && severityLevels.Count() > 0)
                //{
                //    await _context.Locations.AddRangeAsync(severityLevels);
                //    await _context.SaveChangesAsync();
                //}
                if (severityLevels?.Count() > 0)
                {
                    foreach (var item in severityLevels)
                    {
                        _context.SeverityLevels.Add(item);
                    }
                    await _context.SaveChangesAsync();
                }
            }


            if (_context.FloodCauses.Count() == 0)
            {
                //read data 
                var floodCausesData = File.ReadAllText("Data/DataSeed/flood_causes.json");

                // 2.Convert json string to list<T>

                var floodCauses = JsonSerializer.Deserialize<List<FloodCause>>(floodCausesData);

                //// 3.Seed Data Tp DB
                //if (floodCauses is not null && floodCauses.Count() > 0)
                //{
                //    await _context.Locations.AddRangeAsync(floodCauses);
                //    await _context.SaveChangesAsync();
                //}
                if (floodCauses?.Count() > 0)
                {
                    foreach (var item in floodCauses)
                    {
                        _context.FloodCauses.Add(item);
                    }
                    await _context.SaveChangesAsync();
                }

            }


            if (_context.FloodEvents.Count() == 0)
            {
                //read data 
                var floodEventssData = File.ReadAllText("Data/DataSeed/flood_events.json");

                // 2.Convert json string to list<T>

                var floodEvents = JsonSerializer.Deserialize<List<FloodEvent>>(floodEventssData);

                //// 3.Seed Data Tp DB
                //if (floodEvents is not null && floodEvents.Count() > 0)
                //{
                //    await _context.Locations.AddRangeAsync(floodEvents);
                //    await _context.SaveChangesAsync();
                //}
                if (floodEvents?.Count() > 0)
                {
                    foreach (var item in floodEvents)
                    {
                        _context.FloodEvents.Add(item);
                    }
                    await _context.SaveChangesAsync();
                }
            }


        }
    }

}
