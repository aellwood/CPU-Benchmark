﻿#region using

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#if NETCOREAPP3_0
using System.Runtime.Intrinsics.X86;

#endif

#endregion

namespace Benchmarking.Extension
{
	internal class AVX : Benchmark
	{
		private List<float[]> datas;
		private float randomFloatingNumber;

		public AVX(Options options) : base(options)
		{
		}

		public override void Run()
		{
#if NETCOREAPP3_0
			if (!Avx.IsSupported)
			{
				return;
			}

			var threads = new Task[options.Threads];

			for (var i = 0; i < options.Threads; i++)
			{
				var i1 = i;
				threads[i] = Task.Run(() =>
				{
					var randomFloatingSpan = new Span<float>(new[] {randomFloatingNumber});
					var dst = new Span<float>(datas[i1]);

					var iterations = 100000000 / options.Threads;

					for (var j = 0; j < iterations; j++)
					{
						AddScalarU(randomFloatingSpan, dst);
						MultiplyScalarU(randomFloatingSpan, dst);
					}

					BenchmarkRunner.ReportProgress();
				});
			}

			Task.WaitAll(threads);
#else
			return;
#endif
		}

		public override string GetDescription()
		{
			return "AVX benchmark of addition and multiplication on 512 floats (2048 bits) 100.000.000 times.";
		}

		public override void Initialize()
		{
			randomFloatingNumber = float.Epsilon;

			datas = new List<float[]>((int) options.Threads);

			for (var i = 0; i < options.Threads; i++)
			{
				// Multiple of 256 to test AVX only
				datas.Add(new float[512]);
			}
		}

		public override double GetComparison()
		{
			switch (options.Threads)
			{
				case 1:
				{
					return 7867.0d;
				}
				default:
				{
					return base.GetComparison();
				}
			}
		}

		public override double GetReferenceValue()
		{
#if NETCOREAPP3_0
			if (!Avx.IsSupported)
			{
				return 0.0d;
			}

			return 1113.0d;
#else
			return 0.0d;
#endif
		}

#if NETCOREAPP3_0
		private unsafe void MultiplyScalarU(Span<float> scalar, Span<float> dst)
		{
			fixed (float* pdst = dst)
			fixed (float* psrc = scalar)
			{
				var pDstEnd = pdst + dst.Length;
				var pDstCurrent = pdst;

				var scalarVector256 = Avx.BroadcastScalarToVector256(psrc);

				while (pDstCurrent + 8 <= pDstEnd)
				{
					var dstVector = Avx.LoadVector256(pDstCurrent);
					dstVector = Avx.Multiply(dstVector, scalarVector256);
					Avx.Store(pDstCurrent, dstVector);

					pDstCurrent += 8;
				}
			}
		}

		private unsafe void AddScalarU(Span<float> scalar, Span<float> dst)
		{
			fixed (float* pdst = dst)
			fixed (float* psrc = scalar)
			{
				var pDstEnd = pdst + dst.Length;
				var pDstCurrent = pdst;

				var scalarVector256 = Avx.BroadcastScalarToVector256(psrc);

				while (pDstCurrent + 8 <= pDstEnd)
				{
					var dstVector = Avx.LoadVector256(pDstCurrent);
					dstVector = Avx.Add(dstVector, scalarVector256);
					Avx.Store(pDstCurrent, dstVector);

					pDstCurrent += 8;
				}
			}
		}
#endif
		public override string GetCategory()
		{
			return "extension";
		}
	}
}