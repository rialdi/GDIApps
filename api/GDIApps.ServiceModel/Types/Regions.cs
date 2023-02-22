using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

public class Country
{
    public long Id { get; set; }
    public long? Parent { get; set; }
    public string? Name { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }

}

public class Province
{
    public long Id { get; set; }
    public long CountryId { get; set; }
    public string? Name { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? Postal { get; set; }

}

public class City
{
    public long Id { get; set; }
    public long ProvinceId { get; set; }
    public string? Name { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? Postal { get; set; }

}

public class District
{
    public long Id { get; set; }
    public long CityId { get; set; }
    public string? Name { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? Postal { get; set; }
}

public class Village
{
    public long Id { get; set; }
    public long DistrictId { get; set; }
    public string? Name { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? Postal { get; set; }
}

public class QueryCountries : QueryData<Country> {
}

public class QueryProvinces : QueryData<Province> {
    public int? CountryId { get; set;}
}
public class QueryCities : QueryData<City> {
    public int? ProvinceId { get; set;}
}

public class QueryDistricts : QueryData<District> {
    public int? CityId { get; set;}
}

public class QueryVillages : QueryData<Village> {
    public int? DistrictId { get; set;}
}