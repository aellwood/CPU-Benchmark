﻿namespace Benchmarking
{
	public abstract class Benchmark
	{
		protected Options options;

		protected Benchmark(Options options)
		{
			this.options = options;
		}

		public virtual void Run()
		{
		}

		public virtual string GetDescription()
		{
			return "Base Benchmark Class - You should never see this!";
		}

		public virtual void Initialize()
		{

		}

		public virtual double GetReferenceValue()
		{
			return 0.0d;
		}
	}
}