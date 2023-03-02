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
    [References(typeof(Country))]
    public long CountryId { get; set; }
    public string? Name { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? Postal { get; set; }
}

public class City
{
    public long Id { get; set; }
    [References(typeof(Province))]
    public long ProvinceId { get; set; }
    public string? Name { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? Postal { get; set; }
}

public class District
{
    public long Id { get; set; }
    [References(typeof(City))]
    public long CityId { get; set; }
    public string? Name { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? Postal { get; set; }
}

public class Village
{
    public long Id { get; set; }
    [References(typeof(District))]
    public long DistrictId { get; set; }
    public string? Name { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? Postal { get; set; }
}

public class QueryCountries : QueryDb<Country> {
}

public class QueryProvinces : QueryDb<Province> {
    public int? CountryId { get; set;}
}
public class QueryCities : QueryDb<City> {
    public int? ProvinceId { get; set;}
}

public class QueryDistricts : QueryDb<District> {
    public int? CityId { get; set;}
}

public class QueryVillages : QueryDb<Village> {
    public int? DistrictId { get; set;}
}

public class MasterBank
{
    public int Id { get; set; }
    public string? BankName { get; set; }
    public string? SwiftCode { get; set; }
}

public class QueryMasterBanks : QueryDb<MasterBank> {
    // [AutoDefault(Eval = null)]
    // public string? BankNameContains { get; set;} = null;
    // [AutoDefault(Eval = null)]
    // public string? SwiftCodeContains { get; set;} = null;
}