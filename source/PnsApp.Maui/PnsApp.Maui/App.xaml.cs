﻿using PnsApp.Maui.Pages;

namespace PnsApp.Maui;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
