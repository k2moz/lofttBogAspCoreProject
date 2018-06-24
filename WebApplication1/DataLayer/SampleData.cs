using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public static class SampleData
    {
        public static void InitData(EFDBContext context)
        {
            if(!context.Directory.Any())
            {
                context.Directory.Add(new Entityes.Directory() { Title="First Directory", Html="<b>Directory Content</b>"});
                context.Directory.Add(new Entityes.Directory() { Title = "Second Directory", Html = "<b>Directory Content</b>" });
                context.SaveChanges();

                context.Material.Add(new Entityes.Material() { Title="First Material", Html="<i>Material Content</i>", DirectoryId = context.Directory.First().Id });
                context.Material.Add(new Entityes.Material() { Title = "Second Material", Html = "<i>Material Content</i>", DirectoryId = context.Directory.First().Id });
                context.Material.Add(new Entityes.Material() { Title = "Third Material", Html = "<i>Material Content</i>", DirectoryId = context.Directory.Last().Id });
                context.SaveChanges();
            }
        }
    }
}
