using System;
using System.Collections.Generic;
using System.Text;

using CodeCharm.Diagnostic;

using Microsoft.Extensions.Logging;

using Xunit.Abstractions;

namespace CodeCharm.Test.Diagnostic
{
    public class Feedback
        : IFeedback
    {
        private readonly ITestOutputHelper _output;

        public Feedback(ITestOutputHelper output)
        {
            _output = output;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            _output.WriteLine($"BeginScope {state}");
            return new FeedbackScope<TState>(_output, state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, string message)
        {
            this.Log(logLevel, eventId, state, exception, (TState s, Exception ex) => $"{message}; [State: {state}; Exception: {exception}]" );
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);
            if (null != exception)
            {
                message = $"{message}; Exception: {exception.Message}";
            }
            _output.WriteLine($"Level: {logLevel}; ID: {eventId}; {message}");
        }

        public class FeedbackScope<TState>
            : IDisposable
        {
            public FeedbackScope(ITestOutputHelper output, TState state)
            {
                _output = output;
                _state = state;
            }

            private bool disposedValue;
            private readonly ITestOutputHelper _output;
            private readonly TState _state;

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        _output.WriteLine($"Disposing: {_state}");
                        // TODO: dispose managed state (managed objects)
                    }

                    // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                    // TODO: set large fields to null
                    disposedValue = true;
                }
            }

            // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
            // ~FeedbackScope()
            // {
            //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            //     Dispose(disposing: false);
            // }

            public void Dispose()
            {
                // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }
    }
}
