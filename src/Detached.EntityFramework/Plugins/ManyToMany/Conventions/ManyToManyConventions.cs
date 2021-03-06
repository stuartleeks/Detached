﻿using Detached.EntityFramework.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Detached.EntityFramework.Plugins.ManyToMany.Conventions
{
    public class ManyToManyConventions : ICustomConventionBuilder
    {
        public int Priority { get; set; } = 0;

        public void AddConventions(ConventionSet conventionSet)
        {
            conventionSet.NavigationAddedConventions.Add(new ManyToManyNavigationConvention());
        }
    }
}
