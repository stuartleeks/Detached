﻿using EntityFrameworkCore.Detached.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;

namespace EntityFrameworkCore.Detached.Plugins.Auditing.Conventions
{
    public class ModifiedDatePropertyAttributeConvention : PropertyAttributeConvention<ModifiedDateAttribute>
    {
        public override InternalPropertyBuilder Apply(InternalPropertyBuilder propertyBuilder, ModifiedDateAttribute attribute, MemberInfo clrMember)
        {
            var auditProps = AuditingPluginMetadata.GetMetadata(propertyBuilder.Metadata.DeclaringEntityType);
            auditProps.ModifiedDate = propertyBuilder.Metadata;
            propertyBuilder.Metadata.Ignore();
            return propertyBuilder;
        }
    }
}
