﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BooksWebApp.Startup))]
namespace BooksWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}