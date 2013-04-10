using System;

namespace Ortoped.GarpGEM
{
	/// <summary>
	/// Abstrakt klass GarpCOM håller grundfunktioner
	/// mot Garp COM gränsnitt. Field app är static och är således
	/// den "gemensamma" porten för alla som implementerar GarpCOM
	/// </summary>
	abstract public class GarpCOM
	
	{
		public static Garp.Application app;
		protected static bool isGarpStarted = false;

		public GarpCOM()
		{
			connectToGarp("","");
		}


		protected bool connectToGarp(string user, string pwd)
		{
      if (app == null)
        app = new Garp.Application();

			// Om användare skickas med så gör en inloggning
			if(!user.Trim().Equals(""))
				app.Login(user,pwd);

			return checkGarp();
		}

//		public abstract bool doLogin(string user, string pwd);

		private bool checkGarp()
		{
			// Genom att kontrollera om användarnan innehåller något
			// kan vi avgöra ifall Garp är startat eller ej
			if(app.User == "")
			{
				isGarpStarted = false;
				return false;
			}
			else
			{
				isGarpStarted = true;
				return true;
			}
		}

		// Destructor
		~GarpCOM()
		{
			GC.Collect();
		}
	}
}
