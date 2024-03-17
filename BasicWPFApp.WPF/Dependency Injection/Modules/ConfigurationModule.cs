﻿using Microsoft.Extensions.Configuration;
using Ninject.Modules;

namespace BasicWPFApp.Presentation;

internal class ConfigurationModule : NinjectModule
{
	public override void Load()
	{
		Bind<IConfiguration>().ToMethod(ctx => new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build()).InSingletonScope();
	}
}
