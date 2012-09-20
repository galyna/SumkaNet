using Core.Data.Entities;
using FluentNHibernate.Mapping;

namespace Core.Data.Mappings
{
    public class StoreMap : ClassMap<Store>
    {
        public StoreMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.DetailURL);

            Map(x => x.HtmlBanner).CustomSqlType("NTEXT");
            Map(x => x.HtmlDetail).CustomSqlType("NTEXT");
            HasManyToMany(x => x.Products)
                .Cascade.All()
                .Table("StoreProduct");
            HasMany(x => x.Staff)
                .Cascade.All()
                .Inverse();
        }
    }
}