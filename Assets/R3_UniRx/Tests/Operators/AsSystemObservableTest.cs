using System;
using NUnit.Framework;
using R3;
using R3Observable = R3.Observable;

namespace R3_UniRx.Tests.Operators
{
    public class AsSystemObservableTest
    {
        [Test]
        public void R3_AsSystemObservable_IObservableへ変換する()
        {
            // R3のObservable<T>からSystem.IObservable<T>へと変換する。

            // R3
            var observable = R3Observable.Range(1, 3);

            // R3 -> System.IObservable(UniRx)
            var systemObservable = observable.AsSystemObservable();
            Assert.IsInstanceOf<IObservable<int>>(systemObservable);
        }

        [Test]
        public void UniRx_AsSystemObservableは存在しない()
        {
            Assert.Ignore();
        }
    }
}