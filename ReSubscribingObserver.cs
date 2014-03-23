using System;
using System.Reactive.Disposables;

namespace Kinect.ReactiveV2
{
    public class ReSubscribingObserver<T> : IObserver<T>
    {
        private IDisposable sub = Disposable.Empty;
        private IObservable<T> observable;
        private Action<T> _OnNext;
        private Action<Exception> _OnError;

        public ReSubscribingObserver(IObservable<T> observable, Action<T> _OnNext)
            : this(observable, _OnNext, new Action<Exception>(e => Console.WriteLine(e.Message)))
        { }

        public ReSubscribingObserver(IObservable<T> observable, Action<T> _OnNext, Action<Exception> _OnError)
        {
            this.observable = observable;
            this._OnNext = _OnNext;
            this._OnError = _OnError;
        }

        public void OnCompleted()
        {
            if (this.sub != Disposable.Empty)
                this.sub.Dispose();

            this.sub = this.observable.Subscribe(_ => this.OnNext(_),
                                                 e => this.OnError(e),
                                                 () => this.OnCompleted());
        }

        public void OnError(Exception error) { this._OnError(error); }

        public void OnNext(T value) { this._OnNext(value); }
    }
}