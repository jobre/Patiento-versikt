using System;
using System.Threading;
//using Ortoped.se.sll.bkv.externtest;
using Ortoped.se.sll.thord.www;
using Ortoped.Thord;

namespace Thord
{
	
	
	public class ThordFunctionsThreaded
	{
		public delegate void ExampleCallback(string s);
		public delegate void StringArray(string[] s);

		private ExampleCallback ecb;
		private StringArray sa;
		private ThordFunctions tf = null;

		public ThordFunctionsThreaded(ThordFunctions thordfunctions)
		{
			tf = thordfunctions;
		}

		public void getAllISOCode(StringArray s)
		{
			sa = s;
			Thread t = new Thread(new ThreadStart(thread_getAllISOCode));
			t.Start();
		}

		public void helloSecretThord(ExampleCallback cb)
		{
			ecb = cb;
			Thread t = new Thread(new ThreadStart(thread_helloSecretThord));
			t.Start();
		}


		public void helloThord(ExampleCallback cb)
		{
			ecb = cb;
			Thread t = new Thread(new ThreadStart(thread_helloThord));
			t.Start();
		}



		private void thread_helloSecretThord()
		{
//			ecb(tf.helloSecretThord());
		}

		private void thread_helloThord()
		{
//			ecb(tf.helloThord());
		}

		private void thread_getAllISOCode()
		{
//			sa(tf.getAllISOCode());
		}

	}
}
