using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfrastructureBase
{
    public static class EntityEntryExtension
    {
        public static void NotModifyBaseProperties<T>(this EntityEntry<T> entityEntry) where T : PersistenceObjectBase
        {
            entityEntry.Property("CreateTime").IsModified = false;
            entityEntry.Property("CreateUserId").IsModified = false;
            entityEntry.Property("IsDeleted").IsModified = false;
        }
        public static void NotModifyBaseProperties<T>(this IEnumerable<EntityEntry<T>> entityEntrys) where T : PersistenceObjectBase
        {
            foreach(var entityEntry in entityEntrys)
            {
                entityEntry.NotModifyBaseProperties();
            }
        }
    }
}
